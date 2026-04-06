using System;
using System.Collections.Generic;
using System.Linq;

namespace DIGeneratorLibrary
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Returns a new sequence with the elements shuffled using Fisher-Yates.
        /// Uses the shared RandomProvider.Instance.
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var arr = source.ToArray();
            var rnd = RandomProvider.Instance;
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                var tmp = arr[i]; arr[i] = arr[j]; arr[j] = tmp;
            }
            return arr;
        }
    }
}
