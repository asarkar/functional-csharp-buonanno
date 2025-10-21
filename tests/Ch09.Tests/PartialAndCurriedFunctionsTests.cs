// using Ch09;
using static Ch09.PartialAndCurriedFunctions;

namespace Ch09.Tests;

public class PartialAndCurriedFunctionsTests
{
    [Theory]
    [InlineData(4, 3, 1)]
    [InlineData(3, 3, 0)]
    [InlineData(2, 3, 2)]
    [InlineData(1, 3, 1)]
    [InlineData(0, 3, 0)]
    [InlineData(-1, 3, 2)]
    [InlineData(-2, 3, 1)]
    [InlineData(-3, 3, 0)]
    [InlineData(-4, 3, 2)]
    public void Remainder(int x, int y, int expected)
    {
        Assert.Equal(expected, PartialAndCurriedFunctions.Remainder(x, y));
    }

    [Theory]
    [InlineData(6, 1)]
    [InlineData(-1, 4)]
    public void DivideBy5Remainder(int x, int expected)
    {
        Assert.Equal(expected, PartialAndCurriedFunctions.DivideBy5Remainder(x));
    }

    [Fact]
    public void Map_Empty_ReturnsEmpty()
    {
        Assert.Empty(new List<int>().Map(i => i * 2));
    }

    [Fact]
    public void Map_NonEmpty_ReturnsEnumerableWithValuesDoubled()
    {
        Assert.Equal([2, 4, 6], new List<int>() { 1, 2, 3 }.Map(i => i * 2));
    }

    [Fact]
    public void Where_Empty_ReturnsEmpty()
    {
        Assert.Empty(new List<int>().Where(i => i > 0));
    }

    [Fact]
    public void Where_NonEmpty_ReturnsEnumerableWithPositiveNumbers()
    {
        Assert.Equal([1, 2], new List<int>() { -1, 0, 1, 2 }.Where(i => i > 0));
    }

    [Fact]
    public void Bind_Empty_ReturnsEmpty()
    {
        Assert.Empty(new List<int>().Bind<int, int>(i => [i * 2]));
    }

    [Fact]
    public void Bind_NonEmpty_ReturnsEnumerableWithValuesDoubled()
    {
        Assert.Equal([2, 4, 6], new List<int>() { 1, 2, 3 }.Bind<int, int>(i => [i * 2]));
    }
}