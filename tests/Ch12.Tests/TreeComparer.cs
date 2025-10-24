using System.Diagnostics.CodeAnalysis;

using static Ch12.FunctionalDataStructures;
using static Ch12.LinkedList;

using Strings = System.Collections.Generic.List<string>;

namespace Ch12.Tests;

public class TreeComparer<T> : IEqualityComparer<Tree<T>>
{
    public bool Equals(Tree<T>? t1, Tree<T>? t2) =>
        ReferenceEquals(t1, t2) ||
        (
            t1 is not null && t2 is not null &&
            Preorder(t1) == Preorder(t2)
        );

    public int GetHashCode([DisallowNull] Tree<T> tree) =>
        Preorder(tree).GetHashCode();

    // Preorder must encode the internal node positions; if it only collects 
    // the leaves, the following 2 trees both yield the same sequence 'ACE'.
    // Tree 1:               Tree 2:
    //     .                      .
    //    / \                    / \
    //   A   .                  .   E
    //      / \                / \
    //     C   E              A   C
    //
    // With internal node markers, we get ". A . C E" and ". . A C E".
    private static string Preorder(Tree<T> tree)
    {
        Strings buf = [];
        var stack = new Stack<Tree<T>>();
        stack.Push(tree);
        while (stack.Count > 0)
        {
            _ = stack.Pop().Match
            (
                leaf =>
                {
                    buf.Add(leaf!.ToString()!);
                    return buf;
                },
                (left, right) =>
                {
                    stack.Push(right);
                    stack.Push(left);
                    buf.Add(".");
                    return buf;
                }
            );
        }
        return string.Join(",", buf);
    }
}