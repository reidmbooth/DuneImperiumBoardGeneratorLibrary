using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIGeneratorLibrary
{
    public class BoardSpaces
    {
        [JsonProperty]
        private List<Space> spaces { get; set; }

        [JsonProperty("GlobalGainMinimums")]
        public Dictionary<ID, int?> GlobalGainMinimums { get; set; } = new();

        [JsonProperty("GlobalGainMaximums")]
        public Dictionary<ID, int?> GlobalGainMaximums { get; set; } = new();

        [JsonProperty("GlobalCostMinimums")]
        public Dictionary<ID, int?> GlobalCostMinimums { get; set; } = new();

        [JsonProperty("GlobalCostMaximums")]
        public Dictionary<ID, int?> GlobalCostMaximums { get; set; } = new();

        [JsonProperty("Weights")]
        //private double[] weights_base = { 2.5, 1, 1.8, 2.0, 1.25, 1.5, 0.1, 0.5, 1.0, 0.5, 1.5, 2, 1.5, 1, 1.5, 1, 2, 8, 11, 1, 2, 2, 3, 2, 1.5, 1.5, 4.5, -0.5, -0.5, 2, 1, 2, 2.5, 2, 4, 8, 1, 1.5, 4.5 };
        private double[] weights_base = { 2.6, 1, 1.57, 1.52, 1.3, 1.6, 0.1, 0.3, 1.1, 0.7, 1.6, 2, 1.57, 0.73, 1.6, 0.6, 2.14, 8, 11, 0.95, 2.3, 2.2, 3.3, 1.3, 1.6, 1.51, 4.41, -0.73, 0.05, 1.6, 4.85, 2.1, 0.75, 2.17, 3.14, 6.18, 0, 1.6, 4.59 };


        [JsonProperty("BalanceLeeway")]
        private const double balance_leeway = 0.5;

        private Random rand;

        private List<ID> adjustable_gains = new();
        private List<ID> adjustable_costs = new();

        public void SetGlobalGainMin(ID id, int value) { GlobalGainMinimums[id] = value; }
        public void SetGlobalGainMax(ID id, int value) { GlobalGainMaximums[id] = value; }
        public void SetGlobalCostMin(ID id, int value) { GlobalCostMinimums[id] = value; }
        public void SetGlobalCostMax(ID id, int value) { GlobalCostMaximums[id] = value; }

        private int[] cost_targets = { 22, 19, 14, 6, 1, 8, 2, 5, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] gain_targets = { 0, 16, 6, 4, 0, 0, 0, 0, 0, 0, 3, 8, 17, 10, 7, 1, 1, 1, 1, 3, 0, 1, 1, 1, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        public void Generate(Version v, ILogger logger)
        {
            if (!File.Exists("config_balanced.json"))
            {
                logger.Log(LogLevel.Error, "config_balanced.json not found. Please create a config file with the appropriate structure.");
                return;
            }
            GeneratorConfig config = GeneratorConfig.LoadFromJson("config_balanced.json");

            if(config == null)
            {
                logger.Log(LogLevel.Error, "Failed to load config. Please ensure config_balanced.json is properly formatted and contains the necessary data.");
                return;
            }
            ItemPoolBuilder ipb = new ItemPoolBuilder(config, weights_base, v);
            // Build pool using shared RNG inside builder
            ipb.BuildPool(logger);
            SpaceAssembler sa = new SpaceAssembler(ipb.GetGainPool(), ipb.GetCostPool(), spaces, weights_base);
            sa.Assemble(logger);
            //config.Construct();
            //config.SaveToJson("config.json");
        }

        public bool Validate(ILogger logger, out string reason)
        {
            int[] gains_counts = new int[Enum.GetValues<ID>().Count()];
            int[] costs_counts = new int[Enum.GetValues<ID>().Count()];

            foreach (var space in spaces)
            {
                foreach (ID i in Enum.GetValues<ID>())
                {
                    gains_counts[(int)i] += space.Gains.TryGetValue(i, out var v) ? v : 0;
                    costs_counts[(int)i] += space.Costs.TryGetValue(i, out var v2) ? v2 : 0;
                }

            }
            for (int i = 0; i < gains_counts.Length; i++)
            {

                logger.Log(LogLevel.Debug, $"{(ID)i}:{gains_counts[i]}:{(GlobalGainMinimums.TryGetValue((ID)i, out var v) ? $"!!!{v}" : 0)}");

            }

            //string reason = "";
            System.Text.StringBuilder fail_reasons = new System.Text.StringBuilder();
            bool passing = true;

            for (int i = 0; i < gains_counts.Length; i++)
                if (gains_counts[i] < (GlobalGainMinimums.TryGetValue((ID)i, out var v) ? v : 0)) { fail_reasons.AppendLine($"Total {(ID)i} gain {gains_counts[i]} below global min {v}"); passing = false; }

            for (int i = 0; i < gains_counts.Length; i++)
                if (gains_counts[i] > (GlobalGainMaximums.TryGetValue((ID)i, out var v) ? v : 5000000)) { fail_reasons.AppendLine($"Total {(ID)i} gain {gains_counts[i]} above global max {v}"); passing = false; }

            for (int i = 0; i < costs_counts.Length; i++)
                if (costs_counts[i] < (GlobalCostMinimums.TryGetValue((ID)i, out var v) ? v : 0)) { fail_reasons.AppendLine($"Total {(ID)i} cost {costs_counts[i]} below global min {v}"); passing = false; }

            for (int i = 0; i < costs_counts.Length; i++)
                if (costs_counts[i] > (GlobalCostMaximums.TryGetValue((ID)i, out var v) ? v : 5000000)) { fail_reasons.AppendLine($"Total {(ID)i} cost {costs_counts[i]} above global max {v}"); passing = false; }

            foreach (Space space in spaces)
            {
                if (!space.Validate(out var space_reason))
                {
                    fail_reasons.AppendLine($"Space {space.Name} failed validation: {space_reason}");
                    passing = false;
                }
            }

            if (!passing)
            {
                reason = fail_reasons.ToString();
                return false;

            }
            // similar checks for gains...
            reason = null;
            return true;
        }

        public BoardSpaces()
        {
            spaces = new List<Space>();
            rand = RandomProvider.Instance;
        }

        public List<Space> GetSpaces()
        {
            return spaces;
        }

        public void SetSpaces(List<Space> s)
        {
            spaces = s;
        }
        public void Filter(Version to_filter)
        {
            spaces = spaces.Where(s => s.Versions.Contains(to_filter)).ToList();

            int[] base_cost_targets = { 22, 19, 14, 6, 1, 8, 2, 5, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] base_gain_targets = { 0, 16, 6, 4, 0, 0, 0, 0, 0, 0, 3, 8, 17, 10, 7, 1, 1, 1, 1, 3, 0, 1, 1, 1, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] ix_cost_targets = { 22, 18, 12, 6, 2, 8, 2, 5, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] ix_gain_targets = { 0, 8, 6, 4, 0, 0, 0, 0, 0, 0, 3, 8, 12, 10, 7, 1, 1, 1, 1, 3, 3, 1, 1, 1, 1, 1, 1, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] iximmo_cost_targets = { 22, 18, 12, 6, 2, 8, 2, 5, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] iximmo_gain_targets = { 0, 8, 6, 4, 0, 0, 0, 0, 0, 0, 3, 8, 12, 10, 6, 1, 1, 1, 1, 3, 3, 1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] uprising_cost_targets = { 23, 21, 13, 8, 3, 8, 2, 6, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] uprising_gain_targets = { 0, 7, 3, 1, 0, 0, 0, 0, 0, 0, 4, 8, 19, 10, 7, 1, 1, 1, 1, 3, 0, 0, 0, 1, 0, 0, 0, 2, 1, 0, 1, 2, 1, 1, 1, 1, 1, 0, 1 };

            if (to_filter == Version.Ix)
            {
                cost_targets = ix_cost_targets;
                gain_targets = ix_gain_targets;
            }
            else if (to_filter == Version.IxImmo)
            {
                cost_targets = iximmo_cost_targets;
                gain_targets = iximmo_gain_targets;
            }
            else if (to_filter == Version.Uprising)
            {
                cost_targets = uprising_cost_targets;
                gain_targets = uprising_gain_targets;
            }
        }

        public void PrintInformation(ILogger logger)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Space space in spaces)
            {
                
                stringBuilder.AppendLine($"{space.Name}:");
                stringBuilder.AppendLine($"Space Balance: {Math.Round(space.Balance(weights_base), 2)}");

                stringBuilder.Append(string.Join(", ", space.Costs.Where(kvp => kvp.Value > 0).Select(kvp => $"{kvp.Value} {kvp.Key}")));
                stringBuilder.Append(" -> ");

                stringBuilder.Append(string.Join(", ", space.Gains.Where(kvp => kvp.Value > 0).Select(kvp => $"{kvp.Value} {kvp.Key}")));
                stringBuilder.Append("\n\n");
                
            }
            logger.Log(LogLevel.Information, stringBuilder.ToString());
        }

        public static JsonSerializerSettings st = new JsonSerializerSettings { Formatting = Formatting.Indented, Converters = { new StringEnumConverter() } };

        public void SaveToJSON(string filename, ILogger logger)
        {
            try
            {
                File.WriteAllText(filename, JsonConvert.SerializeObject(this, st));
            }
            catch(Exception e)
            {
                logger.Log(LogLevel.Error, "Failed to save spaces to JSON. Make sure the file path is correct and you have write permissions. Exception message: " + e.Message);
            }
        }

        public static BoardSpaces LoadFromJSON(string filename, ILogger logger)
        {
            try
            {
                using (var reader = new StreamReader(filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    JsonReader jreader = new JsonTextReader(reader);
                    return serializer.Deserialize<BoardSpaces>(jreader);
                }
            }
            catch(Exception e)
            {
                logger.Log(LogLevel.Error, "Failed to load spaces from JSON. Make sure the file path is correct and the file is properly formatted. Exception message: " + e.Message);
            }
            return null;
        }
    }
}
