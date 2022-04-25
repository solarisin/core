namespace Solarisin.Core.Extensions;

/// <summary>
/// Class extensions for <see cref="Random"/>
/// </summary>
public static class RandomExtensions
{
    /// <summary>
    /// Randomly shuffles the elements of the given array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="rng">The extended random number generator.</param>
    /// <param name="array">The array to shuffle.</param>
    public static void Shuffle<T>(this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}
