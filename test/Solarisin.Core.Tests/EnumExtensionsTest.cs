using System.ComponentModel;
using Solarisin.Core.Extensions;

namespace Solarisin.Core.Tests;

public class EnumExtensionsTest
{
    enum TestEnumeration
    {
        NoDescription,
        [Description("TheShortDescription")]
        ShortDescription,
        [Description("An Extremely Long Description That Goes On And On And On")]
        LongDescription
    }

    [Theory]
    [InlineData(TestEnumeration.NoDescription, "NoDescription")]
    [InlineData(TestEnumeration.ShortDescription, "TheShortDescription")]
    [InlineData(TestEnumeration.LongDescription, "An Extremely Long Description That Goes On And On And On")]
    public void TestGetDescription(Enum value, string expected)
    {
        Assert.Equal(expected, value.GetDescription());
    }
    
    [Fact]
    public void TestGetDescriptionException()
    {
        Assert.Throws<InvalidOperationException>(() => { ((TestEnumeration)123).GetDescription(); });
    }
}