using System.ComponentModel;
using Solarisin.Core.Extensions;

namespace Solarisin.Core.Tests;

public class EnumExtensionsTest
{
    [Theory]
    [InlineData(TestEnumeration.NoDescription, "NoDescription")]
    [InlineData(TestEnumeration.ShortDescription, "TheShortDescription")]
    [InlineData(TestEnumeration.LongDescription, "An Extremely Long Description That Goes On And On And On")]
    public void TestGetDescription(Enum value, string expected)
    {
        Assert.Equal(expected, value.GetDescription());
    }

    [Theory]
    [InlineData(true, (TestEnumeration)123)]
    [InlineData(false, TestEnumeration.NoDescription)]
    public void TestGetDescriptionException(bool shouldThrow, Enum value)
    {
        if (shouldThrow)
            Assert.Throws<InvalidOperationException>(
                value.GetDescription
            );
        else
            Assert.Equal("NoDescription", value.GetDescription());
    }

    private enum TestEnumeration
    {
        NoDescription,
        [Description("TheShortDescription")] ShortDescription,

        [Description("An Extremely Long Description That Goes On And On And On")]
        LongDescription
    }
}