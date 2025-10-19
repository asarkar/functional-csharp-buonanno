using Ch08;

using LaYumba.Functional;

using static Ch08.FunctionalErrors;

using static LaYumba.Functional.F;

namespace Ch08.Tests;

public class FunctionErrorsTests
{
    [Fact]
    public void ToOption_Left_ReturnsNone()
    {
        Either<int, int> either = Left(1);
        Assert.Equal(None, either.ToOption());
    }

    [Fact]
    public void ToOption_Right_ReturnsSome()
    {
        Either<int, int> either = Right(1);
        Assert.Equal(Some(1), either.ToOption());
    }

    [Fact]
    public void ToEither_None_ReturnsLeft()
    {
        Option<int> opt = None;
        Assert.Equal(Left(1), opt.ToEither(() => 1));
    }

    [Fact]
    public void ToEither_Some_ReturnsRight()
    {
        Option<int> opt = Some(1);
        Assert.Equal(Right(1), opt.ToEither(() => 1));
    }

    [Fact]
    public void TryRun_NoException_ReturnsExceptional()
    {
        Exceptional<int> actual = TryRun(() => 1);
        Assert.Equal(1, actual);
    }

    [Fact]
    public void TryRun_Exception_ReturnsExceptional()
    {
        var expected = new ArithmeticException("boom!");
        Exceptional<int> actual = TryRun<int>(() => throw expected);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Safely_NoException_ReturnsRight()
    {
        Either<int, int> actual = Safely(() => 1, _ => int.MaxValue);
        Assert.Equal(Right(1), actual);
    }

    [Fact]
    public void Safely_Exception_ReturnsLeft()
    {
        Either<int, int> actual =
            Safely<int, int>(() => throw new ArithmeticException("boom!"), _ => 1);
        Assert.Equal(Left(1), actual);
    }
}