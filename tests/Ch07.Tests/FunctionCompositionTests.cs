using Ch07;

namespace Ch07.Tests;

public class FunctionCompositionTests
{
    [Fact]
    public void Compose()
    {
        // Method groups are not automatically inferred as Funcs in extension methods.
        Func<int, int> times2 = i => i * 2;
        static int Plus1(int i) => i + 1;

        var actual = times2.Compose<int, int, int>(Plus1)(2);

        Assert.Equal(6, actual);
    }
}