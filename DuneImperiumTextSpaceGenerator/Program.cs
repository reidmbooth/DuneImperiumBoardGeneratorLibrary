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

DIGeneratorLibrary.Generator.BuildSpaces();
DIGeneratorLibrary.Generator.Generate(DIGeneratorLibrary.Version.Base);