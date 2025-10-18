using LaYumba.Functional;

using static Ch05.Options;
using static LaYumba.Functional.F;

namespace Ch05.Tests;

public class OptionsTests
{
    [Fact]
    public void Parse_ValidDay_ReturnsSome()
    {
        Assert.Equal(Parse<DayOfWeek>("Friday"), Some(DayOfWeek.Friday));
    }

    [Fact]
    public void Parse_InvalidDay_ReturnsNone()
    {
        Assert.Equal(Parse<DayOfWeek>("Freeday"), None);
    }

    [Fact]
    public void Lookup_OddAbsent_ReturnsNone()
    {
        Assert.Equal(None, new List<int> { 2 }.Lookup(IsOdd));
    }

    private static bool IsOdd(int i) =>
        i % 2 == 1;

    [Fact]
    public void Lookup_OddEmptyList_ReturnsNone()
    {
        Assert.Equal(None, new List<int>().Lookup(IsOdd));
    }

    [Fact]
    public void Lookup_OddPresent_ReturnsSome()
    {
        Assert.Equal(Some(1), new List<int> { 1 }.Lookup(IsOdd));
    }

    [Fact]
    public void Email_Valid_ReturnsSome()
    {
        var actual = GetValue(Email.Create("bruce@wayne.com"));
        Assert.Equal("bruce@wayne.com", actual);
    }

    private static string GetValue(Option<Email> email) =>
        email.Match
        (
            None: () => string.Empty,
            Some: (email) => email
        );

    [Fact]
    public void Email_Invalid_ReturnsNone()
    {
        var actual = GetValue(Email.Create("batman"));
        Assert.Equal(string.Empty, actual);
    }
}