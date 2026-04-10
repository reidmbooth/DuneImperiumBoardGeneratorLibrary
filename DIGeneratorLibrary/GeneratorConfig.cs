using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Logging;

namespace DIGeneratorLibrary
{
    public class GeneratorConfig
    {
        [JsonProperty]
        public Dictionary<Version, List<Item>> configurations = new();
        //private List<Item> items = new();

        public GeneratorConfig() { 
            
        }

        public static GeneratorConfig LoadFromJson(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    throw new FileNotFoundException($"Configuration file not found: {filename}");
                }

                using (TextReader reader = new StreamReader(filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var result = (GeneratorConfig)serializer.Deserialize(reader, typeof(GeneratorConfig));
                    if (result == null)
                    {
                        throw new InvalidOperationException($"Failed to deserialize {filename}: result is null");
                    }
                    return result;
                }
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to parse JSON from {filename}: {ex.Message}", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"IO error reading {filename}: {ex.Message}", ex);
            }
        }

        public List<Item> GetItems(Version v)
        {
            return configurations[v];
        }

        /// <summary>
        /// Sets the items for a specific game version.
        /// </summary>
        public void SetItems(Version v, List<Item> items)
        {
            configurations[v] = items;
        }

        public void Construct()
        {
            List<Item> base_game = new List<Item>();
            List<Item> ix_game = new List<Item>();
            List<Item> iximmo_game = new List<Item>();
            List<Item> uprising_game = new List<Item>();
            base_game.Add(new Item { id = ID.GoingToSpace, minimum_cost = 20, maximum_cost = 24, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 4, minimum_cost_split = 4, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.SolariValue, minimum_cost = 17, maximum_cost = 21, minimum_gain = 14, maximum_gain = 18, maximum_cost_split = 5, minimum_cost_split = 4, maximum_gain_split = 4, minimum_gain_split = 4 });
            base_game.Add(new Item { id = ID.SpiceValue, minimum_cost = 12, maximum_cost = 16, minimum_gain = 4, maximum_gain = 8, maximum_cost_split = 4, minimum_cost_split = 3, maximum_gain_split = 3, minimum_gain_split = 2 });
            base_game.Add(new Item { id = ID.WaterValue, minimum_cost = 4, maximum_cost = 8, minimum_gain = 3, maximum_gain = 5, maximum_cost_split = 3, minimum_cost_split = 2, maximum_gain_split = 2, minimum_gain_split = 2 });
            base_game.Add(new Item { id = ID.Faction2req, minimum_cost = 0, maximum_cost = 2, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.FactionSpace, minimum_cost = 6, maximum_cost = 10, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.Oncegame, minimum_cost = 1, maximum_cost = 3, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.GreenSpace, minimum_cost = 4, maximum_cost = 6, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.BlueSpace, minimum_cost = 3, maximum_cost = 5, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.YellowSpace, minimum_cost = 4, maximum_cost = 6, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.DrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.FactionBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 6, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            base_game.Add(new Item { id = ID.GainTroops, minimum_cost = 0, maximum_cost = 0, minimum_gain = 15, maximum_gain = 19, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 4, minimum_gain_split = 4 });
            base_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 10, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 10, minimum_gain_split = 10 });
            base_game.Add(new Item { id = ID.DrawCard, minimum_cost = 0, maximum_cost = 0, minimum_gain = 5, maximum_gain = 9, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            base_game.Add(new Item { id = ID.StealIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.Trash, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.HighCouncil, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.Swordmaster, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.SpiceAccumulation, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.Shipping, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.Foldspace, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.Mentat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.TempInfluence, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.TechDiscountNegotiator, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.BuyTech, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.GetDreadnought, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.SolariFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 1, maximum_gain = 3, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.SpiceFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            base_game.Add(new Item { id = ID.Microscope, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.DiscardDrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.Contract, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.PickUpWorker, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.Spy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.TwoSpiceOrDeploy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.FourSpiceOrDeployTwo, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.GetMakerHook, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.DestroyShieldWall, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            base_game.Add(new Item { id = ID.WildBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });

            ix_game.Add(new Item { id = ID.GoingToSpace, minimum_cost = 20, maximum_cost = 24, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 4, minimum_cost_split = 4, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.SolariValue, minimum_cost = 16, maximum_cost = 20, minimum_gain = 6, maximum_gain = 10, maximum_cost_split = 5, minimum_cost_split = 4, maximum_gain_split = 3, minimum_gain_split = 2 });
            ix_game.Add(new Item { id = ID.SpiceValue, minimum_cost = 10, maximum_cost = 14, minimum_gain = 4, maximum_gain = 8, maximum_cost_split = 4, minimum_cost_split = 3, maximum_gain_split = 3, minimum_gain_split = 2 });
            ix_game.Add(new Item { id = ID.WaterValue, minimum_cost = 4, maximum_cost = 8, minimum_gain = 3, maximum_gain = 5, maximum_cost_split = 3, minimum_cost_split = 2, maximum_gain_split = 2, minimum_gain_split = 2 });
            ix_game.Add(new Item { id = ID.Faction2req, minimum_cost = 1, maximum_cost = 3, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.FactionSpace, minimum_cost = 6, maximum_cost = 10, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.Oncegame, minimum_cost = 1, maximum_cost = 3, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.GreenSpace, minimum_cost = 4, maximum_cost = 6, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.BlueSpace, minimum_cost = 3, maximum_cost = 5, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.YellowSpace, minimum_cost = 4, maximum_cost = 6, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.DrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.FactionBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 6, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            ix_game.Add(new Item { id = ID.GainTroops, minimum_cost = 0, maximum_cost = 0, minimum_gain = 10, maximum_gain = 14, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 3, minimum_gain_split = 3 });
            ix_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 10, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 10, minimum_gain_split = 10 });
            ix_game.Add(new Item { id = ID.DrawCard, minimum_cost = 0, maximum_cost = 0, minimum_gain = 5, maximum_gain = 9, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            ix_game.Add(new Item { id = ID.StealIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.Trash, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.HighCouncil, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.Swordmaster, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.SpiceAccumulation, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.Shipping, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.Foldspace, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.Mentat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.TempInfluence, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.TechDiscountNegotiator, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.BuyTech, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.GetDreadnought, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.SolariFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 1, maximum_gain = 3, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.SpiceFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            ix_game.Add(new Item { id = ID.Microscope, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.DiscardDrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.Contract, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.PickUpWorker, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.Spy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.TwoSpiceOrDeploy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.FourSpiceOrDeployTwo, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.GetMakerHook, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.DestroyShieldWall, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            ix_game.Add(new Item { id = ID.WildBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });

            iximmo_game.Add(new Item { id = ID.GoingToSpace, minimum_cost = 20, maximum_cost = 24, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 4, minimum_cost_split = 4, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.SolariValue, minimum_cost = 16, maximum_cost = 20, minimum_gain = 6, maximum_gain = 10, maximum_cost_split = 5, minimum_cost_split = 4, maximum_gain_split = 3, minimum_gain_split = 2 });
            iximmo_game.Add(new Item { id = ID.SpiceValue, minimum_cost = 10, maximum_cost = 14, minimum_gain = 4, maximum_gain = 8, maximum_cost_split = 4, minimum_cost_split = 3, maximum_gain_split = 3, minimum_gain_split = 2 });
            iximmo_game.Add(new Item { id = ID.WaterValue, minimum_cost = 4, maximum_cost = 8, minimum_gain = 3, maximum_gain = 5, maximum_cost_split = 3, minimum_cost_split = 2, maximum_gain_split = 2, minimum_gain_split = 2 });
            iximmo_game.Add(new Item { id = ID.Faction2req, minimum_cost = 1, maximum_cost = 3, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.FactionSpace, minimum_cost = 6, maximum_cost = 10, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.Oncegame, minimum_cost = 1, maximum_cost = 3, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.GreenSpace, minimum_cost = 4, maximum_cost = 6, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.BlueSpace, minimum_cost = 3, maximum_cost = 5, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.YellowSpace, minimum_cost = 4, maximum_cost = 6, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.DrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.FactionBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 6, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            iximmo_game.Add(new Item { id = ID.GainTroops, minimum_cost = 0, maximum_cost = 0, minimum_gain = 10, maximum_gain = 14, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 3, minimum_gain_split = 3 });
            iximmo_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 10, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 10, minimum_gain_split = 10 });
            iximmo_game.Add(new Item { id = ID.DrawCard, minimum_cost = 0, maximum_cost = 0, minimum_gain = 4, maximum_gain = 8, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            iximmo_game.Add(new Item { id = ID.StealIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.Trash, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.HighCouncil, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.Swordmaster, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.SpiceAccumulation, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.Shipping, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.Foldspace, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.Mentat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.TempInfluence, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.TechDiscountNegotiator, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.BuyTech, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.GetDreadnought, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.SolariFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 1, maximum_gain = 3, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.SpiceFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.Microscope, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            iximmo_game.Add(new Item { id = ID.DiscardDrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.Contract, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.PickUpWorker, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.Spy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.TwoSpiceOrDeploy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.FourSpiceOrDeployTwo, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.GetMakerHook, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.DestroyShieldWall, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            iximmo_game.Add(new Item { id = ID.WildBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });

            uprising_game.Add(new Item { id = ID.GoingToSpace, minimum_cost = 21, maximum_cost = 25, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 4, minimum_cost_split = 4, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.SolariValue, minimum_cost = 19, maximum_cost = 23, minimum_gain = 5, maximum_gain = 9, maximum_cost_split = 5, minimum_cost_split = 4, maximum_gain_split = 3, minimum_gain_split = 2 });
            uprising_game.Add(new Item { id = ID.SpiceValue, minimum_cost = 11, maximum_cost = 15, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 4, minimum_cost_split = 3, maximum_gain_split = 2, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.WaterValue, minimum_cost = 6, maximum_cost = 10, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 3, minimum_cost_split = 2, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.Faction2req, minimum_cost = 2, maximum_cost = 4, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.FactionSpace, minimum_cost = 6, maximum_cost = 10, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 10, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 10, minimum_gain_split = 10 });
            uprising_game.Add(new Item { id = ID.GreenSpace, minimum_cost = 4, maximum_cost = 8, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.BlueSpace, minimum_cost = 3, maximum_cost = 5, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.YellowSpace, minimum_cost = 4, maximum_cost = 6, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 2, minimum_cost_split = 2, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.DrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 3, maximum_gain = 5, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            uprising_game.Add(new Item { id = ID.FactionBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 6, maximum_gain = 10, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            uprising_game.Add(new Item { id = ID.GainTroops, minimum_cost = 0, maximum_cost = 0, minimum_gain = 17, maximum_gain = 21, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 4, minimum_gain_split = 4 });
            uprising_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 8, maximum_gain = 12, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 3, minimum_gain_split = 3 });
            uprising_game.Add(new Item { id = ID.DrawCard, minimum_cost = 0, maximum_cost = 0, minimum_gain = 5, maximum_gain = 9, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 2, minimum_gain_split = 2 });
            uprising_game.Add(new Item { id = ID.StealIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.Trash, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.HighCouncil, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.Swordmaster, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.SpiceAccumulation, minimum_cost = 0, maximum_cost = 0, minimum_gain = 2, maximum_gain = 4, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.Shipping, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.Foldspace, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.Mentat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.TempInfluence, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.TechDiscountNegotiator, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.BuyTech, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.GetDreadnought, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.SolariFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 1, maximum_gain = 3, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.SpiceFlag, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.Microscope, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.DiscardDrawIntrigue, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.Contract, minimum_cost = 0, maximum_cost = 0, minimum_gain = 1, maximum_gain = 3, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.PickUpWorker, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.Spy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.TwoSpiceOrDeploy, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.FourSpiceOrDeployTwo, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.GetMakerHook, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });
            uprising_game.Add(new Item { id = ID.DestroyShieldWall, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 0, minimum_gain_split = 0 });
            uprising_game.Add(new Item { id = ID.WildBump, minimum_cost = 0, maximum_cost = 0, minimum_gain = 0, maximum_gain = 2, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 1, minimum_gain_split = 1 });

            configurations.Add(Version.Base, base_game);
            configurations.Add(Version.Ix, ix_game);
            configurations.Add(Version.IxImmo, iximmo_game);
            configurations.Add(Version.Uprising, uprising_game);
        }

        public void SaveToJson(string filename)
        {
            try
            {
                File.WriteAllText(filename, JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Failed to save config to {filename}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Optimizes the configuration to minimize space imbalance by iterating parameter variations.
        /// </summary>
        public static GeneratorConfig OptimizeForMinimumImbalance(
            double[] weights_base,
            Microsoft.Extensions.Logging.ILogger logger,
            ConfigOptimizer.OptimizationSettings settings = null,
            Version version = Version.Base)
        {
            //GeneratorConfig cf = new GeneratorConfig();
            //cf.Construct();
            //settings.InitialConfigurations = cf.configurations;
            settings ??= new ConfigOptimizer.OptimizationSettings();
            settings.EvaluationRuns = 15;
            settings.TargetVersion = version;
            var optimizer = new ConfigOptimizer(weights_base, logger);
            var result = optimizer.OptimizeConfig(settings);
            return result.Config;
        }

        /// <summary>
        /// Optimizes the configuration to minimize space imbalance by iterating parameter variations.
        /// Uses provided initial configurations instead of defaults from Construct().
        /// </summary>
        public static GeneratorConfig OptimizeForMinimumImbalance(
            double[] weights_base,
            Microsoft.Extensions.Logging.ILogger logger,
            Dictionary<Version, List<Item>> initialConfigurations,
            Version version = Version.Base,
            ConfigOptimizer.OptimizationSettings settings = null)
        {
            settings ??= new ConfigOptimizer.OptimizationSettings();
            settings.InitialConfigurations = initialConfigurations;
            settings.TargetVersion = version;
            settings.EvaluationRuns = 20;
            settings.IgnoreItemIds = new List<ID>
            {
                ID.GoingToSpace,
                ID.YellowSpace,
                ID.GreenSpace,
                ID.BlueSpace,
                ID.FactionSpace,
                ID.Faction2req,
                ID.Swordmaster,
                ID.Mentat,
                ID.PickUpWorker,
                ID.Combat
            };
            settings.IgnoreItemCosts = new List<ID>
            {
                ID.GainTroops,
                ID.GetDreadnought,
                ID.Trash
            };
            var optimizer = new ConfigOptimizer(weights_base, logger);
            var result = optimizer.OptimizeConfig(settings);
            return result.Config;
        }

        public static void Compare(GeneratorConfig config1, GeneratorConfig config2, ILogger logger)
        {
            foreach(var kvp in config1.configurations)
            {
                if (config2.configurations.TryGetValue(kvp.Key, out var items2))
                {
                    logger.LogInformation($"Comparing version {kvp.Key}:");
                    var items1 = kvp.Value;
                    for (int i = 0; i < Math.Min(items1.Count, items2.Count); i++)
                    {
                        Item item1 = items1[i];
                        Item item2 = items2[i];
                        if(item1.minimum_cost != item2.minimum_cost || item1.maximum_cost != item2.maximum_cost || item1.minimum_gain != item2.minimum_gain || item1.maximum_gain != item2.maximum_gain)
                        {
                            logger.LogInformation($"  Item {item1.id} differs:");
                            logger.LogInformation($"    Config 1: cost [{item1.minimum_cost}, {item1.maximum_cost}], csplit [{item1.minimum_cost_split}, {item1.maximum_cost_split}], gain [{item1.minimum_gain}, {item1.maximum_gain}], gsplit [{item1.minimum_gain_split}, {item1.maximum_gain_split}]");
                            logger.LogInformation($"    Config 2: cost [{item2.minimum_cost}, {item2.maximum_cost}], csplit [{item2.minimum_cost_split}, {item2.maximum_cost_split}], gain [{item2.minimum_gain}, {item2.maximum_gain}], gsplit [{item2.minimum_gain_split}, {item2.maximum_gain_split}]");
                        }
                    }
                }
                else
                {
                    logger.LogInformation($"Version {kvp.Key} not found in config 2.");
                }
            }
        }

        /// <summary>
        /// Creates a deep clone of this configuration, copying all items for all versions.
        /// </summary>
        public GeneratorConfig DeepClone()
        {
            var cloned = new GeneratorConfig();

            foreach (var kvp in this.configurations)
            {
                var clonedItems = new List<Item>();
                foreach (var item in kvp.Value)
                {
                    clonedItems.Add(new Item
                    {
                        id = item.id,
                        minimum_cost = item.minimum_cost,
                        maximum_cost = item.maximum_cost,
                        minimum_gain = item.minimum_gain,
                        maximum_gain = item.maximum_gain,
                        minimum_cost_split = item.minimum_cost_split,
                        maximum_cost_split = item.maximum_cost_split,
                        minimum_gain_split = item.minimum_gain_split,
                        maximum_gain_split = item.maximum_gain_split
                    });
                }
                cloned.configurations[kvp.Key] = clonedItems;
            }

            return cloned;
        }
    }
}
