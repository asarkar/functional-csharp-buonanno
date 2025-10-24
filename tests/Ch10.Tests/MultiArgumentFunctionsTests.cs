using LaYumba.Functional;

using static Ch10.MultiArgumentFunctions;
using static LaYumba.Functional.F;

namespace Ch10.Tests;

public class MultiArgumentFunctionsTests
{
    [Fact]
    public void Apply_LeftOnLeft_ReturnsLeft()
    {
        Either<string, int> either = Left("whatever");
        Either<string, Func<int, int>> eitherF = Left("");
        Either<string, int> actual = eitherF.Apply(either);
        Assert.Equal(Left(""), actual);
    }

    [Fact]
    public void Apply_RightOnLeft_ReturnsLeft()
    {
        Either<string, int> either = Left("whatever");
        Either<string, Func<int, int>> eitherF =
            Right<Func<int, int>>(_ => throw new NotImplementedException());
        Assert.Equal(either, eitherF.Apply(either));
    }

    [Fact]
    public void Apply_LeftOnRight_ReturnsLeft()
    {
        Either<string, int> either = Right(1);
        Either<string, Func<int, int>> eitherF = Left("");
        Either<string, int> actual = eitherF.Apply(either);
        Assert.Equal(Left(""), actual);
    }

    [Fact]
    public void Apply_RightOnRight_ReturnsRight()
    {
        Either<string, int> either = Right(1);
        Either<string, Func<int, int>> eitherF =
            Right<Func<int, int>>(x => x * 2);
        Either<string, int> actual = eitherF.Apply(either);
        Assert.Equal(Right(2), actual);
    }

    [Fact]
    public void Select_Left_ReturnsLeft()
    {
        Either<string, int> either = Left("whatever");
        Either<string, int> actual = either.Select(x => x * 2);
        Assert.Equal(either, actual);
    }

    [Fact]
    public void Select_Right_ReturnsRight()
    {
        Either<string, int> either = Right(1);
        Either<string, int> actual = either.Select(x => x * 2);
        Assert.Equal(Right(2), actual);
    }

    [Fact]
    public void SelectMany_Left_ReturnsLeft()
    {
        Either<string, int> either = Left("whatever");
        Either<string, int> actual =
            either.SelectMany<string, int, int>(_ => throw new NotImplementedException());
        Assert.Equal(either, actual);
    }

    [Fact]
    public void SelectMany_Right_ReturnsRight()
    {
        Either<string, int> either = Right(1);
        Either<string, int> actual =
            either.SelectMany<string, int, int>(x => Right(x * 2));
        Assert.Equal(Right(2), actual);
    }
}