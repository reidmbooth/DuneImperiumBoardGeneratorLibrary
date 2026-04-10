using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Logging;

namespace DIGeneratorLibrary
{
    public record struct PoolItem(ID id, int qty, double value);
    /*public struct PoolItem
    {
        public ID id;
        public int qty;
        public double value;
    }*/
    public class ItemPoolBuilder
    {
        private GeneratorConfig config;
        private double[] weights_base;
        private Version version;

        private List<PoolItem> GainPoolItems = new();
        private List<PoolItem> CostPoolItems = new();

        public ItemPoolBuilder(GeneratorConfig _config, double[] _weights_base, Version _version)
        {
            config = _config;
            weights_base = _weights_base;
            version = _version;
        }

        public void BuildPool(ILogger logger)
        {
            // use shared Random instance
            var rand = RandomProvider.Instance;
            foreach (var item in config.GetItems(version)
                .Where(i => i.id != ID.FactionSpace)
                .Where(i => i.id != ID.GoingToSpace)
                .Where(i => i.id != ID.BlueSpace)
                .Where(i => i.id != ID.GreenSpace)
                .Where(i => i.id != ID.YellowSpace)
                .Where(i => i.id != ID.SolariFlag)
                .Where(i => i.id != ID.SpiceFlag)
                .Where(i => i.id != ID.Mentat)
                .Where(i => i.id != ID.Swordmaster)
                .Where(i => i.id != ID.HighCouncil)
                .Where(i => i.id != ID.FactionBump)
                .Where(i => i.id != ID.Oncegame)
                .Where(i => i.id != ID.PickUpWorker))
            {
                //int gainqty = 0;
                if (item.maximum_gain > 0)
                {
                    int r = rand.Next(item.minimum_gain, item.maximum_gain + 1);
                    logger.Log(LogLevel.Debug, $"{r} {item.id} rolled for gain");
                    if (r > 0)
                    {
                        // Ensure split is at least 1 when we have something to partition
                        int minSplit = Math.Max(1, item.minimum_gain_split);
                        int maxSplit = Math.Max(minSplit, item.maximum_gain_split);
                        int split = Math.Min(rand.Next(minSplit, maxSplit + 1), r);
                        List<int> partition = RandomPartitionInt(r, split);
                        foreach (int i in partition)
                        {
                            logger.Log(LogLevel.Debug, $"Added to gain pool {i} {item.id} with total value {i * weights_base[(int)item.id]}");
                            GainPoolItems.Add(new PoolItem { id = item.id, qty = i, value = i * weights_base[(int)item.id] });
                        }
                    }
                }
                if (item.maximum_cost > 0)
                {
                    /*if (item.maximum_cost_split == 1)
                    {*/
                    int r = rand.Next(item.minimum_cost, item.maximum_cost + 1);
                    logger.Log(LogLevel.Debug, $"{r} {item.id} rolled");
                    if (r > 0)
                    {
                        // Ensure split is at least 1 when we have something to partition
                        int minSplit = Math.Max(1, item.minimum_cost_split);
                        int maxSplit = Math.Max(minSplit, item.maximum_cost_split);
                        int split = Math.Min(rand.Next(minSplit, maxSplit + 1), r);
                        List<int> partition = RandomPartitionInt(r, split);
                        foreach (int i in partition)
                        {
                            logger.Log(LogLevel.Debug, $"Added to cost pool {i} {item.id} with total value {i * weights_base[(int)item.id]}");
                            CostPoolItems.Add(new PoolItem { id = item.id, qty = i, value = i * weights_base[(int)item.id] });
                        }
                    }
                }
                //actually make balances
            }

            double gains_total = GainPoolItems.Sum(g => g.value);
            double costs_total = CostPoolItems.Sum(c => c.value);

            logger.Log(LogLevel.Debug, $"Total costs: {costs_total} Total gains: {gains_total}");
        }

        public static List<int> RandomPartitionInt(int x, int y)
        {
            if (y <= 0) throw new ArgumentException("y must be positive.", nameof(y));
            if (x < y) throw new ArgumentException("x must be >= y so each summand is at least 1.", nameof(x));

            var rng = RandomProvider.Instance;

            // Produce lower-variance partitions by starting with the even split (floor division)
            // and distributing the remainder (+1) to random indices. This makes outcomes like
            // 3,3,4 far more likely than extreme splits such as 1,1,8.
            int baseVal = x / y;
            int remainder = x - baseVal * y; // x % y

            var result = Enumerable.Repeat(baseVal, y).ToList();
            if (remainder > 0)
            {
                // choose 'remainder' distinct indices to increment by 1
                var indices = Enumerable.Range(0, y).OrderBy(i => rng.Next()).Take(remainder);
                foreach (int idx in indices)
                    result[idx] += 1;
            }

            // Increase variance a bit by performing a few random transfers.
            // Each transfer moves 1..maxMove units from a randomly chosen donor (with at least 2)
            // to a randomly chosen recipient. This allows some parts to grow/shrink beyond
            // the minimal remainder distribution while keeping most partitions fairly even.
            int transfers = Math.Max(1, Math.Min(x / 5, y / 2)); // scale transfers with x and y
            for (int t = 0; t < transfers; t++)
            {
                // pick donor with value > 1
                var donors = result.Select((val, idx) => (val, idx)).Where(p => p.val > 1).ToList();
                if (donors.Count == 0) break;
                var donor = donors[rng.Next(donors.Count)].idx;

                int recipient = donor;
                // pick a distinct recipient
                if (y > 1)
                {
                    while (recipient == donor)
                        recipient = rng.Next(0, y);
                }

                int maxMove = Math.Min( Math.Max(1, result[donor] - 1), 3); // move at most 3 to avoid extremes
                int move = rng.Next(1, maxMove + 1);
                result[donor] -= move;
                result[recipient] += move;
            }

            return result;
        }


        public List<PoolItem> GetGainPool()
        {
            return GainPoolItems;
        }
        public List<PoolItem> GetCostPool()
        {
            return CostPoolItems;
        }
    }
}
