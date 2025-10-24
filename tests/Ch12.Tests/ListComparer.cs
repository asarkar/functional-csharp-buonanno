using System.Diagnostics.CodeAnalysis;

using static Ch12.FunctionalDataStructures;
using static Ch12.LinkedList;

namespace Ch12.Tests;

public class ListComparer<T> : IEqualityComparer<List<T>>
{
    public bool Equals(List<T>? xs, List<T>? ys) =>
        ReferenceEquals(xs, ys) ||
        (
            xs is not null && ys is not null &&
            xs.ToSystemList().SequenceEqual(ys.ToSystemList())
        );

    public int GetHashCode([DisallowNull] List<T> list) =>
        list.Aggregate
        (
            new HashCode(),
            (hash, x) =>
            {
                hash.Add(x);
                return hash;
            }
        ).ToHashCode();
}