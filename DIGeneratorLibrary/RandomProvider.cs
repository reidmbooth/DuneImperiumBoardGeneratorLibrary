namespace DIGeneratorLibrary
{
    /// <summary>
    /// Provides a single shared Random instance for the process to avoid
    /// creating many Random() instances with correlated seeds.
    /// </summary>
    public static class RandomProvider
    {
        // Use thread-safe shared Random instance available on modern runtimes
        public static readonly System.Random Instance = System.Random.Shared;
    }
}
