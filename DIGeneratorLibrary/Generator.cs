

namespace DIGeneratorLibrary
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using Microsoft.Extensions.Logging;

    

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
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

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
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
        private static double[] weights_base = { 2.5, 1, 1.8, 2.0, 1.25, 1.5, 0.1, 0.5, 1.0, 0.5, 1.5, 2, 1.5, 1, 1.5, 1, 2, 8, 11, 1, 2, 2, 3, 2, 1.5, 1.5, 4.5, -0.5, -0.5, 2, 1, 2, 2.5, 2, 4, 8, 1, 1.5, 4.5 };
        /*double[] minimums = { -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, 1, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0 };
        double[] maximums = { 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0 };
        Random rand = RandomProvider.Instance;*/

        public static void Generate(Version version, ILogger logger)
        {
            BoardSpaces spaces = BoardSpaces.LoadFromJSON("spaces.json", logger);
            
            spaces.Filter(version);

            spaces.Generate(version, logger);



            /*string reason = "";
            spaces.Validate(logger, out reason);
            logger.Log(LogLevel.Error, reason);*/
            spaces.PrintInformation(logger);
            spaces.SaveToJSON($"spaces_{version}.json", logger);
        }

        public static void BuildConfig(ILogger logger)
        {
            GeneratorConfig config = new GeneratorConfig();
            config.Construct();
            try
            {
                config.SaveToJson("config.json");
            }
            catch(Exception e)
            {
                logger.Log(LogLevel.Error, "Failed to save config to JSON. Make sure the program has permission to write to the location and that there is enough disk space. Exception message: " + e.Message);
            }
        }

        public static void BalanceConfig(ILogger logger)
        {
            //GeneratorConfig config3 = new GeneratorConfig();
            //config3.Construct();
            //config3.SaveToJson($"config_balanced.json");
            GeneratorConfig config3 = GeneratorConfig.LoadFromJson($"config_balanced.json");
            for (int i = 0; i < 100; i++)
            {
                //GeneratorConfig config = GeneratorConfig.LoadFromJson($"config_balanced.json");
                GeneratorConfig config2 = GeneratorConfig.OptimizeForMinimumImbalance(weights_base, logger, config3.configurations, Version.Base);
                GeneratorConfig.Compare(config3, config2, logger);
                config3 = config2;
                //config2.SaveToJson($"config_balanced.json");
                //
            }

            for (int i = 0; i < 100; i++)
            {
                //GeneratorConfig config = GeneratorConfig.LoadFromJson($"config_balanced.json");
                GeneratorConfig config2 = GeneratorConfig.OptimizeForMinimumImbalance(weights_base, logger, config3.configurations, Version.Ix);
                GeneratorConfig.Compare(config3, config2, logger);
                config3 = config2;
                //config2.SaveToJson($"config_balanced.json");
                //
            }

            for (int i = 0; i < 100; i++)
            {
                //GeneratorConfig config = GeneratorConfig.LoadFromJson($"config_balanced.json");
                GeneratorConfig config2 = GeneratorConfig.OptimizeForMinimumImbalance(weights_base, logger, config3.configurations, Version.IxImmo);
                GeneratorConfig.Compare(config3, config2, logger);
                config3 = config2;
                //config2.SaveToJson($"config_balanced.json");
                //
            }

            for (int i = 0; i < 100; i++)
            {
                //GeneratorConfig config = GeneratorConfig.LoadFromJson($"config_balanced.json");
                GeneratorConfig config2 = GeneratorConfig.OptimizeForMinimumImbalance(weights_base, logger, config3.configurations, Version.Uprising);
                GeneratorConfig.Compare(config3, config2, logger);
                config3 = config2;
                //config2.SaveToJson($"config_balanced.json");
                //
            }
            config3.SaveToJson($"config_balanced.json");
            //logger.LogInformation("Hello");
        }


        public static void BuildSpaces(ILogger logger)
        {
            SpaceBuilder.BuildSpaces(logger);
        }

        /// <summary>
        /// Optimizes the configuration to minimize space imbalance and saves the result.
        /// </summary>
        public static void OptimizeConfig(ILogger logger)
        {
            var weights_base = new double[] { 2.6, 1, 1.57, 1.52, 1.3, 1.6, 0.1, 0.3, 1.1, 0.7, 1.6, 2, 1.57, 0.73, 1.6, 0.6, 2.14, 8, 11, 0.95, 2.3, 2.2, 3.3, 1.3, 1.6, 1.51, 4.41, -0.73, 0.05, 1.6, 4.85, 2.1, 0.75, 2.17, 3.14, 6.18, 0, 1.6, 4.59 };

            var settings = new ConfigOptimizer.OptimizationSettings
            {
                AdjustmentStep = 1,
                MaxAdjustmentRange = 2,
                EvaluationRuns = 3,
                TargetVersion = Version.Base,
                OnlyOptimizeActiveItems = true,
                FocusOnHighImpactItems = true
            };

            logger.LogInformation("Starting config optimization...");
            var optimizedConfig = GeneratorConfig.OptimizeForMinimumImbalance(weights_base, logger, settings);

            try
            {
                optimizedConfig.SaveToJson("config_optimized.json");
                logger.LogInformation("Optimized config saved to config_optimized.json");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to save optimized config");
            }
        }

    }


}
