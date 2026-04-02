using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIGeneratorLibrary
{
    public class SpaceAssembler
    {
        private List<PoolItem> GainPoolItems;
        private List<PoolItem> CostPoolItems;
        private List<Space> Spaces;

        private List<PoolItem> combined = new();
        private double[] weights_base;

        public SpaceAssembler(List<PoolItem> _GainPoolItems, List<PoolItem> _CostPoolItems, List<Space> _PartialSpaces, double[] _weights_base) { 
            GainPoolItems = _GainPoolItems;
            CostPoolItems = _CostPoolItems;
            Spaces = _PartialSpaces;
            weights_base = _weights_base;
            
        }

        public void Assemble(ILogger logger)
        {
            foreach (PoolItem p in GainPoolItems)
            {
                combined.Add(p);
            }
            foreach (PoolItem p in CostPoolItems)
            {
                PoolItem p2 = new PoolItem { id = p.id, qty = p.qty, value = p.value * -1 };
                combined.Add(p2);
            }




            
            //Spaces = Spaces.OrderByDescending(p => Math.Abs(p.Balance(weights_base))).ToList<Space>();

            //force add benefit to mentat and force good costs for swordmaster and high council if available
            Space mentat = Spaces.Where(i => i.Name == "Mentat").FirstOrDefault<Space>();
            Space swordmaster = Spaces.Where(i => i.Name == "Swordmaster").FirstOrDefault<Space>();
            Space highcouncil = Spaces.Where(i => i.Name == "High Council").FirstOrDefault<Space>();
            if (mentat is null || swordmaster is null || highcouncil is null)
            {
                throw new Exception("Space missing exception");
            }
            ID[] toadd = { ID.DrawIntrigue, ID.WaterValue, ID.SpiceValue, ID.DrawCard, ID.Trash };
            ID forced = toadd.Shuffle().First();
            mentat.Gains[forced]++;//add a gain to mentat
            for(int i = 0; i < combined.Count; i++)
            {
                PoolItem p = combined[i];
                if(p.id == forced)
                {
                    if(p.qty == 1)
                    {
                        combined.Remove(p);
                        break;
                    }
                    else
                    {
                        p.qty--;
                    }
                }
            }

            combined = combined.OrderByDescending(p => Math.Abs(p.value)).ToList<PoolItem>();
            //do some specific initial configurations to create a more even game
            /*Space mentat = Spaces.Where(i => i.Name == "Mentat").FirstOrDefault<Space>();
            Space swordmaster = Spaces.Where(i => i.Name == "Swordmaster").FirstOrDefault<Space>();
            Space highcouncil = Spaces.Where(i => i.Name == "High Council").FirstOrDefault<Space>();
            if (mentat is null || swordmaster is null || highcouncil is null)
            {
                throw new Exception("Space missing exception");
            }
            
            combined.Select(i => )*/

            foreach (PoolItem p in combined)
            {
                double least_impact = 55535835;
                Space least_impact_causing = null;
                logger.Log(LogLevel.Debug, $"Attempting to add {p.id} {p.value}");

                if (p.value < 0)
                {
                    List<Space> sorted = Spaces.Shuffle().OrderByDescending(p => p.Balance(weights_base)).ToList<Space>();
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        Space sp = sorted[i];
                        if (sorted[i].Gains[p.id] > 0) continue;
                        if (sorted[i].CostsAllowed == false) continue;
                        if (sorted[i].Costs[ID.FactionSpace] > 0 && p.id == ID.Faction2req) continue;
                        sp.Costs[p.id] = sp.Costs[p.id] + p.qty;
                        logger.Log(LogLevel.Debug, $"Adding {p.id} {p.qty} {p.value} to space {sp.Name} seemed to be the best improvement, new val {sp.Balance(weights_base)}");
                        break;
                    }

                }
                if (p.value > 0)
                {
                    List<Space> sorted = Spaces.Shuffle().OrderBy(p => p.Balance(weights_base)).ToList<Space>();
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        Space sp = sorted[i];
                        if (sorted[i].Costs[p.id] > 0) continue;
                        if (sorted[i].GainsAllowed == false) continue;
                        if (p.id == ID.Combat && sorted[i].Gains[ID.Combat] > 0) continue;
                        sp.Gains[p.id] = sp.Gains[p.id] + p.qty;
                        logger.Log(LogLevel.Debug, $"Adding {p.id} {p.qty} {p.value} to space {sp.Name} seemed to be the best improvement, new val {sp.Balance(weights_base)}");
                        break;
                    }
                }
                /*foreach(Space space in Spaces)
                {
                    if (p.value < 0)
                    {
                        //find space with largest balance
                    }
                }*/
                /*if (p.value < 0)
                {
                    least_impact_causing = least_impact_causing.SetCost(p.id, p.qty + least_impact_causing.Costs[p.id]);
                }
                else if (p.value > 0)
                {
                    least_impact_causing = least_impact_causing.SetGain(p.id, p.qty + least_impact_causing.Gains[p.id]);
                }*/

                //Console.WriteLine($"Adding {p.id} {p.qty} {p.value} to space {least_impact_causing.Name} seemed to be the best improvement, value {least_impact}");
            }

            
        }

    }
}
