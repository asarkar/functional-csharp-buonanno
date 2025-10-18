namespace Ch07;

public static class FunctionComposition
{
    // private sealed record Person(decimal Earnings);

    // 1. Without looking at any code or documentation (or intllisense), write the function 
    // signatures of `OrderByDescending`, `Take` and `Average`, which we used to implement 
    // `AverageEarningsOfRichestQuartile`:

    // private static decimal AverageEarningsOfRichestQuartile(List<Person> population) =>
    //     population
    //         .OrderByDescending(p => p.Earnings)
    //         .Take(population.Count / 4)
    //         .Average(p => p.Earnings);

    // LINQ's built-in OrderBy and OrderByDescending methods don't require TKey : IComparable<TKey>.
    // They accept an IComparer<TKey>? parameter (which defaults to Comparer<R>.Default).

    // public static IEnumerable<T> OrderByDescending<TSource, TKey>(
    //     this IEnumerable<TSource> ts,
    //     Func<TSource, TKey> keyFunc)
    //     where TKey : IComparable<TKey>
    // { }

    // public static IEnumerable<TSource> Take<TSource>(
    //     this IEnumerable<TSource> ts,
    //     int n)
    // { }

    // Average is different because it's a terminal method, and causes the previous
    // methods in the chain to be evaluated.

    // public static double Average<TSource>(
    //     this IEnumerable<TSource> ts,
    //     Func<TSource, double> keyFunc)
    // { }

    // 3. Implement a general purpose Compose function that takes two unary functions
    // and returns the composition of the two.
    // Usage: f.Compose(g)
    public static Func<TG, TH> Compose<TG, TF, TH>(this Func<TF, TH> f, Func<TG, TF> g) =>
        tg => f(g(tg));
}