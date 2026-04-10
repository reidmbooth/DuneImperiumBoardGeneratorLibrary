

using Microsoft.Extensions.Logging;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger<Program>();

Console.WriteLine("Please input option.  1 = Generate Config file (do this first if first run), 2 = Generate Spaces based on config file");

int option = Console.Read();

switch (option){
    case '1':
        DIGeneratorLibrary.Generator.BuildConfig(logger);
        break;
    case '2':
        DIGeneratorLibrary.Generator.BuildSpaces(logger);
        //DIGeneratorLibrary.Generator.Generate(DIGeneratorLibrary.Version.Base, logger);
        DIGeneratorLibrary.Generator.Generate(DIGeneratorLibrary.Version.Base, logger);
        break;
    case '3':
        DIGeneratorLibrary.Generator.BalanceConfig(logger);
        break;
}

