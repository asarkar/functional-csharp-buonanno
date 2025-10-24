using System.Diagnostics.CodeAnalysis;

using static Ch12.FunctionalDataStructures;

using Strings = System.Collections.Generic.List<string>;

namespace Ch12.Tests;

public class LabelTreeComparer<T> : IEqualityComparer<LabelTree<T>>
{
    public bool Equals(LabelTree<T>? t1, LabelTree<T>? t2) =>
        ReferenceEquals(t1, t2) ||
        (
            t1 is not null && t2 is not null &&
            LevelOrder(t1).SequenceEqual(LevelOrder(t2))
        );

    public int GetHashCode([DisallowNull] LabelTree<T> tree)
    {
        var hasher = new HashCode();
        hasher.Add(tree.Label);
        if (tree.Children != null)
        {
            _ = tree.Children.Aggregate
            (
                hasher,
                (acc, child) =>
                {
                    acc.Add(GetHashCode(child));
                    return acc;
                }
            );
        }
        return hasher.ToHashCode();
    }

    public Strings LevelOrder(LabelTree<T> root)
    {
        var q = new Queue<LabelTree<T>>();
        q.Enqueue(root);
        Strings levels = [];
        while (q.Count > 0)
        {
            var n = q.Count;
            Strings level = [];
            for (var i = 0; i < n; i++)
            {
                LabelTree<T> tree = q.Dequeue();
                level.Add(tree.Label!.ToString()!);
                if (tree.Children != null)
                {
                    _ = tree.Children.Aggregate
                    (
                        q,
                        (acc, child) =>
                        {
                            acc.Enqueue(child);
                            return acc;
                        }
                    );
                }
            }
            levels.Add(string.Join(",", level));
        }
        return levels;
    }
}