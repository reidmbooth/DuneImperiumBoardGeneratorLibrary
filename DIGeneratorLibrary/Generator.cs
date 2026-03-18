

namespace DIGeneratorLibrary
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Drawing;

    public class Spaces
    {
        [JsonProperty]
        private List<Space> spaces { get; set; }

        [JsonProperty("globalGainMinimums")]
        public Dictionary<ID, int?> GlobalGainMinimums { get; set; } = new();

        [JsonProperty("globalGainMaximums")]
        public Dictionary<ID, int?> GlobalGainMaximums { get; set; } = new();

        [JsonProperty("globalCostMinimums")]
        public Dictionary<ID, int?> GlobalCostMinimums { get; set; } = new();

        [JsonProperty("globalCostMaximums")]
        public Dictionary<ID, int?> GlobalCostMaximums { get; set; } = new();

        [JsonProperty("weights")]
        private double[] weights_base = { 2.5, 1, 1.8, 2.0, 1.25, 1.5, 0.1, 0.5, 1.0, 0.5, 1.5, 2, 1.5, 1, 1.5, 1, 2, 8, 11, 1, 2, 2, 3, 2, 1.5, 1.5, 4.5, -0.5, -0.5, 2, 1, 2, 2.5, 2, 4, 8, 1, 1.5, 4.5 };

        [JsonProperty("balanceLeeway")]
        private double balance_leeway = 0.5;

        private List<ID> adjustable_gains = new();
        private List<ID> adjustable_costs = new();

        public void SetGlobalGainMin(ID id, int value) { GlobalGainMinimums[id] = value; }
        public void SetGlobalGainMax(ID id, int value) { GlobalGainMaximums[id] = value; }
        public void SetGlobalCostMin(ID id, int value) { GlobalCostMinimums[id] = value; }
        public void SetGlobalCostMax(ID id, int value) { GlobalCostMaximums[id] = value; }

        public void Generate()
        {
            
            foreach (Space space in spaces)
            {
                Console.WriteLine($"Working on space {space.Name}");
                space.Generate(weights_base, balance_leeway, adjustable_costs, adjustable_gains);
                space.DeleteDuplicates();
            }
        }

        public bool Validate(out string reason)
        {
            int[] gains_counts = new int[Enum.GetValues<ID>().Count()];
            int[] costs_counts = new int[Enum.GetValues<ID>().Count()];

            foreach (var space in spaces)
            {
                foreach(ID i in Enum.GetValues<ID>())
                {
                    gains_counts[(int)i] += space.Gains.TryGetValue(i, out var v) ? v : 0;
                    costs_counts[(int)i] += space.Costs.TryGetValue(i, out var v2) ? v2 : 0;
                }
                
            }
            for (int i = 0; i < gains_counts.Length; i++)
            {
                Console.WriteLine($"{(ID)i}:{gains_counts[i]}:{(GlobalGainMinimums.TryGetValue((ID)i, out var v) ? $"!!!{v}" : 0)}");

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

            foreach(Space space in spaces)
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

        public Spaces()
        {
            spaces = new List<Space>();
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

            if (to_filter == Version.Base)
            {
                //adjustable_costs = new List<ID>();
                adjustable_costs.Add(ID.SolariValue);
                adjustable_costs.Add(ID.SpiceValue);
                adjustable_costs.Add(ID.WaterValue);
                //adjustable_costs.Add(ID.Faction2req);
                adjustable_costs.Add(ID.DrawIntrigue);
                adjustable_costs.Add(ID.DrawCard);

                //adjustable_gains = new List<ID>();
                adjustable_gains.Add(ID.SolariValue);
                adjustable_gains.Add(ID.SpiceValue);
                adjustable_gains.Add(ID.WaterValue);
                adjustable_gains.Add(ID.DrawIntrigue);
                //adjustable_gains.Add(ID.FactionBump);
                adjustable_gains.Add(ID.GainTroops);
                adjustable_gains.Add(ID.Combat);
                adjustable_gains.Add(ID.DrawCard);
                //adjustable_gains.Add(ID.StealIntrigue);
                adjustable_gains.Add(ID.Trash);
                //adjustable_gains.Add(ID.HighCouncil);
                //adjustable_gains.Add(ID.Swordmaster);
                adjustable_gains.Add(ID.SpiceAccumulation);
                //adjustable_gains.Add(ID.Shipping);
                adjustable_gains.Add(ID.Foldspace);
                //adjustable_gains.Add(ID.Mentat);
                adjustable_gains.Add(ID.TempInfluence);
                //adjustable_gains.Add(ID.TechDiscountNegotiator);
                //adjustable_gains.Add(ID.BuyTech);
                //adjustable_gains.Add(ID.GetDreadnought);
                //adjustable_gains.Add(ID.Microscope);
                adjustable_gains.Add(ID.DiscardDrawIntrigue);
                //adjustable_gains.Add(ID.Contract);
                //adjustable_gains.Add(ID.PickUpWorker);
                //adjustable_gains.Add(ID.Spy);
                //adjustable_gains.Add(ID.TwoSpiceOrDeploy);
                //adjustable_gains.Add(ID.FourSpiceOrDeployTwo);
                //adjustable_gains.Add(ID.GetMakerHook);
                //adjustable_gains.Add(ID.DestroyShieldWall);
                adjustable_gains.Add(ID.WildBump);
            }
            else if (to_filter == Version.Ix)
            {
                //List<ID> adjustable_costs = new List<ID>();
                adjustable_costs.Add(ID.SolariValue);
                adjustable_costs.Add(ID.SpiceValue);
                adjustable_costs.Add(ID.WaterValue);
                //adjustable_costs.Add(ID.Faction2req);
                adjustable_costs.Add(ID.DrawIntrigue);
                adjustable_costs.Add(ID.DrawCard);

                //List<ID> adjustable_gains = new List<ID>();
                adjustable_gains.Add(ID.SolariValue);
                adjustable_gains.Add(ID.SpiceValue);
                adjustable_gains.Add(ID.WaterValue);
                adjustable_gains.Add(ID.DrawIntrigue);
                //adjustable_gains.Add(ID.FactionBump);
                adjustable_gains.Add(ID.GainTroops);
                adjustable_gains.Add(ID.Combat);
                adjustable_gains.Add(ID.DrawCard);
                //adjustable_gains.Add(ID.StealIntrigue);
                adjustable_gains.Add(ID.Trash);
                //adjustable_gains.Add(ID.HighCouncil);
                //adjustable_gains.Add(ID.Swordmaster);
                adjustable_gains.Add(ID.SpiceAccumulation);
                adjustable_gains.Add(ID.Shipping);
                adjustable_gains.Add(ID.Foldspace);
                //adjustable_gains.Add(ID.Mentat);
                adjustable_gains.Add(ID.TempInfluence);
                adjustable_gains.Add(ID.TechDiscountNegotiator);
                adjustable_gains.Add(ID.BuyTech);
                adjustable_gains.Add(ID.GetDreadnought);
                //adjustable_gains.Add(ID.Microscope);
                adjustable_gains.Add(ID.DiscardDrawIntrigue);
                //adjustable_gains.Add(ID.Contract);
                //adjustable_gains.Add(ID.PickUpWorker);
                //adjustable_gains.Add(ID.Spy);
                //adjustable_gains.Add(ID.TwoSpiceOrDeploy);
                //adjustable_gains.Add(ID.FourSpiceOrDeployTwo);
                //adjustable_gains.Add(ID.GetMakerHook);
                //adjustable_gains.Add(ID.DestroyShieldWall);
                adjustable_gains.Add(ID.WildBump);
            }
            else if (to_filter == Version.IxImmo)
            {
                //List<ID> adjustable_costs = new List<ID>();
                adjustable_costs.Add(ID.SolariValue);
                adjustable_costs.Add(ID.SpiceValue);
                adjustable_costs.Add(ID.WaterValue);
                //adjustable_costs.Add(ID.Faction2req);
                adjustable_costs.Add(ID.DrawIntrigue);
                adjustable_costs.Add(ID.DrawCard);

                //List<ID> adjustable_gains = new List<ID>();
                adjustable_gains.Add(ID.SolariValue);
                adjustable_gains.Add(ID.SpiceValue);
                adjustable_gains.Add(ID.WaterValue);
                adjustable_gains.Add(ID.DrawIntrigue);
                //adjustable_gains.Add(ID.FactionBump);
                adjustable_gains.Add(ID.GainTroops);
                adjustable_gains.Add(ID.Combat);
                adjustable_gains.Add(ID.DrawCard);
                //adjustable_gains.Add(ID.StealIntrigue);
                adjustable_gains.Add(ID.Trash);
                //adjustable_gains.Add(ID.HighCouncil);
                //adjustable_gains.Add(ID.Swordmaster);
                adjustable_gains.Add(ID.SpiceAccumulation);
                adjustable_gains.Add(ID.Shipping);
                adjustable_gains.Add(ID.Foldspace);
                //adjustable_gains.Add(ID.Mentat);
                adjustable_gains.Add(ID.TempInfluence);
                adjustable_gains.Add(ID.TechDiscountNegotiator);
                adjustable_gains.Add(ID.BuyTech);
                adjustable_gains.Add(ID.GetDreadnought);
                adjustable_gains.Add(ID.Microscope);
                adjustable_gains.Add(ID.DiscardDrawIntrigue);
                //adjustable_gains.Add(ID.Contract);
                //adjustable_gains.Add(ID.PickUpWorker);
                //adjustable_gains.Add(ID.Spy);
                //adjustable_gains.Add(ID.TwoSpiceOrDeploy);
                //adjustable_gains.Add(ID.FourSpiceOrDeployTwo);
                //adjustable_gains.Add(ID.GetMakerHook);
                //adjustable_gains.Add(ID.DestroyShieldWall);
                adjustable_gains.Add(ID.WildBump);
            }
            else if (to_filter == Version.Uprising)
            {
                //List<ID> adjustable_costs = new List<ID>();
                adjustable_costs.Add(ID.SolariValue);
                adjustable_costs.Add(ID.SpiceValue);
                adjustable_costs.Add(ID.WaterValue);
                //adjustable_costs.Add(ID.Faction2req);
                adjustable_costs.Add(ID.DrawIntrigue);
                adjustable_costs.Add(ID.DrawCard);

                //List<ID> adjustable_gains = new List<ID>();
                adjustable_gains.Add(ID.SolariValue);
                adjustable_gains.Add(ID.SpiceValue);
                adjustable_gains.Add(ID.WaterValue);
                adjustable_gains.Add(ID.DrawIntrigue);
                //adjustable_gains.Add(ID.FactionBump);
                adjustable_gains.Add(ID.GainTroops);
                adjustable_gains.Add(ID.Combat);
                adjustable_gains.Add(ID.DrawCard);
                //adjustable_gains.Add(ID.StealIntrigue);
                adjustable_gains.Add(ID.Trash);
                //adjustable_gains.Add(ID.HighCouncil);
                //adjustable_gains.Add(ID.Swordmaster);
                adjustable_gains.Add(ID.SpiceAccumulation);
                //adjustable_gains.Add(ID.Shipping);
                adjustable_gains.Add(ID.Foldspace);
                //adjustable_gains.Add(ID.Mentat);
                adjustable_gains.Add(ID.TempInfluence);
                //adjustable_gains.Add(ID.TechDiscountNegotiator);
                //adjustable_gains.Add(ID.BuyTech);
                //adjustable_gains.Add(ID.GetDreadnought);
                //adjustable_gains.Add(ID.Microscope);
                adjustable_gains.Add(ID.DiscardDrawIntrigue);
                adjustable_gains.Add(ID.Contract);
                adjustable_gains.Add(ID.PickUpWorker);
                adjustable_gains.Add(ID.Spy);
                adjustable_gains.Add(ID.TwoSpiceOrDeploy);
                adjustable_gains.Add(ID.FourSpiceOrDeployTwo);
                adjustable_gains.Add(ID.GetMakerHook);
                adjustable_gains.Add(ID.DestroyShieldWall);
                adjustable_gains.Add(ID.WildBump);
            }
        }

        public void PrintInformation()
        {
            foreach(Space space in spaces)
            {
                Console.WriteLine($"{space.Name}:");
                foreach(KeyValuePair<ID, int> kvp in space.Costs){
                    if (kvp.Value > 0)
                    {
                        Console.Write($"{kvp.Value} {kvp.Key}, ");
                    }
                }
                Console.Write("\b\b -> ");
                foreach (KeyValuePair<ID, int> kvp in space.Gains)
                {
                    if (kvp.Value > 0)
                    {
                        Console.Write($"{kvp.Value} {kvp.Key}, ");
                    }
                }
                Console.Write("\b\b\n\n");
            }
        }

        public void SaveToJSON(string filename)
        {
            /*TextWriter writer = new StreamWriter(filename);
            JsonTextWriter w = new JsonTextWriter(writer);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(w, this);*/
            File.WriteAllText(filename, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        
        public static Spaces LoadFromJSON(string filename)
        {
            TextReader reader = new StreamReader(filename);
            JsonSerializer serializer = new JsonSerializer();
            return (Spaces)serializer.Deserialize(reader, typeof(Spaces));
        }
    }
    public enum ID
    {
        GoingToSpace = 0,
        SolariValue = 1,
        SpiceValue = 2,
        WaterValue = 3,
        Faction2req = 4,
        FactionSpace = 5,
        Oncegame = 6,
        GreenSpace = 7,
        BlueSpace = 8,
        YellowSpace = 9,
        DrawIntrigue = 10,
        FactionBump = 11,
        GainTroops = 12,
        Combat = 13,
        DrawCard = 14,
        StealIntrigue = 15,
        Trash = 16,
        HighCouncil = 17,
        Swordmaster = 18,
        SpiceAccumulation = 19,
        Shipping = 20,
        Foldspace = 21,
        Mentat = 22,
        TempInfluence = 23,
        TechDiscountNegotiator = 24,
        BuyTech = 25,
        GetDreadnought = 26,
        SolariFlag = 27,
        SpiceFlag = 28,
        Microscope = 29,
        DiscardDrawIntrigue = 30,
        Contract = 31,
        PickUpWorker = 32,
        Spy = 33,
        TwoSpiceOrDeploy = 34,
        FourSpiceOrDeployTwo = 35,
        GetMakerHook = 36,
        DestroyShieldWall = 37,
        WildBump = 38
    };

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Version
    {
        Base = 0,
        Ix = 1,
        IxImmo = 2,
        Uprising = 3,
        Bloodlines = 4
    }
    public class Generator
    {
        private double[] weights_base = { 2.5, 1, 1.8, 2.0, 1.25, 1.5, 0.1, 0.5, 1.0, 0.5, 1.5, 2, 1.5, 1, 1.5, 1, 2, 8, 11, 1, 2, 2, 3, 2, 1.5, 1.5, 4.5, -0.5, -0.5, 2, 1, 2, 2.5, 2, 4, 8, 1, 1.5, 4.5 };
        double[] minimums = { -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, 1, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0 };
        double[] maximums = { 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0 };

        public static void Generate(Version version)
        {
            Spaces spaces = Spaces.LoadFromJSON("spaces.json");
            spaces.Filter(version);

            Random rnd = new Random();
            //distribute necessary minimums
            foreach (KeyValuePair<ID, int?> entry in spaces.GlobalGainMinimums)
            {
                int? minimum = 0;
                if (!spaces.GlobalGainMinimums.TryGetValue(entry.Key, out minimum)) continue;
                if (entry.Key == ID.WaterValue)
                {
                    //Console.WriteLine("Adding water");
                    /*List<Space> spaces2 = new List<Space>();
                    spaces.GetSpaces().ForEach((item) =>
                    {
                        spaces2.Add(item);
                    });*/

                    //spaces2 = (List<Space>)spaces2.Shuffle();

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {
                        if(space.Name=="High Council" || space.Name == "Swordmaster")
                        {
                            continue;
                        }
                        if(space.TotalCostMinimum is not null)
                        {
                            Console.WriteLine($"Set {entry.Key} {space.Name}");
                            space.SetGainMin(entry.Key, 1);
                            minimum--;
                        }
                        if (minimum == 0)
                        {
                            break;
                        }   
                    }
                }
                if (entry.Key == ID.SpiceValue)
                {
                    /*Console.WriteLine("Adding spice");
                    List<Space> spaces2 = new List<Space>();
                    spaces.GetSpaces().ForEach((item) =>
                    {
                        spaces2.Add(item);
                    });*/

                    //spaces2 = (List<Space>)spaces2.Shuffle();

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {
                        if (space.Name == "High Council" || space.Name == "Swordmaster")
                        {
                            continue;
                        }
                        int val = rnd.Next(1, Math.Min((int)minimum, 5));
                        if (space.TotalCostMinimum is not null)
                        {
                            Console.WriteLine($"Set {entry.Key} {val} {space.Name}");
                            space.SetGainMin(entry.Key, val);
                            minimum -= val;
                        }
                        if (minimum == 0)
                        {
                            break;
                        }
                    }
                }
                if (entry.Key == ID.SolariValue)
                {

                    /*    Console.WriteLine("Adding solari");
                    List<Space> spaces2 = new List<Space>();
                    spaces.GetSpaces().ForEach((item) =>
                    {
                        spaces2.Add(item);
                    });*/

                    //spaces2 = (List<Space>)spaces2.Shuffle();

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {
                        if (space.Name == "High Council" || space.Name == "Swordmaster")
                        {
                            continue;
                        }
                        int val = rnd.Next(1, Math.Min((int)minimum, 9));
                        if (space.TotalCostMinimum is not null)
                        {
                            Console.WriteLine($"Set {entry.Key} {val} {space.Name}");
                            space.SetGainMin(entry.Key, val);
                            minimum -= val;
                        }
                        if (space.TotalCostMinimum is null)
                        {
                            Console.WriteLine($"Set {entry.Key} {val} {space.Name}");
                            space.SetGainMin(entry.Key, 1);
                            minimum -= 1;
                        }
                        if (minimum == 0)
                        {
                            break;
                        }
                    }
                }
                if (entry.Key == ID.Combat)
                {

                    /*    Console.WriteLine("Adding solari");
                    List<Space> spaces2 = new List<Space>();
                    spaces.GetSpaces().ForEach((item) =>
                    {
                        spaces2.Add(item);
                    });*/

                    //spaces2 = (List<Space>)spaces2.Shuffle();

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {
                        Console.WriteLine($"Set {entry.Key} {space.Name}");
                        space.SetGainMin(entry.Key, 1);
                        minimum -= 1;
                        //Console.WriteLine("Combat added");
                        if (minimum == 0)
                        {
                            break;
                        }
                    }
                }
                if (entry.Key == ID.DrawCard)
                {

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {
                        if (space.Name == "High Council" || space.Name == "Swordmaster")
                        {
                            continue;
                        }
                        int val = rnd.Next(1, Math.Min((int)minimum, 4));
                        if (space.TotalCostMinimum is not null)
                        {
                            Console.WriteLine($"Set {entry.Key} {val} {space.Name}");
                            space.SetGainMin(entry.Key, val);
                            minimum -= val;
                        }
                        if (minimum == 0)
                        {
                            break;
                        }
                    }
                }
                if (entry.Key == ID.DrawIntrigue)
                {

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {
                        if (space.Name == "High Council" || space.Name == "Swordmaster")
                        {
                            continue;
                        }
                        int val = rnd.Next(1, Math.Min((int)minimum, 4));
                        if (space.TotalCostMinimum is not null)
                        {
                            Console.WriteLine($"Set {entry.Key} {val} {space.Name}");
                            space.SetGainMin(entry.Key, val);
                            minimum -= val;
                        }
                        if (minimum == 0)
                        {
                            break;
                        }
                    }
                }

                if (entry.Key == ID.GainTroops)
                {

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {
                        if (space.Name == "High Council" || space.Name == "Swordmaster")
                        {
                            continue;
                        }
                        int val = rnd.Next(1, Math.Min((int)minimum, 6));
                        if (space.TotalCostMinimum is not null)
                        {
                            Console.WriteLine($"Set {entry.Key} {val} {space.Name}");
                            space.SetGainMin(entry.Key, val);
                            minimum -= val;
                        }
                        if (minimum == 0)
                        {
                            break;
                        }
                    }
                }

                if (entry.Key == ID.StealIntrigue)
                {
                    //Console.WriteLine("Adding water");
                    /*List<Space> spaces2 = new List<Space>();
                    spaces.GetSpaces().ForEach((item) =>
                    {
                        spaces2.Add(item);
                    });*/

                    //spaces2 = (List<Space>)spaces2.Shuffle();

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {


                        Console.WriteLine($"Set {entry.Key} {space.Name}");
                        space.SetGainMin(entry.Key, 1);
                        space.Gains.TryAdd(ID.StealIntrigue, 1);
                        minimum--;

                        if (minimum == 0)
                        {
                            break;
                        }



                    }
                }

            }

            foreach (KeyValuePair<ID, int?> entry in spaces.GlobalCostMinimums)
            {
                int? minimum = 0;
                if (!spaces.GlobalCostMinimums.TryGetValue(entry.Key, out minimum)) continue;
                if (entry.Key == ID.Faction2req)
                {
                    //Console.WriteLine("Adding water");
                    /*List<Space> spaces2 = new List<Space>();
                    spaces.GetSpaces().ForEach((item) =>
                    {
                        spaces2.Add(item);
                    });*/

                    //spaces2 = (List<Space>)spaces2.Shuffle();

                    foreach (Space space in spaces.GetSpaces().Shuffle())
                    {

                        if (space.Costs.TryGetValue(ID.FactionSpace,out int total))
                        {
                            if (total > 0)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine($"Set {entry.Key} {space.Name}");
                                space.SetCostMin(entry.Key, 1);
                                space.Costs.TryAdd(ID.Faction2req, 1);
                                minimum--;

                                if (minimum == 0)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Set {entry.Key} {space.Name}");
                            space.SetCostMin(entry.Key, 1);
                            space.Costs.TryAdd(ID.Faction2req, 1);
                            minimum--;

                            if (minimum == 0)
                            {
                                break;
                            }
                        }
                        
                        
                    }
                }
                

            }


            spaces.Generate();
            


            string reason = "";
            spaces.Validate(out reason);
            Console.WriteLine(reason);
            spaces.PrintInformation();
            spaces.SaveToJSON("spaces_" + version.ToString() + ".json");
        }


        public static void BuildSpaces()
        {
            List<Space> spaces = new List<Space>();
            
            spaces.Add(new Space("Arrakeen")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .SetVersion(Version.Uprising)
                .Default(ID.BlueSpace)
                .SetGain(ID.SolariFlag, 1)
                );
            spaces.Add(new Space("Carthag")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.BlueSpace)
                .SetGain(ID.SolariFlag, 1)
                );
            spaces.Add(new Space("Research Station")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .Default(ID.BlueSpace)
                );
            spaces.Add(new Space("Research Station")
                .SetVersion(Version.IxImmo)
                .Default(ID.BlueSpace)
                );
            spaces.Add(new Space("Sietch Tabr")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.BlueSpace)
                );
            spaces.Add(new Space("Sell Melange")
                .SetVersion(Version.Base)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("Secure Contract")
                .SetVersion(Version.Base)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("Interstellar Shipping")
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("Smuggling")
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("Imperial Basin")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .SetVersion(Version.Uprising)
                .Default(ID.YellowSpace)
                .SetGain(ID.SpiceFlag, 1)
                );
            spaces.Add(new Space("Hagga Basin")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("The Great Flat")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("Conspire")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                );
            spaces.Add(new Space("Wealth")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetNoCosts()
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                );
            spaces.Add(new Space("Heighliner")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                );
            spaces.Add(new Space("Foldspace")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetNoCosts()
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                );
            spaces.Add(new Space("Selective Breeding")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                );
            spaces.Add(new Space("Secrets")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetNoCosts()
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                );
            spaces.Add(new Space("Hardy Warriors")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                );
            spaces.Add(new Space("Stillsuits")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.FactionSpace)
                .SetNoCosts()
                .SetGainMax(ID.FactionBump, 1)
                .SetGainMin(ID.FactionBump, 1)
                .SetGain(ID.FactionBump, 1)
                 );
            spaces.Add(new Space("High Council")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.GreenSpace)
                .SetTotalCostMinimum(5)
                .SetCostMax(ID.Oncegame, 1)
                .SetCostMin(ID.Oncegame, 1)
                .SetGainMax(ID.HighCouncil, 1)
                .SetGainMin(ID.HighCouncil, 1)
                .SetGain(ID.HighCouncil, 1)
                .SetCost(ID.Oncegame, 1)
                 );
            spaces.Add(new Space("Mentat")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.GreenSpace)
                .SetTotalCostMinimum(2)
                .SetGainMax(ID.Mentat, 1)
                .SetGainMin(ID.Mentat, 1)
                .SetGain(ID.Mentat, 1)
                 );
            spaces.Add(new Space("Swordmaster")
                .SetVersion(Version.Base)
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.GreenSpace)
                .SetTotalCostMinimum(8)
                .SetCostMax(ID.Oncegame, 1)
                .SetCostMin(ID.Oncegame, 1)
                .SetGainMax(ID.Swordmaster, 1)
                .SetGainMin(ID.Swordmaster, 1)
                .SetGain(ID.Swordmaster, 1)
                .SetCost(ID.Oncegame, 1)
                 );
            spaces.Add(new Space("Hall of Oratory")
                .SetVersion(Version.Base)
                .Default(ID.GreenSpace)
                 );
            spaces.Add(new Space("Rally Troops")
                .SetVersion(Version.Base)
                .Default(ID.GreenSpace)
                 );
            spaces.Add(new Space("Tech Negotiation")
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.GreenSpace)
                 );
            spaces.Add(new Space("Dreadnought")
                .SetVersion(Version.Ix)
                .SetVersion(Version.IxImmo)
                .Default(ID.GreenSpace)
                 );
            
            
            //Uprising spaces
            spaces.Add(new Space("Spice Refinery")
                .SetVersion(Version.Uprising)
                .Default(ID.BlueSpace)
                .SetGain(ID.SolariFlag, 1)
                 );
            spaces.Add(new Space("Research Station")
                .SetVersion(Version.Uprising)
                .Default(ID.BlueSpace)
                 );
            spaces.Add(new Space("Sietch Tabr")
                .SetVersion(Version.Uprising)
                .Default(ID.BlueSpace)
                );
            spaces.Add(new Space("Hagga Basin")
                .SetVersion(Version.Uprising)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("Deep Desert")
                .SetVersion(Version.Uprising)
                .Default(ID.YellowSpace)
                 );
            spaces.Add(new Space("Shipping")
                .SetVersion(Version.Uprising)
                .Default(ID.YellowSpace)
                 );
            spaces.Add(new Space("Accept Contract")
                .SetVersion(Version.Uprising)
                .Default(ID.YellowSpace)
                );
            spaces.Add(new Space("Sardaukar")
                .SetVersion(Version.Uprising)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                 );
            spaces.Add(new Space("Dutiful Service")
                .SetVersion(Version.Uprising)
                .Default(ID.FactionSpace)
                .SetNoCosts()
                 );
            spaces.Add(new Space("Uprising Heighliner")
                .SetVersion(Version.Uprising)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                 );
            spaces.Add(new Space("Deliver Supplies")
                .SetVersion(Version.Uprising)
                .Default(ID.FactionSpace)
                .SetNoCosts()
                 );
            spaces.Add(new Space("Espionage")
                .SetVersion(Version.Uprising)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                 );
            spaces.Add(new Space("Desert Tactics")
                .SetVersion(Version.Uprising)
                .Default(ID.FactionSpace)
                .SetNoCosts()
                 );
            spaces.Add(new Space("Fremkit")
                .SetVersion(Version.Uprising)
                .Default(ID.FactionSpace)
                .SetTotalCostMinimum(2)
                 );

            spaces.Add(new Space("High Council 1")
                .SetVersion(Version.Uprising)
                .Default(ID.GreenSpace)
                .SetTotalCostMinimum(5)
                .SetCostMax(ID.Oncegame, 1)
                .SetCostMin(ID.Oncegame, 1)
                .SetGainMax(ID.HighCouncil, 1)
                .SetGainMin(ID.HighCouncil, 1)
                .SetGain(ID.HighCouncil, 1)
                 );
            spaces.Add(new Space("High Council 2")
                .SetVersion(Version.Uprising)
                .Default(ID.GreenSpace)
                 );
            spaces.Add(new Space("Imperial Privilege")
                .SetVersion(Version.Uprising)
                .Default(ID.GreenSpace)
                .SetTotalCostMinimum(3)
                .SetGain(ID.PickUpWorker, 1)
                 );
            spaces.Add(new Space("Swordmaster")
                .SetVersion(Version.Uprising)
                .Default(ID.GreenSpace)
                .SetTotalCostMinimum(8)
                .SetCostMax(ID.Oncegame, 1)
                .SetCostMin(ID.Oncegame, 1)
                .SetGainMax(ID.Swordmaster, 1)
                .SetGainMin(ID.Swordmaster, 1)
                .SetGain(ID.Swordmaster, 1)
                 );
            spaces.Add(new Space("Assembly Hall")
                .SetVersion(Version.Uprising)
                .Default(ID.GreenSpace)
                 );
            spaces.Add(new Space("Gather Support")
                .SetVersion(Version.Uprising)
                .Default(ID.GreenSpace)
                 );


            Spaces sp = new Spaces();
            sp.SetGlobalGainMin(ID.WaterValue, 3);
            sp.SetGlobalGainMin(ID.SpiceValue, 6);
            sp.SetGlobalGainMin(ID.SolariValue, 12);
            sp.SetGlobalGainMin(ID.DrawCard, 3);
            sp.SetGlobalGainMin(ID.DrawIntrigue, 3);
            sp.SetGlobalGainMin(ID.GainTroops, 10);
            sp.SetGlobalGainMin(ID.Combat, 10);
            sp.SetGlobalGainMin(ID.StealIntrigue, 1);

            sp.SetGlobalCostMin(ID.Faction2req, 2);

            
            sp.SetGlobalGainMax(ID.Swordmaster, 1);
            sp.SetGlobalGainMax(ID.HighCouncil, 1);
            sp.SetGlobalGainMax(ID.Mentat, 1);
            //sp.SetGlobalCostMax(ID.Faction2req, 1);

            sp.SetSpaces(spaces);
            sp.SaveToJSON("spaces.json");
        }
    }




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

        // helpers
        public Space() { }

        public Space(string name) { Name = name; }

        public Space SetVersion(Version v) { Versions.Add(v); return this; }
        public Space SetCost(ID id, int value) { Costs[id] = value; return this; }
        public Space SetGain(ID id, int value) { Gains[id] = value; return this; }
        public Space SetCostMin(ID id, int value) { CostMinimums[id] = value; return this; }
        public Space SetCostMax(ID id, int value) { CostMaximums[id] = value; return this; }
        public Space SetGainMin(ID id, int value) { GainMinimums[id] = value; return this; }
        public Space SetGainMax(ID id, int value) { GainMaximums[id] = value; return this; }
        public Space Default(ID id) {
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
        public Space SetNoCosts() {
            foreach(ID id in Enum.GetValues<ID>())
            {
                if (id != ID.GoingToSpace && id != ID.FactionSpace && id != ID.Oncegame && id != ID.GreenSpace && id != ID.BlueSpace && id != ID.YellowSpace)
                {
                    CostMaximums[id] = 0;
                }
            }
                
            return this;
        }
        public double Balance(double[] weights_base)
        {
            double total_gains = 0;
            double total_costs = 0;
            foreach(ID id in Enum.GetValues<ID>())
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
           

            

            Random rand = new Random();
            while (Math.Abs(Balance(weights_base)) > balance_leeway)
            {
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
                        Gains.TryAdd(id, v.GetValueOrDefault());
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
                                if (!Costs.TryAdd(id, cost + 1))
                                {
                                    Costs[id] += 1;
                                }
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
                                        if (!Costs.TryAdd(id, cost - 1))
                                        {
                                            Costs[id] -= 1;
                                        }
                                        //Costs.Add(id, cost - 1);
                                        //Costs[id] -= 1;
                                        Console.WriteLine($"Adjusting cost down of {id} for {Name}");
                                        break;
                                    }
                                }
                                else if (cost - 1 >= 0)
                                {
                                    if (!Costs.TryAdd(id, cost - 1))
                                    {
                                        Costs[id] -= 1;
                                    }
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
                                        if(!Gains.TryAdd(id, gain + 1))
                                        {
                                            Gains[id] += 1;
                                        }
                                        //Gains.TryAdd(id, gain + 1);
                                        //Gains[id] += 1;
                                        Console.WriteLine($"Adjusting gain up of {id} for {Name}");
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!Gains.TryAdd(id, gain + 1))
                                    {
                                        Gains[id] += 1;
                                    }
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
                                        if (!Gains.TryAdd(id, gain - 1))
                                        {
                                            Gains[id] -= 1;
                                        }
                                        //Gains.Add(id, gain - 1);
                                        //Gains[id] -= 1;
                                        Console.WriteLine($"Adjusting gain down of {id} for {Name}");
                                        break;
                                    }
                                }
                                else if (gain - 1 >= 0)
                                {
                                    if (!Gains.TryAdd(id, gain - 1))
                                    {
                                        Gains[id] -= 1;
                                    }
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
                                        if (!Costs.TryAdd(id, cost + 1))
                                        {
                                            Costs[id] += 1;
                                        }
                                        //Costs.Add(id, cost + 1);
                                        //Costs[id] += 1;
                                        Console.WriteLine($"Adjusting cost up of {id} for {Name}");
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!Costs.TryAdd(id, cost + 1))
                                    {
                                        Costs[id] += 1;
                                    }
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
