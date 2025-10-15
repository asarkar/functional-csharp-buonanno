namespace Ch02Tests;

using Ch02;

public class Ch02Tests
{
    [Fact]
    public void Negate()
    {
        static bool IsEven(int x) => x % 2 == 0;

        Assert.False(Ch02.Negate<int>(IsEven)(2));
        Assert.True(Ch02.Negate<int>(IsEven)(1));
    }

    [Theory]
    [MemberData(nameof(QsortData))]
    public void Qsort(int[] xs)
    {
        var expected = xs
            .OrderBy(n => n)
            .ToList();

        static int NaturalOrder(int x, int y) => x.CompareTo(y);

        Assert.Equal(expected, Ch02.Qsort(xs.ToList(), NaturalOrder));
    }

    // xUnit1047: Avoid using TheoryDataRow arguments that might not be serializable.
    // https://xunit.net/xunit.analyzers/rules/xUnit1047
    public static IEnumerable<TheoryDataRow<int[]>> QsortData() =>
        [
            new([3, 2, 1]), new([1, 2, 3]), new([1, 1, 1]), new([1]), new([])
        ];
}