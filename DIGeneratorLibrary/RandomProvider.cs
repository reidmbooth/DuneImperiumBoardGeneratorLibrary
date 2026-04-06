namespace DIGeneratorLibrary
{
    /// <summary>
    /// Provides a single shared Random instance for the process to avoid
    /// creating many Random() instances with correlated seeds.
    /// </summary>
    public static class RandomProvider
    {
        public static readonly System.Random Instance = new System.Random();
    }
}
