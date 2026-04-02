using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace DIGeneratorLibrary
{
    public struct PoolItem
    {
        public ID id;
        public int qty;
        public double value;
    }
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

        public void BuildPool()
        {
            Random rand = new Random();
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
                .Where(i => i.id != ID.Oncegame))
            {
                //int gainqty = 0;
                if (item.maximum_gain > 0)
                {
                    int r = rand.Next(item.minimum_gain, item.maximum_gain + 1);
                    Console.WriteLine($"{r} {item.id} rolled");
                    if (r > 0)
                    {
                        int split = Math.Min(rand.Next(item.minimum_gain_split, item.maximum_gain_split + 1), r);
                        List<int> partition = RandomPartitionInt(r, split, rand);
                        foreach (int i in partition)
                        {
                            Console.WriteLine($"Added to gain pool {i} {item.id} with total value {i * weights_base[(int)item.id]}");
                            GainPoolItems.Add(new PoolItem { id = item.id, qty = i, value = i * weights_base[(int)item.id] });
                        }
                    }
                    /*for(int i = 0; i < r; i++)
                        GainPoolItems.Add(new PoolItem { id = item.id, qty = 1, value = 1 * weights_base[(int)item.id] });*/

                    //Console.WriteLine($"Gain {item.id} qty {r} split {split} ways");
                    //GainPoolItems.Add(new PoolItem { id = item.id, qty = r, value = r * weights_base[(int)item.id] });
                    //Console.WriteLine($"BuildPool {item.id} {r} {r * weights_base[(int)item.id]}");
                }
                if (item.maximum_cost > 0)
                {
                    /*if (item.maximum_cost_split == 1)
                    {*/
                    int r = rand.Next(item.minimum_cost, item.maximum_cost + 1);
                    Console.WriteLine($"{r} {item.id} rolled");
                    if (r > 0)
                    {
                        int split = Math.Min(rand.Next(item.minimum_cost_split, item.maximum_cost_split + 1), r);
                        List<int> partition = RandomPartitionInt(r, split, rand);
                        foreach (int i in partition)
                        {
                            Console.WriteLine($"Added to cost pool {i} {item.id} with total value {i * weights_base[(int)item.id]}");
                            CostPoolItems.Add(new PoolItem { id = item.id, qty = i, value = i * weights_base[(int)item.id] });
                        }
                    }
                    /*for (int i = 0; i < r; i++)
                        CostPoolItems.Add(new PoolItem { id = item.id, qty = 1, value = 1 * weights_base[(int)item.id] });*/

                    //Console.WriteLine($"Cost {item.id} qty {r} split {split} ways");
                    //CostPoolItems.Add(new PoolItem { id = item.id, qty = r, value = r * weights_base[(int)item.id] });
                    //Console.WriteLine($"BuildPool {item.id} {r} {r * weights_base[(int)item.id]}");
                    /*}
                    //gainqty = rand.Next(item.minimum_gain, item.maximum_gain+1);
                    if (item.maximum_gain_split > 0)
                    {

                    }*/
                }
                //actually make balances
            }

            double gains_total = 0;
            double costs_total = 0;
            foreach (PoolItem p in GainPoolItems)
            {
                gains_total += p.value;
            }
            foreach (PoolItem p in CostPoolItems)
            {
                costs_total += p.value;
            }
            Console.WriteLine($"Total costs: {costs_total} Total gains: {gains_total}");
        }

        public static List<int> RandomPartitionInt(int x, int y, Random _rng)
        {
            if (y <= 0) throw new ArgumentException("y must be positive.", nameof(y));
            if (x < y) throw new ArgumentException("x must be >= y so each summand is at least 1.", nameof(x));

            // Produce lower-variance partitions by starting with the even split (floor division)
            // and distributing the remainder (+1) to random indices. This makes outcomes like
            // 3,3,4 far more likely than extreme splits such as 1,1,8.
            int baseVal = x / y;
            int remainder = x - baseVal * y; // x % y

            var result = Enumerable.Repeat(baseVal, y).ToList();
            if (remainder > 0)
            {
                // choose 'remainder' distinct indices to increment by 1
                var indices = Enumerable.Range(0, y).OrderBy(i => _rng.Next()).Take(remainder);
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
                var donor = donors[_rng.Next(donors.Count)].idx;

                int recipient = donor;
                // pick a distinct recipient
                if (y > 1)
                {
                    while (recipient == donor)
                        recipient = _rng.Next(0, y);
                }

                int maxMove = Math.Min( Math.Max(1, result[donor] - 1), 3); // move at most 3 to avoid extremes
                int move = _rng.Next(1, maxMove + 1);
                result[donor] -= move;
                result[recipient] += move;
            }

            return result;
        }

        /*public static List<int> RandomPartitionInt(int x, int y, Random _rng)
        {
            if (y <= 0) throw new ArgumentException("y must be positive.", nameof(y));
            if (x < y) throw new ArgumentException("x must be >= y so each summand is at least 1.", nameof(x));

            // Sample y-1 unique cut points from [1, x-1]
            var cuts = new SortedSet<int>();
            while (cuts.Count < y - 1)
                cuts.Add(_rng.Next(1, x));
            // Produce lower-variance partitions by starting with the even split (floor division)
            // and distributing the remainder (+1) to random indices. This makes outcomes like
            // 3,3,4 far more likely than extreme splits such as 1,1,8.
            int baseVal = x / y;
            int remainder = x - baseVal * y; // x % y

            var points = new List<int> { 0 };
            points.AddRange(cuts);
            points.Add(x);

            var result = new List<int>(y);
            for (int i = 0; i < y; i++)
                result.Add(points[i + 1] - points[i]);
            var result = Enumerable.Repeat(baseVal, y).ToList();
            if (remainder > 0)
            {
                // choose 'remainder' distinct indices to increment by 1
                var indices = Enumerable.Range(0, y).OrderBy(i => _rng.Next()).Take(remainder);
                foreach (int idx in indices)
                    result[idx] += 1;
            }

            return result;
        }*/

        /*public static List<int> Split(int n, int split, Random rand)
        {
            List<int> splitter = new List<int>();
            while (splitter.Count < split-1)
            {
                int change = rand.Next(n - split) + 1;
                splitter.Add(change);
                n -= change;
            }
            return splitter;
        }*/

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
