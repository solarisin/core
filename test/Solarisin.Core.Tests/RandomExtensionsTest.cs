using System.Linq;
using Solarisin.Core.Extensions;
using Xunit.Abstractions;

namespace Solarisin.Core.Tests;

public class RandomExtensionsTest
{
    private readonly ITestOutputHelper _output;

    public RandomExtensionsTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestShuffle()
    {
        Random r = new();
        var originalArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        _output.WriteLine($"original array: {{{string.Join(",", originalArray)}}}");

        // copy the original array
        var shuffledArray1 = originalArray.ToArray();

        // Shuffle and check that it's different
        r.Shuffle(shuffledArray1);
        _output.WriteLine($"shuffled array 1: {{{string.Join(",", shuffledArray1)}}}");
        Assert.NotEqual(originalArray, shuffledArray1);

        // copy the shuffled array
        var shuffledArray2 = shuffledArray1.ToArray();

        // Shuffle again and check that it's still different
        r.Shuffle(shuffledArray2);
        _output.WriteLine($"shuffled array 2: {{{string.Join(",", shuffledArray2)}}}");
        Assert.NotEqual(shuffledArray1, shuffledArray2);
    }
}