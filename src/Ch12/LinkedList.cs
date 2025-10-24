using LaYumba.Functional;

using static LaYumba.Functional.F;

namespace Ch12;

public abstract record List<T>;
internal sealed record Empty<T> : List<T>;
internal sealed record Cons<T>(T Head, List<T> Tail) : List<T>;

public static class LinkedList
{
    public static R Match<T, R>
    (
        this List<T> list,
        Func<R> empty,
        Func<T, List<T>, R> cons
    ) =>
    list switch
    {
        Empty<T> => empty(),
        Cons<T>(var head, var tail) => cons(head, tail),
        _ => throw new ArgumentException("List can only be Empty or Cons")
    };

    public static List<T> List<T>() =>
        new Empty<T>();

    public static List<T> List<T>(T h, List<T> t) =>
        new Cons<T>(h, t);

    public static List<T> List<T>(params T[] items) =>
        items.Reverse().Aggregate(List<T>(), (tail, head) => List(head, tail));

    public static R Aggregate<T, R>
    (
        this List<T> list,
        R acc,
        Func<R, T, R> f
    ) =>
        list.Match
        (
            () => acc,
            (head, tail) => tail.Aggregate(f(acc, head), f)
        );

    public static List<R> Map<T, R>
    (
        this List<T> list,
        Func<T, R> f
    ) =>
        list.Match
        (
            List<R>,
            (head, tail) => List(f(head), tail.Map(f))
        );

    public static System.Collections.Generic.List<T> ToSystemList<T>(this List<T> list) =>
        list.Aggregate
        (
            new System.Collections.Generic.List<T>(),
            (acc, t) =>
            {
                acc.Add(t);
                return acc;
            }
        );

    public static Option<T> Head<T>(this IEnumerable<T> list)
    {
        IEnumerator<T> enumerator = list.GetEnumerator();
        return enumerator.MoveNext()
            ? Some(enumerator.Current)
            : None;
    }

    public static R Match<T, R>
    (
       this IEnumerable<T> list,
       Func<R> empty,
       Func<T, IEnumerable<T>, R> otherwise
    ) =>
        list.Head().Match
        (None: () => empty(),
        Some: (head) => otherwise(head, list.Skip(1))
        );
}