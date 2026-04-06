using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIGeneratorLibrary
{
    public class SpaceBuilder
    {
        public static void BuildSpaces(ILogger logger)
        {
            List<Space> spaces = new List<Space>();

            spaces.Add(new Space
            {
                Name = "Arrakeen",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo, Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.SolariFlag] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Carthag",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.SolariFlag] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Research Station",
                Versions = new HashSet<Version> { Version.Base, Version.Ix },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Research Station",
                Versions = new HashSet<Version> { Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Sietch Tabr",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Sell Melange",
                Versions = new HashSet<Version> { Version.Base },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Secure Contract",
                Versions = new HashSet<Version> { Version.Base },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Interstellar Shipping",
                Versions = new HashSet<Version> { Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Smuggling",
                Versions = new HashSet<Version> { Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Imperial Basin",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo, Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.SpiceFlag] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Hagga Basin",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "The Great Flat",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Conspire",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                TotalCostMinimum = 2,
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Wealth",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                CostsAllowed = false,
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Heighliner",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                TotalCostMinimum = 2,
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Foldspace",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                CostsAllowed = false,
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Selective Breeding",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                TotalCostMinimum = 2,
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Secrets",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                CostsAllowed = false,
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Hardy Warriors",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                TotalCostMinimum = 2,
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Stillsuits",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                CostsAllowed = false,
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.FactionBump] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.FactionBump] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "High Council",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1, [ID.Oncegame] = 1 },
                Gains = new Dictionary<ID, int> { [ID.HighCouncil] = 1 },
                TotalCostMinimum = 5,
                CostMaximums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                CostMinimums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.HighCouncil] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.HighCouncil] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Mentat",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.Mentat] = 1 },
                TotalCostMinimum = 2,
                GainMaximums = new Dictionary<ID, int?> { [ID.Mentat] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.Mentat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Swordmaster",
                Versions = new HashSet<Version> { Version.Base, Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1, [ID.Oncegame] = 1 },
                Gains = new Dictionary<ID, int> { [ID.Swordmaster] = 1 },
                TotalCostMinimum = 8,
                CostMaximums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                CostMinimums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Swordmaster] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.Swordmaster] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Hall of Oratory",
                Versions = new HashSet<Version> { Version.Base },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Rally Troops",
                Versions = new HashSet<Version> { Version.Base },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Tech Negotiation",
                Versions = new HashSet<Version> { Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Dreadnought",
                Versions = new HashSet<Version> { Version.Ix, Version.IxImmo },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            //Uprising spaces
            spaces.Add(new Space
            {
                Name = "Spice Refinery",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                Gains = new Dictionary<ID, int> { [ID.SolariFlag] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Research Station",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Sietch Tabr",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.BlueSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Hagga Basin",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Deep Desert",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Shipping",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Accept Contract",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.YellowSpace] = 1, [ID.GoingToSpace] = 1 },
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Combat] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Sardaukar",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                TotalCostMinimum = 2,
                CostMaximums = new Dictionary<ID, int?> { [ID.Faction2req] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Dutiful Service",
                Versions = new HashSet<Version> { Version.Uprising },
                CostsAllowed = false,
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
            });

            spaces.Add(new Space
            {
                Name = "Uprising Heighliner",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                TotalCostMinimum = 2
            });

            spaces.Add(new Space
            {
                Name = "Deliver Supplies",
                Versions = new HashSet<Version> { Version.Uprising },
                CostsAllowed = false,
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
            });

            spaces.Add(new Space
            {
                Name = "Espionage",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                TotalCostMinimum = 2
            });

            spaces.Add(new Space
            {
                Name = "Desert Tactics",
                Versions = new HashSet<Version> { Version.Uprising },
                CostsAllowed = false,
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
            });

            spaces.Add(new Space
            {
                Name = "Fremkit",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.FactionSpace] = 1, [ID.GoingToSpace] = 1 },
                TotalCostMinimum = 2
            });

            spaces.Add(new Space
            {
                Name = "High Council 1",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1, [ID.Oncegame] = 1 },
                Gains = new Dictionary<ID, int> { [ID.HighCouncil] = 1 },
                TotalCostMinimum = 5,
                CostMaximums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                CostMinimums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.HighCouncil] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.HighCouncil] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "High Council 2",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Imperial Privilege",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 },
                TotalCostMinimum = 3,
                Gains = new Dictionary<ID, int> { [ID.PickUpWorker] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Swordmaster",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1, [ID.Oncegame] = 1 },
                Gains = new Dictionary<ID, int> { [ID.Swordmaster] = 1 },
                TotalCostMinimum = 8,
                CostMaximums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                CostMinimums = new Dictionary<ID, int?> { [ID.Oncegame] = 1 },
                GainMaximums = new Dictionary<ID, int?> { [ID.Swordmaster] = 1 },
                GainMinimums = new Dictionary<ID, int?> { [ID.Swordmaster] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Assembly Hall",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 }
            });

            spaces.Add(new Space
            {
                Name = "Gather Support",
                Versions = new HashSet<Version> { Version.Uprising },
                Costs = new Dictionary<ID, int> { [ID.GreenSpace] = 1, [ID.GoingToSpace] = 1 }
            });


            BoardSpaces sp = new BoardSpaces();
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
}
