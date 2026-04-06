using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIGeneratorLibrary
{
    public class Space
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("versions")]
        public HashSet<Version> Versions { get; set; } = new();

        [JsonProperty("costs")]
        public Dictionary<ID, int> Costs { get; set; } = new();

        [JsonProperty("gains")]
        public Dictionary<ID, int> Gains { get; set; } = new();

        // nullable so missing entries mean "no explicit bound"
        [JsonProperty("costMinimums")]
        public Dictionary<ID, int?> CostMinimums { get; set; } = new();

        [JsonProperty("costMaximums")]
        public Dictionary<ID, int?> CostMaximums { get; set; } = new();

        [JsonProperty("gainMinimums")]
        public Dictionary<ID, int?> GainMinimums { get; set; } = new();

        [JsonProperty("gainMaximums")]
        public Dictionary<ID, int?> GainMaximums { get; set; } = new();

        [JsonProperty("totalCostMinimum")]
        public double? TotalCostMinimum { get; set; }

        [JsonProperty("CostsAllowed")]
        public bool CostsAllowed { get; set; } = true;
        [JsonProperty("GainsAllowed")]
        public bool GainsAllowed { get; set; } = true;

        // helpers
        public Space()
        {
            foreach (ID id in Enum.GetValues<ID>())
            {
                Costs.Add(id, 0);
                Gains.Add(id, 0);
            }
        }

        public Space(string name) { Name = name; }

        public Space SetVersion(Version v) { Versions.Add(v); return this; }
        public Space SetCost(ID id, int value) { Costs[id] = value; return this; }
        public Space SetGain(ID id, int value) { Gains[id] = value; return this; }
        public Space SetCostMin(ID id, int value) { CostMinimums[id] = value; return this; }
        public Space SetCostMax(ID id, int value) { CostMaximums[id] = value; return this; }
        public Space SetGainMin(ID id, int value) { GainMinimums[id] = value; return this; }
        public Space SetGainMax(ID id, int value) { GainMaximums[id] = value; return this; }
        public Space Default(ID id)
        {
            /*if (id == ID.FactionSpace)
            {
                SetCost(ID.BlueSpace, 0);
                SetCostMax(ID.BlueSpace, 0);
                SetCost(id, 1);
            }*/
            SetGainMax(ID.Combat, 1);
            SetCostMax(ID.Faction2req, 1);
            SetCost(id, 1);
            SetCost(ID.GoingToSpace, 1);
            return this;
        }
        public Space SetTotalCostMinimum(int value) { TotalCostMinimum = value; return this; }
        //public int GetTotalCostMinimum() { return TotalCostMinimum; }
        public Space SetNoCosts()
        {
            /*foreach(ID id in Enum.GetValues<ID>())
            {
                if (id != ID.GoingToSpace && id != ID.FactionSpace && id != ID.Oncegame && id != ID.GreenSpace && id != ID.BlueSpace && id != ID.YellowSpace)
                {
                    CostMaximums[id] = 0;
                }
            }
                
            return this;*/
            CostsAllowed = false; return this;
        }
        public Space SetNoGains()
        {
            /*foreach(ID id in Enum.GetValues<ID>())
            {
                if (id != ID.GoingToSpace && id != ID.FactionSpace && id != ID.Oncegame && id != ID.GreenSpace && id != ID.BlueSpace && id != ID.YellowSpace)
                {
                    CostMaximums[id] = 0;
                }
            }
                
            return this;*/
            GainsAllowed = false; return this;
        }
        public double Balance(double[] weights_base)
        {
            double total_gains = 0;
            double total_costs = 0;
            foreach (ID id in Enum.GetValues<ID>())
            {

                total_costs += weights_base[(int)id] * (Costs.TryGetValue(id, out var val) ? val : 0);
                total_gains += weights_base[(int)id] * (Gains.TryGetValue(id, out var val2) ? val2 : 0);
            }

            return total_gains - total_costs;
        }

        public double CheckTotalCosts(double[] weights_base)
        {
            double total_costs = 0;
            foreach (ID id in Enum.GetValues<ID>())
            {

                total_costs += weights_base[(int)id] * (Costs.TryGetValue(id, out var val) ? val : 0);
            }

            return total_costs;
        }

        public void Generate(double[] weights_base, double balance_leeway, List<ID> adjustable_costs, List<ID> adjustable_gains)
        {


            int counter = 0;
            // use shared RNG
            var rand = RandomProvider.Instance;
            while (Math.Abs(Balance(weights_base)) > balance_leeway)
            {
                if (counter > 10000) break;
                counter++;
                foreach (var kv in GainMinimums)
                {
                    var id = kv.Key;
                    var v = kv.Value;
                    //if (v < gMin || v > gMax) { reason = $"Cost {id}={v} outside global range [{gMin},{gMax}]"; return false; }
                    if (Gains.TryGetValue(id, out var gains))
                    {
                        if (gains < v)
                            Gains[id] = v.GetValueOrDefault();
                    }
                    else
                    {
                        Gains.Add(id, v.GetValueOrDefault());
                        //Gains.TryAdd(id, v.GetValueOrDefault());
                    }
                    //if (GainMinimums.TryGetValue(id, out var min) && !min.HasValue) { Gains.Add(id, )}
                    Console.WriteLine($"Set minimum {id} to {Gains[id]} on {Name}");
                }

                if (CheckTotalCosts(weights_base) < TotalCostMinimum)
                {
                    IEnumerable<ID> s2 = adjustable_costs.Shuffle();
                    //too few costs
                    foreach (ID id in s2)
                    {
                        int cost = 0;
                        Costs.TryGetValue(id, out cost);
                        int? maximum_cst = 0;
                        if (CostMaximums.TryGetValue(id, out maximum_cst))
                        {
                            if (cost + 1 > maximum_cst)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Adding cost for space cost minimums");
                                //if (!Costs.TryAdd(id, cost + 1))
                                //{
                                Costs[id] += 1;
                                //}
                                //Costs[id] += 1;
                            }
                        }

                    }
                }
                double difference = Balance(weights_base);
                if (difference < 0) //too many costs, too few gains
                {
                    int pattern = rand.Next(1, 3);
                    switch (pattern)
                    {
                        case 1:
                            //Console.WriteLine("Too many costs?");
                            IEnumerable<ID> s = adjustable_costs.Shuffle();
                            //too many costs
                            foreach (ID id in s)
                            {
                                int cost = 0;
                                Costs.TryGetValue(id, out cost);
                                int? minimum_cst = 0;
                                if (CostMinimums.TryGetValue(id, out minimum_cst))
                                {
                                    if (cost - 1 < minimum_cst || cost - 1 < 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //if (!Costs.TryAdd(id, cost - 1))
                                        //{
                                        Costs[id] -= 1;
                                        //}
                                        //Costs.Add(id, cost - 1);
                                        //Costs[id] -= 1;
                                        Console.WriteLine($"Adjusting cost down of {id} for {Name}");
                                        break;
                                    }
                                }
                                else if (cost - 1 >= 0)
                                {
                                    //if (!Costs.TryAdd(id, cost - 1))
                                    //{
                                    Costs[id] -= 1;
                                    //}
                                    //Costs[id] -= 1;
                                    Console.WriteLine($"Adjusting cost down of {id} for {Name}");
                                    break;
                                }
                            }
                            break;

                        case 2:
                            //Console.WriteLine("Too few gains?");
                            IEnumerable<ID> s2 = adjustable_gains.Shuffle();
                            //too few gains
                            foreach (ID id in s2)
                            {
                                int gain = 0;
                                Gains.TryGetValue(id, out gain);
                                int? maximum_gain = 0;
                                if (GainMaximums.TryGetValue(id, out maximum_gain))
                                {
                                    if (gain + 1 > maximum_gain)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //if(!Gains.TryAdd(id, gain + 1))
                                        //{
                                        Gains[id] += 1;
                                        //}
                                        //Gains.TryAdd(id, gain + 1);
                                        //Gains[id] += 1;
                                        Console.WriteLine($"Adjusting gain up of {id} for {Name}");
                                        break;
                                    }
                                }
                                else
                                {
                                    //if (!Gains.TryAdd(id, gain + 1))
                                    //{
                                    Gains[id] += 1;
                                    //}
                                    //Gains[id] += 1;
                                    Console.WriteLine($"Adjusting gain up of {id} for {Name}");
                                    break;
                                }
                            }
                            break;
                    }


                }
                else //too many gains, too few costs
                {
                    int pattern = rand.Next(1, 3);
                    switch (pattern)
                    {
                        case 1:
                            //Console.WriteLine("Too many gains?");
                            IEnumerable<ID> s = adjustable_gains.Shuffle();
                            //too many gains
                            foreach (ID id in s)
                            {
                                int gain = 0;
                                Gains.TryGetValue(id, out gain);
                                int? minimum_gain = 0;
                                if (GainMinimums.TryGetValue(id, out minimum_gain))
                                {
                                    if (gain - 1 < minimum_gain)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //if (!Gains.TryAdd(id, gain - 1))
                                        //{
                                        Gains[id] -= 1;
                                        //}
                                        //Gains.Add(id, gain - 1);
                                        //Gains[id] -= 1;
                                        Console.WriteLine($"Adjusting gain down of {id} for {Name}");
                                        break;
                                    }
                                }
                                else if (gain - 1 >= 0)
                                {
                                    //if (!Gains.TryAdd(id, gain - 1))
                                    //{
                                    Gains[id] -= 1;
                                    //}
                                    //Gains[id] -= 1;
                                    Console.WriteLine($"Adjusting gain down of {id} for {Name}");
                                    break;
                                }
                            }
                            break;

                        case 2:
                            //Console.WriteLine("Too few costs?");
                            IEnumerable<ID> s2 = adjustable_costs.Shuffle();
                            //too few costs
                            foreach (ID id in s2)
                            {
                                int cost = 0;
                                Costs.TryGetValue(id, out cost);
                                int? maximum_cst = 0;
                                if (CostMaximums.TryGetValue(id, out maximum_cst))
                                {
                                    if (cost + 1 > maximum_cst)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //if (!Costs.TryAdd(id, cost + 1))
                                        //{
                                        Costs[id] += 1;
                                        //}
                                        //Costs.Add(id, cost + 1);
                                        //Costs[id] += 1;
                                        Console.WriteLine($"Adjusting cost up of {id} for {Name}");
                                        break;
                                    }
                                }
                                else
                                {
                                    //if (!Costs.TryAdd(id, cost + 1))
                                    //{
                                    Costs[id] += 1;
                                    //}
                                    //Costs.Add(id, cost + 1);
                                    //Costs[id] += 1;
                                    Console.WriteLine($"Adjusting cost up of {id} for {Name}");
                                    break;
                                }
                            }
                            break;
                    }
                }

                foreach (ID id in Enum.GetValues<ID>())
                {
                    if (Gains.TryGetValue(id, out var gains) && Costs.TryGetValue(id, out var costs))
                    {
                        if (gains > 0 && costs > 0)
                        {
                            int mind = Math.Min(gains, costs);
                            Gains[id] -= mind;
                            Costs[id] -= mind;
                        }
                    }
                }
            }
            Console.WriteLine($"Space balance: {Balance(weights_base)} with leeway {balance_leeway}");
        }

        // validate against global min/max arrays or weights before accepting this configuration
        public bool Validate(out string reason)
        {
            foreach (var kv in Costs)
            {
                var id = kv.Key;
                var v = kv.Value;
                //if (v < gMin || v > gMax) { reason = $"Cost {id}={v} outside global range [{gMin},{gMax}]"; return false; }
                if (CostMinimums.TryGetValue(id, out var min) && min.HasValue && v < min.Value) { reason = $"Cost {id} below local min"; return false; }
                if (CostMaximums.TryGetValue(id, out var max) && max.HasValue && v > max.Value) { reason = $"Cost {id} above local max"; return false; }
            }
            foreach (var kv in Gains)
            {
                var id = kv.Key;
                var v = kv.Value;
                //if (v < gMin || v > gMax) { reason = $"Cost {id}={v} outside global range [{gMin},{gMax}]"; return false; }
                if (GainMinimums.TryGetValue(id, out var min) && min.HasValue && v < min.Value) { reason = $"Gain {id} below local min"; return false; }
                if (GainMaximums.TryGetValue(id, out var max) && max.HasValue && v > max.Value) { reason = $"Gain {id} above local max"; return false; }
            }
            // similar checks for gains...
            reason = null;
            return true;
        }

        public void DeleteDuplicates()
        {

        }

        // Example serializer settings:
        // var settings = new JsonSerializerSettings {
        //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //    Converters = new[] { new StringEnumConverter() },
        //    NullValueHandling = NullValueHandling.Ignore
        // };
        // File.WriteAllText(path, JsonConvert.SerializeObject(spaceInstance, Formatting.Indented, settings));
    }
}
