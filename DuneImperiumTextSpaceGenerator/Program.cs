// See https://aka.ms/new-console-template for more information
/*for(int j = 0; j < 100; j++)
{
    int n = 5;
    int split = 3;
    List<int> splitt = DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(n, split, new Random());
    foreach (int i in splitt)
    {
        Console.WriteLine(i);
    }
    Console.WriteLine();
}*/

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
        DIGeneratorLibrary.Generator.Generate(DIGeneratorLibrary.Version.Base, logger);
        break;
}

