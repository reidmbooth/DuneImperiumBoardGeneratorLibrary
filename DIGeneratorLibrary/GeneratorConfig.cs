using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DIGeneratorLibrary
{
    public class GeneratorConfig
    {
        [JsonProperty]
        private Dictionary<Version, List<Item>> configurations = new();
        //private List<Item> items = new();

        public GeneratorConfig() { 
            
        }

        public static GeneratorConfig LoadFromJson(string filename)
        {
            TextReader reader = new StreamReader(filename);
            JsonSerializer serializer = new JsonSerializer();
            return (GeneratorConfig)serializer.Deserialize(reader, typeof(GeneratorConfig));
        }

        public List<Item> GetItems(Version v)
        {
            return configurations[v];
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
            base_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 8, maximum_gain = 12, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 3, minimum_gain_split = 3 });
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
            ix_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 8, maximum_gain = 12, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 3, minimum_gain_split = 3 });
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
            iximmo_game.Add(new Item { id = ID.Combat, minimum_cost = 0, maximum_cost = 0, minimum_gain = 8, maximum_gain = 12, maximum_cost_split = 0, minimum_cost_split = 0, maximum_gain_split = 3, minimum_gain_split = 3 });
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
            uprising_game.Add(new Item { id = ID.Oncegame, minimum_cost = 1, maximum_cost = 3, minimum_gain = 0, maximum_gain = 0, maximum_cost_split = 1, minimum_cost_split = 1, maximum_gain_split = 0, minimum_gain_split = 0 });
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
            File.WriteAllText(filename, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
