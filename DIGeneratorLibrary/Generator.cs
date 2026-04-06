

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
        private double[] weights_base = { 2.5, 1, 1.8, 2.0, 1.25, 1.5, 0.1, 0.5, 1.0, 0.5, 1.5, 2, 1.5, 1, 1.5, 1, 2, 8, 11, 1, 2, 2, 3, 2, 1.5, 1.5, 4.5, -0.5, -0.5, 2, 1, 2, 2.5, 2, 4, 8, 1, 1.5, 4.5 };
        double[] minimums = { -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, 1, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0, -5.0 };
        double[] maximums = { 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0, 25.0 };
        Random rand = RandomProvider.Instance;

        public static void Generate(Version version, ILogger logger)
        {
            BoardSpaces spaces = BoardSpaces.LoadFromJSON("spaces.json");
            spaces.Filter(version);

            spaces.Generate(version, logger);



            string reason = "";
            spaces.Validate(logger, out reason);
            logger.Log(LogLevel.Error, reason);
            spaces.PrintInformation();
            spaces.SaveToJSON("spaces_" + version.ToString() + ".json");
        }

        public static void BuildConfig(ILogger logger)
        {
            GeneratorConfig config = new GeneratorConfig();
            config.Construct();
            config.SaveToJson("config.json");
        }


        public static void BuildSpaces(ILogger logger)
        {
            SpaceBuilder.BuildSpaces(logger);
        }


    }

    
}
