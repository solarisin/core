using System.ComponentModel;
using Solarisin.Core.Extensions;

namespace Solarisin.Core.Tests;

public class ListExtensionsTest
{
    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 0, 1)]
    [InlineData(new[] { 1, 2, 3 }, 1, 2)]
    [InlineData(new[] { 1, 2, 3 }, 2, 3)]
    [InlineData(new[] { 1,2,3,4,5,6,7,8,9,0 }, 6, 7)]
    [InlineData(new[] { 1, 2, 3 }, 7, int.MinValue)]
    public void TestPopAtInt(int[] array, int index, int expected)
    {
        var list = new List<int>(array);
        if (expected == int.MinValue)
            Assert.Throws<ArgumentOutOfRangeException>(() => list.PopAt(index));
        else
            Assert.Equal(expected, list.PopAt(index));
    }
    
    [Theory]
    [InlineData(new[] { "abc", "def", "ghi" }, 0, "abc")]
    [InlineData(new[] { "abc", "def", "ghi" }, 1, "def")]
    [InlineData(new[] { "abc", "def", "ghi" }, 2, "ghi")]
    [InlineData(new[] { "a","b","c","d","e","f","g","h","i" }, 7, "h")]
    public void TestPopAtString(string[] array, int index, string expected)
    {
        var list = new List<string>(array);
        Assert.Equal(expected, list.PopAt(index));
    }   
    
    [Theory]
    [InlineData(new[] { 32, 77, 840 }, 32)]
    [InlineData(new[] { 5, 3, 8, 11, 60, 88, 79 }, 5)]
    [InlineData(new[] { 900, 34, 11, 42 }, 900)]
    [InlineData(new[] { 20, 34, 11, 42 }, 20)]
    [InlineData(new int[] { }, null)]
    public void TestPopFirstNoPredicate(int[] array, int? expected)
    {
        var list = new List<int>(array);
        if (expected == null)
            Assert.Throws<ArgumentOutOfRangeException>(() => list.PopFirst());
        else
            Assert.Equal(expected, list.PopFirst());
    }
    
    [Theory]
    [InlineData(new[] { 32, 77, 840 }, 77)]
    [InlineData(new[] { 5, 3, 8, 11, 60, 88, 79 }, 60)]
    [InlineData(new[] { 900, 34, 11, 42 }, 900)]
    [InlineData(new[] { 20, 34, 11, 42 }, null)]
    public void TestPopFirstInt(int[] array, int? expected)
    {
        var list = new List<int>(array);
        if (expected == null)
            Assert.Throws<ArgumentOutOfRangeException>(() => list.PopFirst(x => x > 50));
        else
            Assert.Equal(expected, list.PopFirst(x => x > 50));
    }
    
    [Theory]
    [InlineData(new[] { "a", "abcfd", "ddd", "abacalkd" }, "abacalkd")]
    [InlineData(new[] { "test", "testtest", "aa", "dd" }, "testtest")]
    [InlineData(new[] { "cool", "test", "aa", "dd" }, null)]
    public void TestPopFirstString(string[] array, string? expected)
    {     
        var list = new List<string>(array);
        if (expected == null)
            Assert.Throws<ArgumentOutOfRangeException>(() => list.PopFirst(x=>x.Length>5));
        else
            Assert.Equal(expected, list.PopFirst(x=>x.Length>5));
    }
    
    [Theory]
    [InlineData(new[] { "a", "abcfd", "ddd", "abacalkd" }, "abacalkd")]
    [InlineData(new[] { "test", "testtest", "aa", "dd" }, "testtest")]
    [InlineData(new[] { "hellothere" }, "hellothere")]
    [InlineData(new[] { "cool", "test", "aa", "dd" }, null)]
    public void TestPopFirstOrDefaultString(string[] array, string? expected)
    {     
        var list = new List<string?>(array);
        Assert.Equal(expected, list.PopFirstOrDefault(x=>x?.Length>5));
    }
    
    [Theory]
    [InlineData(new[] { "a", "abcfd", "ddd", "abacalkd" }, "a")]
    [InlineData(new[] { "test", "testtest", "aa", "dd" }, "test")]
    [InlineData(new[] { "hello" }, "hello")]
    [InlineData(new string[] { }, null)]
    public void TestPopFirstOrDefaultNoPredicate(string[] array, string? expected)
    {
        var list = new List<string?>(array);
        Assert.Equal(expected, list.PopFirstOrDefault());
    }
}