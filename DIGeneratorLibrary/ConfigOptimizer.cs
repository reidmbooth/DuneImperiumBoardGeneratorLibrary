using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DIGeneratorLibrary
{
    /// <summary>
    /// Optimizes GeneratorConfig by iterating parameter variations and evaluating
    /// which configuration produces the lowest average space imbalance.
    /// </summary>
    public class ConfigOptimizer
    {
        private readonly double[] weights_base;
        private readonly ILogger logger;

        /// <summary>
        /// Parameters controlling the optimization search space.
        /// </summary>
        public class OptimizationSettings
        {
            /// <summary>
            /// Amount to adjust each min/max value (e.g., ±2 means try value-2, value, value+2)
            /// </summary>
            public int AdjustmentStep { get; set; } = 1;

            /// <summary>
            /// Maximum number of iterations per parameter sweep
            /// </summary>
            public int MaxAdjustmentRange { get; set; } = 3;

            /// <summary>
            /// Number of random generation runs to average per configuration
            /// </summary>
            public int EvaluationRuns { get; set; } = 5;

            /// <summary>
            /// Version to optimize for
            /// </summary>
            public Version TargetVersion { get; set; } = Version.Base;

            /// <summary>
            /// If true, only optimize IDs with non-zero min/max ranges
            /// </summary>
            public bool OnlyOptimizeActiveItems { get; set; } = true;

            /// <summary>
            /// If true, focus optimization on high-impact IDs (those with non-zero gain/cost)
            /// </summary>
            public bool FocusOnHighImpactItems { get; set; } = true;

            /// <summary>
            /// Optional: Initial item configurations for each version.
            /// If null, uses GeneratorConfig.Construct() to create defaults.
            /// Format: Dictionary<Version, List<Item>>
            /// </summary>
            public Dictionary<Version, List<Item>> InitialConfigurations { get; set; } = null;

            /// <summary>
            /// Optional: List of item IDs to skip during optimization.
            /// Items with IDs in this list will not be adjusted.
            /// </summary>
            public List<ID> IgnoreItemIds { get; set; } = new();

            /// <summary>
            /// Optional: List of item IDs whose cost parameters should NOT be adjusted.
            /// These items can still have gain parameters adjusted.
            /// </summary>
            public List<ID> IgnoreItemCosts { get; set; } = new();

            /// <summary>
            /// Optional: List of item IDs whose gain parameters should NOT be adjusted.
            /// These items can still have cost parameters adjusted.
            /// </summary>
            public List<ID> IgnoreItemGains { get; set; } = new();
        }

        /// <summary>
        /// Result of a single configuration evaluation.
        /// </summary>
        public class EvaluationResult
        {
            public GeneratorConfig Config { get; set; }
            public double AverageImbalance { get; set; }
            public double StdDevImbalance { get; set; }
            public List<double> PerRunImbalances { get; set; }
            public TimeSpan EvaluationTime { get; set; }
            public Dictionary<string, object> Metadata { get; set; } = new();
        }

        public ConfigOptimizer(double[] weights_base, ILogger logger)
        {
            this.weights_base = weights_base;
            this.logger = logger;
        }

        /// <summary>
        /// Optimizes the config by performing a parameter sweep and evaluating results.
        /// </summary>
        public EvaluationResult OptimizeConfig(OptimizationSettings settings = null)
        {
            settings ??= new OptimizationSettings();

            logger.LogInformation("Starting config optimization for version {Version}", settings.TargetVersion);
            logger.LogInformation("Settings: AdjustmentStep={Step}, MaxRange={Range}, EvalRuns={Runs}",
                settings.AdjustmentStep, settings.MaxAdjustmentRange, settings.EvaluationRuns);

            var overallStopwatch = Stopwatch.StartNew();
            var baselineConfig = new GeneratorConfig();

            // Use provided configurations or construct defaults
            if (settings.InitialConfigurations != null)
            {
                logger.LogInformation("Using provided initial configurations");
                // Set each version's items
                foreach (var kvp in settings.InitialConfigurations)
                {
                    baselineConfig.SetItems(kvp.Key, new List<Item>(kvp.Value));
                }
            }
            else
            {
                logger.LogInformation("Using default configurations from Construct()");
                baselineConfig.Construct();
            }

            // Evaluate baseline
            logger.LogInformation("Evaluating baseline configuration...");
            var baselineResult = EvaluateConfig(baselineConfig, settings);
            logger.LogInformation("Baseline imbalance: {Imbalance:F4} (±{StdDev:F4})", 
                baselineResult.AverageImbalance, baselineResult.StdDevImbalance);

            EvaluationResult bestResult = baselineResult;
            var allResults = new List<EvaluationResult> { baselineResult };

            // Get items to optimize
            var baseItems = baselineConfig.GetItems(settings.TargetVersion);
            var itemsToOptimize = GetItemsToOptimize(baseItems, settings);

            logger.LogInformation("Optimizing {Count} items", itemsToOptimize.Count);

            // Perform parameter sweep: try adjusting each item's parameters
            int evaluationCount = 0;
            int totalEvaluations = itemsToOptimize.Count * 8 * (2 * settings.MaxAdjustmentRange + 1); // rough estimate (8 parameters: min/max cost/gain + min/max cost/gain splits)

            foreach (var item in itemsToOptimize)
            {
                logger.LogDebug("Optimizing {ItemId}", item.id);

                // Try adjusting minimum_cost (skip if item is in IgnoreItemCosts)
                if (!settings.IgnoreItemCosts.Contains(item.id))
                {
                    var costAdjustments = TryAdjustParameter(baselineConfig, item, 
                        (i, val) => i.minimum_cost = Math.Min(val, i.maximum_cost), 
                        item.minimum_cost, settings, ref evaluationCount, totalEvaluations);
                    if (costAdjustments.Item1 != null && costAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = costAdjustments.Item1;
                        baselineConfig = costAdjustments.Item1.Config;
                    }
                    allResults.Add(costAdjustments.Item1);
                }

                // Try adjusting maximum_cost (skip if item is in IgnoreItemCosts)
                if (!settings.IgnoreItemCosts.Contains(item.id))
                {
                    var maxCostAdjustments = TryAdjustParameter(baselineConfig, item,
                        (i, val) => i.maximum_cost = Math.Max(i.minimum_cost, val),
                        item.maximum_cost, settings, ref evaluationCount, totalEvaluations);
                    if (maxCostAdjustments.Item1 != null && maxCostAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = maxCostAdjustments.Item1;
                        baselineConfig = maxCostAdjustments.Item1.Config;
                    }
                    allResults.Add(maxCostAdjustments.Item1);
                }

                // Try adjusting minimum_gain (skip if item is in IgnoreItemGains)
                if (!settings.IgnoreItemGains.Contains(item.id))
                {
                    var gainAdjustments = TryAdjustParameter(baselineConfig, item,
                        (i, val) => i.minimum_gain = Math.Min(val, i.maximum_gain),
                        item.minimum_gain, settings, ref evaluationCount, totalEvaluations);
                    if (gainAdjustments.Item1 != null && gainAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = gainAdjustments.Item1;
                        baselineConfig = gainAdjustments.Item1.Config;
                    }
                    allResults.Add(gainAdjustments.Item1);
                }

                // Try adjusting maximum_gain (skip if item is in IgnoreItemGains)
                if (!settings.IgnoreItemGains.Contains(item.id))
                {
                    var maxGainAdjustments = TryAdjustParameter(baselineConfig, item,
                        (i, val) => i.maximum_gain = Math.Max(i.minimum_gain, val),
                        item.maximum_gain, settings, ref evaluationCount, totalEvaluations);
                    if (maxGainAdjustments.Item1 != null && maxGainAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = maxGainAdjustments.Item1;
                        baselineConfig = maxGainAdjustments.Item1.Config;
                    }
                    allResults.Add(maxGainAdjustments.Item1);
                }

                // Try adjusting minimum_cost_split (skip if item is in IgnoreItemCosts)
                if (!settings.IgnoreItemCosts.Contains(item.id))
                {
                    var minCostSplitAdjustments = TryAdjustParameter(baselineConfig, item,
                        (i, val) => i.minimum_cost_split = Math.Min(val, i.maximum_cost_split),
                        item.minimum_cost_split, settings, ref evaluationCount, totalEvaluations);
                    if (minCostSplitAdjustments.Item1 != null && minCostSplitAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = minCostSplitAdjustments.Item1;
                        baselineConfig = minCostSplitAdjustments.Item1.Config;
                    }
                    allResults.Add(minCostSplitAdjustments.Item1);
                }

                // Try adjusting maximum_cost_split (skip if item is in IgnoreItemCosts)
                if (!settings.IgnoreItemCosts.Contains(item.id))
                {
                    var maxCostSplitAdjustments = TryAdjustParameter(baselineConfig, item,
                        (i, val) => i.maximum_cost_split = Math.Max(i.minimum_cost_split, val),
                        item.maximum_cost_split, settings, ref evaluationCount, totalEvaluations);
                    if (maxCostSplitAdjustments.Item1 != null && maxCostSplitAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = maxCostSplitAdjustments.Item1;
                        baselineConfig = maxCostSplitAdjustments.Item1.Config;
                    }
                    allResults.Add(maxCostSplitAdjustments.Item1);
                }

                // Try adjusting minimum_gain_split (skip if item is in IgnoreItemGains)
                if (!settings.IgnoreItemGains.Contains(item.id))
                {
                    var minGainSplitAdjustments = TryAdjustParameter(baselineConfig, item,
                        (i, val) => i.minimum_gain_split = Math.Min(val, i.maximum_gain_split),
                        item.minimum_gain_split, settings, ref evaluationCount, totalEvaluations);
                    if (minGainSplitAdjustments.Item1 != null && minGainSplitAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = minGainSplitAdjustments.Item1;
                        baselineConfig = minGainSplitAdjustments.Item1.Config;
                    }
                    allResults.Add(minGainSplitAdjustments.Item1);
                }

                // Try adjusting maximum_gain_split (skip if item is in IgnoreItemGains)
                if (!settings.IgnoreItemGains.Contains(item.id))
                {
                    var maxGainSplitAdjustments = TryAdjustParameter(baselineConfig, item,
                        (i, val) => i.maximum_gain_split = Math.Max(i.minimum_gain_split, val),
                        item.maximum_gain_split, settings, ref evaluationCount, totalEvaluations);
                    if (maxGainSplitAdjustments.Item1 != null && maxGainSplitAdjustments.Item1.AverageImbalance < bestResult.AverageImbalance)
                    {
                        bestResult = maxGainSplitAdjustments.Item1;
                        baselineConfig = maxGainSplitAdjustments.Item1.Config;
                    }
                    allResults.Add(maxGainSplitAdjustments.Item1);
                }
            }

            overallStopwatch.Stop();

            logger.LogInformation("Optimization complete in {Time:F2} seconds", overallStopwatch.Elapsed.TotalSeconds);
            logger.LogInformation("Best imbalance found: {Imbalance:F4} (improvement: {Improvement:F4})",
                bestResult.AverageImbalance,
                baselineResult.AverageImbalance - bestResult.AverageImbalance);
            logger.LogInformation("Evaluated {Count} configurations", allResults.Count);

            // Store optimization metadata
            bestResult.Metadata["BaselineImbalance"] = baselineResult.AverageImbalance;
            bestResult.Metadata["TotalConfiguationsEvaluated"] = allResults.Count;
            bestResult.Metadata["OptimizationTime"] = overallStopwatch.Elapsed.TotalSeconds;

            return bestResult;
        }

        /// <summary>
        /// Evaluates a configuration by running multiple generation attempts and calculating
        /// average imbalance across all resulting spaces.
        /// </summary>
        private EvaluationResult EvaluateConfig(GeneratorConfig config, OptimizationSettings settings)
        {
            var imbalances = new List<double>();
            var stopwatch = Stopwatch.StartNew();

            // Validate config items before evaluation
            var items = config.GetItems(settings.TargetVersion);
            ValidateAndFixItems(items);

            for (int run = 0; run < settings.EvaluationRuns; run++)
            {
                var boardSpaces = new BoardSpaces();
                boardSpaces.SetSpaces(GenerateInitialSpaces());

                try
                {
                    var itemPoolBuilder = new ItemPoolBuilder(config, weights_base, settings.TargetVersion);
                    itemPoolBuilder.BuildPool(logger);

                    var spaceAssembler = new SpaceAssembler(
                        itemPoolBuilder.GetGainPool(),
                        itemPoolBuilder.GetCostPool(),
                        boardSpaces.GetSpaces(),
                        weights_base);
                    spaceAssembler.Assemble(logger);

                    // Generate each space to balance it
                    foreach (var space in boardSpaces.GetSpaces())
                    {
                        var adjustableIds = new List<ID>();
                        foreach (ID id in Enum.GetValues<ID>())
                        {
                            adjustableIds.Add(id);
                        }
                        space.Generate(weights_base, 0.5, adjustableIds, adjustableIds, logger);
                    }

                    // Calculate average imbalance
                    double totalImbalance = 0;
                    foreach (var space in boardSpaces.GetSpaces())
                    {
                        totalImbalance += Math.Abs(space.Balance(weights_base));
                    }
                    double averageImbalance = totalImbalance / boardSpaces.GetSpaces().Count;
                    imbalances.Add(averageImbalance);
                }
                catch (Exception ex)
                {
                    logger.LogWarning("Error during evaluation run {Run}: {Message}", run, ex.Message);
                    imbalances.Add(double.MaxValue); // Penalize failed runs
                }
            }

            stopwatch.Stop();

            double mean = imbalances.Average();
            double stdDev = Math.Sqrt(imbalances.Sum(x => Math.Pow(x - mean, 2)) / imbalances.Count);

            return new EvaluationResult
            {
                Config = config,
                AverageImbalance = mean,
                StdDevImbalance = stdDev,
                PerRunImbalances = imbalances,
                EvaluationTime = stopwatch.Elapsed
            };
        }

        /// <summary>
        /// Validates and fixes item configurations to ensure split counts are valid.
        /// Only fixes splits if they're truly invalid (e.g., needed for partitioning but set to 0).
        /// </summary>
        private void ValidateAndFixItems(List<Item> items)
        {
            foreach (var item in items)
            {
                // Only fix splits if the range exists but splits are 0
                // If both are 0, don't force a split to be created

                // For gain: only fix if there's a range (max > min) and no splits
                if (item.maximum_gain > item.minimum_gain)
                {
                    if (item.maximum_gain_split == 0)
                    {
                        item.maximum_gain_split = 1;
                    }
                    if (item.minimum_gain_split == 0)
                    {
                        item.minimum_gain_split = 1;
                    }
                }

                // For cost: only fix if there's a range (max > min) and no splits
                if (item.maximum_cost > item.minimum_cost)
                {
                    if (item.maximum_cost_split == 0)
                    {
                        item.maximum_cost_split = 1;
                    }
                    if (item.minimum_cost_split == 0)
                    {
                        item.minimum_cost_split = 1;
                    }
                }

                // Ensure minimum <= maximum for splits
                if (item.minimum_gain_split > item.maximum_gain_split)
                {
                    item.minimum_gain_split = item.maximum_gain_split;
                }
                if (item.minimum_cost_split > item.maximum_cost_split)
                {
                    item.minimum_cost_split = item.maximum_cost_split;
                }
            }
        }

        /// <summary>
        /// Tries adjusting a parameter and returns the best configuration found.
        /// Clones the baseline config to preserve optimizations from previous parameter adjustments.
        /// </summary>
        private (EvaluationResult, int) TryAdjustParameter(
            GeneratorConfig baselineConfig,
            Item baselineItem,
            Action<Item, int> setter,
            int baselineValue,
            OptimizationSettings settings,
            ref int evaluationCount,
            int totalEvaluations)
        {
            EvaluationResult bestResult = null;

            for (int adjustment = -settings.MaxAdjustmentRange; adjustment <= settings.MaxAdjustmentRange; adjustment++)
            {
                int newValue = baselineValue + (adjustment * settings.AdjustmentStep);
                if (newValue < 0) continue; // Skip invalid values

                // Clone baseline config to preserve previously optimized parameters
                var testConfig = baselineConfig.DeepClone();
                var testItems = testConfig.GetItems(settings.TargetVersion);
                var testItem = testItems.First(i => i.id == baselineItem.id);

                setter(testItem, newValue);

                evaluationCount++;
                double progress = (evaluationCount / (double)totalEvaluations) * 100;
                logger.LogDebug("Evaluating config [{Progress:F1}%] {ItemId} = {Value}",
                    progress, baselineItem.id, newValue);

                var result = EvaluateConfig(testConfig, settings);

                if (bestResult == null)
                {
                    bestResult = result;
                    logger.LogDebug("  {ItemId} = {Value}: imbalance = {Imbalance:F4} (baseline)", 
                        baselineItem.id, newValue, result.AverageImbalance);
                }
                else if (result.AverageImbalance < bestResult.AverageImbalance)
                {
                    logger.LogDebug("  {ItemId} = {Value}: imbalance = {Imbalance:F4} (IMPROVEMENT: {Delta:F4})",
                        baselineItem.id, newValue, result.AverageImbalance, 
                        bestResult.AverageImbalance - result.AverageImbalance);
                    bestResult = result;
                }
                else
                {
                    logger.LogDebug("  {ItemId} = {Value}: imbalance = {Imbalance:F4}",
                        baselineItem.id, newValue, result.AverageImbalance);
                }
            }

            return (bestResult, evaluationCount);
        }

        /// <summary>
        /// Filters items to optimize based on settings.
        /// </summary>
        private List<Item> GetItemsToOptimize(List<Item> items, OptimizationSettings settings)
        {
            var filtered = items;

            // Filter out ignored items
            if (settings.IgnoreItemIds.Count > 0)
            {
                filtered = filtered.Where(i => !settings.IgnoreItemIds.Contains(i.id)).ToList();
            }

            if (settings.OnlyOptimizeActiveItems)
            {
                filtered = filtered.Where(i => i.maximum_cost > 0 || i.maximum_gain > 0).ToList();
            }

            if (settings.FocusOnHighImpactItems)
            {
                // Prioritize items with wider ranges (more flexibility)
                filtered = filtered.OrderByDescending(i => (i.maximum_cost - i.minimum_cost) + (i.maximum_gain - i.minimum_gain))
                    .ToList();
            }

            return filtered;
        }

        /// <summary>
        /// Generates initial space list (for evaluation purposes).
        /// </summary>
        private List<Space> GenerateInitialSpaces()
        {
            var spaces = new List<Space>();
            
            // This is a minimal set - in a real scenario, you'd load from SpaceBuilder
            spaces.Add(new Space { Name = "Space1" });
            spaces.Add(new Space { Name = "Space2" });
            spaces.Add(new Space { Name = "Space3" });
            spaces.Add(new Space { Name = "Space4" });
            spaces.Add(new Space { Name = "Space5" });

            return spaces;
        }
    }
}
