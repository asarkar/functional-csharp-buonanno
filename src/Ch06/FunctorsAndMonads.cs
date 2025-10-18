using System.Collections.Immutable;

using LaYumba.Functional;

using static LaYumba.Functional.F;

namespace Ch06;

public static class FunctorsAndMonads
{
    // 1. Implement Map for ISet<T> and IDictionary<K, T>. (Tip: start by writing down
    // the signature in arrow notation.)

    // Map : ISet<T> -> (T -> R) -> ISet<R>
    public static ISet<R> Map<T, R>(this ISet<T> ts, Func<T, R> f)
    {
        var rs = new HashSet<R>();
        foreach (T t in ts)
        {
            _ = rs.Add(f(t));
        }
        return rs.ToImmutableHashSet();
    }

    // Map : IDictionary<K, T> -> (T -> R) -> IDictionary<K, R>
    public static IDictionary<K, R> Map<K, T, R>(this IDictionary<K, T> kts, Func<T, R> f)
        where K : notnull
    {
        var rs = new Dictionary<K, R>();
        foreach (KeyValuePair<K, T> entry in kts)
        {
            rs[entry.Key] = f(entry.Value);
        }
        return rs.ToImmutableDictionary();
    }

    // 2. Implement Map for Option and IEnumerable in terms of Bind and Return.
    public static Option<R> Map<T, R>(this Option<T> opt, Func<T, R> f) =>
        opt.Bind(x => Some(f(x)));

    public static IEnumerable<R> Map<T, R>(this IEnumerable<T> ts, Func<T, R> f)
        => ts.Bind(x => new List<R>() { f(x) });

    // 3. Use Bind and an Option-returning Lookup function (such as the one we defined
    // in chapter 3) to implement GetWorkPermit, shown below. 

    // Then enrich the implementation so that `GetWorkPermit`
    // returns `None` if the work permit has expired.
    public static Option<WorkPermit> GetWorkPermit(
        Dictionary<string, Employee> people, string employeeId) =>
            people.Lookup((x) => x == employeeId)
            .Bind(e => e.WorkPermit.Where(p => p.Expiry >= DateTime.Now));

    private static Option<T> Lookup<K, T>(this IDictionary<K, T> kts, Predicate<K> p)
        where K : notnull
    {
        foreach (KeyValuePair<K, T> entry in kts)
        {
            if (p(entry.Key))
            {
                return Some(entry.Value);
            }
        }
        return None;
    }

    // 4. Use Bind to implement AverageYearsWorkedAtTheCompany, shown below (only
    // employees who have left should be included).
    public static double AverageYearsWorkedAtTheCompany(List<Employee> employees) =>
        employees
            .Bind(e => e.LeftOn.Map(left => left - e.JoinedOn))
            .Average(ts => ts.TotalDays / 365.2425);

    public record Employee
   (
      string Id,
      Option<WorkPermit> WorkPermit,

      DateTime JoinedOn,
      Option<DateTime> LeftOn
   );

    public record WorkPermit
   (
      string Number,
      DateTime Expiry
   );
}