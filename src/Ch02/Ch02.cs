namespace Ch02;

public static class Ch02
{
    // 2. Write a function that negates a given predicate: whenvever the given predicate
    // evaluates to `true`, the resulting function evaluates to `false`, and vice versa.
    public static Predicate<T> Negate<T>(this Predicate<T> p) => x => !p(x);

    // 3. Write a method that uses quicksort to sort a `List<int>` (return a new list,
    // rather than sorting it in place).
    // 4. Generalize your implementation to take a `List<T>`, and additionally a 
    // `Comparison<T>` delegate.
    public static List<T> Qsort<T>(List<T> ts, Comparison<T> comparison)
    {
        // Shallow copy.
        var xs = new List<T>(ts);

        /// <summary>
        /// <para>
        /// Partitions the range [lo..hi+1] into three groups: values less than the pivot, 
        /// values equal to the pivot, and values greater than the pivot.
        /// </para>
        /// </summary>
        /// <param name="lo">The starting index of the partition.</param>
        /// <param name="hi">The ending index of the partition.</param>
        /// <returns> Nothing, the list is sorted in place.</returns>
        void Partition(int lo, int hi)
        {
            if (lo >= hi)
            {
                return;
            }
            T pivot = xs[(lo + hi) / 2];
            int lt = lo, eq = lo, gt = hi;

            while (eq <= gt)
            {
                var cmp = comparison(xs[eq], pivot);
                if (cmp < 0)
                {
                    // Swap the elements at the equal and lesser indices.
                    (xs[eq], xs[lt]) = (xs[lt], xs[eq]);
                    lt++;
                    eq++;
                }
                else if (cmp > 0)
                {
                    // Swap the elements at the equal and greater indices
                    (xs[eq], xs[gt]) = (xs[gt], xs[eq]);
                    gt--;
                }
                else
                {
                    eq++;
                }
            }
            Partition(lo, lt - 1);
            Partition(gt + 1, hi);
        }

        Partition(0, xs.Count - 1);
        return xs;
    }

    // 5. In this chapter, you've seen a `Using` function that takes an `IDisposable`
    // and a function of type `Func<TDisp, R>`. Write an overload of `Using` that
    // takes a `Func<IDisposable>` as first parameter, instead of the `IDisposable`. 
    // (This can be used to fix warnings given by some code analysis tools about 
    // instantiating an `IDisposable` and not disposing it.)
    public static T Using<TDisp, T>(
        Func<TDisp> createDisposable,
        Func<TDisp, T> func)
        where TDisp : IDisposable
    {
        using TDisp disp = createDisposable();
        return func(disp);
    }
}