using Ch06;

using LaYumba.Functional;

using static Ch06.FunctorsAndMonads;
using static LaYumba.Functional.F;

namespace Ch06.Tests;

public class FunctorsAndMonadsTests
{
    [Fact]
    public void Map_SetTimes2_ReturnsSetWithValuesDoubled()
    {
        var xs = new HashSet<int>() { 1, 2, 3 };
        ISet<int> actual = xs.Map(Times2);
        var expected = new HashSet<int>() { 2, 4, 6 };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Map_DictionaryTimes2_ReturnsSetWithValuesDoubled()
    {
        var xs = new Dictionary<char, int>()
        {
            ['a'] = 1,
            ['b'] = 2,
            ['c'] = 3
        };
        IDictionary<char, int> actual = xs.Map(Times2);
        var expected = new Dictionary<char, int>()
        {
            ['a'] = 2,
            ['b'] = 4,
            ['c'] = 6
        };
        Assert.Equal(expected, actual);
    }

    private int Times2(int i) =>
        i * 2;

    [Fact]
    public void Map_SomeTimes2_ReturnsSomeWithValueDoubled()
    {
        Assert.Equal(Some(4), Some(2).Map(Times2));
    }

    [Fact]
    public void Map_None_ReturnsNone()
    {
        Assert.Equal(None, Some(2).Where(i => i == 0).Map(Times2));
    }

    [Fact]
    public void GetWorkPermit_NoWorkPermit_ReturnsNone()
    {
        Option<WorkPermit> actual = GetWorkPermit(EmployeesById, "john");
        Assert.Equal(None, actual);
    }

    [Fact]
    public void GetWorkPermit_ExpiredWorkPermit_ReturnsNone()
    {
        Option<WorkPermit> actual = GetWorkPermit(EmployeesById, "jane");
        Assert.Equal(None, actual);
    }

    [Fact]
    public void GetWorkPermit_ValidWorkPermit_ReturnsSome()
    {
        Option<WorkPermit> actual = GetWorkPermit(EmployeesById, "barb");
        Assert.Equal(WorkPermitsByNumber["barb"], actual);
    }

    [Fact]
    public void AverageYearsWorkedAtTheCompany()
    {
        var actual = FunctorsAndMonads.AverageYearsWorkedAtTheCompany([.. EmployeesById.Values]);
        Assert.Equal(1.5, actual, 2);
    }

    private static readonly int CurrentYear = DateTime.Now.Year;

    private static DateTime FirstDayOfYear(int year) =>
        new(year, 1, 1);

    private static readonly Dictionary<string, WorkPermit> WorkPermitsByNumber =
        new List<WorkPermit>()
        {
            new("jane", FirstDayOfYear(CurrentYear - 1)),
            new("barb", FirstDayOfYear(CurrentYear + 5)),
            new("tom", FirstDayOfYear(CurrentYear + 1))
        }.ToDictionary(obj => obj.Number);

    private static readonly Dictionary<string, Employee> EmployeesById =
        new List<Employee>()
        {
            new("john", None, FirstDayOfYear(CurrentYear - 4), Some(FirstDayOfYear(CurrentYear - 2))),
            new("ivan", None, FirstDayOfYear(CurrentYear - 2), Some(FirstDayOfYear(CurrentYear - 1))),
            new("jane", Some(WorkPermitsByNumber["jane"]), FirstDayOfYear(CurrentYear - 2), None),
            new("barb", Some(WorkPermitsByNumber["barb"]), FirstDayOfYear(CurrentYear - 10), None),
            new("tom", Some(WorkPermitsByNumber["tom"]), FirstDayOfYear(CurrentYear - 1), None),
        }.ToDictionary(obj => obj.Id);
}