
using Ch02;

namespace Ch02.Tests;

public class HigherOrderFunctionsTests
{
    [Fact]
    public void Negate_WhenIsEven2_ReturnsFalse()
    {
        Assert.False(HigherOrderFunctions.Negate<int>(IsEven)(2));
    }

    [Fact]
    public void Negate_WhenIsEven1_ReturnsTrue()
    {
        Assert.True(HigherOrderFunctions.Negate<int>(IsEven)(1));
    }

    private static bool IsEven(int x) =>
        x % 2 == 0;

    [Theory]
    [MemberData(nameof(QsortData))]
    public void Qsort(int[] xs)
    {
        var expected = xs
            .OrderBy(n => n)
            .ToList();

        static int NaturalOrder(int x, int y) => x.CompareTo(y);

        Assert.Equal(expected, HigherOrderFunctions.Qsort(xs.ToList(), NaturalOrder));
    }

    // xUnit1047: Avoid using TheoryDataRow arguments that might not be serializable.
    // https://xunit.net/xunit.analyzers/rules/xUnit1047
    public static IEnumerable<TheoryDataRow<int[]>> QsortData() =>
        [
            new([3, 2, 1]), new([1, 2, 3]), new([1, 1, 1]), new([1]), new([])
        ];
}