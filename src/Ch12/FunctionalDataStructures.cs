using LaYumba.Functional;

using static Ch12.BinaryTree;
using static Ch12.LinkedList;

namespace Ch12;

public static class FunctionalDataStructures
{
    // Lists:

    // Implement functions to work with the singly linked List defined in this chapter:
    // Tip: start by writing the function signature in arrow-notation.

    // 1.
    // ◦ InsertAt inserts an item at the given index.
    // ◦ RemoveAt removes the item at the given index.
    // ◦ TakeWhile takes a predicate, and traverses the list yielding 
    //   all items until it find one that fails the predicate.
    // ◦ DropWhile works similarly, but excludes all items at the front of the list.

    public static List<T> InsertAt<T>(this List<T> list, int index, T item) =>
        list.Match
        (
            () => throw new ArgumentOutOfRangeException(nameof(index)),
            (head, tail) =>
                index == 0 ?
                List(item, List(head, tail)) :
                List(head, tail.InsertAt(index - 1, item))
        );

    public static List<T> RemoveAt<T>(this List<T> list, int index) =>
        list.Match
        (
            () => throw new ArgumentOutOfRangeException(nameof(index)),
            (head, tail) =>
                index == 0 ?
                tail :
                List(head, tail.RemoveAt(index - 1))
        );

    public static List<T> TakeWhile<T>(this List<T> list, Predicate<T> p) =>
        list.Match
        (
            () => list,
            (head, tail) =>
                p(head) ?
                List(head, tail.TakeWhile(p)) :
                List<T>()
        );

    public static List<T> DropWhile<T>(this List<T> list, Predicate<T> p) =>
        list.Match
        (
            () => list,
            (head, tail) =>
                p(head) ?
                tail.DropWhile(p) :
                list
        );

    // 2. What’s the complexity of these four functions? How many 
    // new objects are required to create the new list?

    // Complexity:
    //  InsertAt: O(n)
    //  RemoveAt: O(n)
    //  TakeWhile: O(n)
    //  DropWhile: O(n)

    // Number of new objects required: 
    //  InsertAt: O(n)
    //  RemoveAt: O(n)
    //  TakeWhile: O(n)
    //  DropWhile: O(1), no new objects are created

    // 3. TakeWhile and DropWhile are useful when working with a list that is sorted 
    // and you'd like to get all items greater/smaller than some value; write implementations 
    // that take an IEnumerable rather than a List

    // Trees:

    // 1. Is it possible to define `Bind` for the binary tree implementation shown in this
    // chapter? If so, implement `Bind`, else explain why it's not possible (hint: start by writing
    // the signature; then sketch binary tree and how you could apply a tree-returning function to
    // each value in the tree).

    // Since the function t -> Tree expects a value, it can only be applied to a leaf.
    // Thus, `Bind` for a tree replaces the leaves with new trees produced by the function `f`. 
    public static Tree<R> Bind<T, R>(this Tree<T> tree, Func<T, Tree<R>> f) =>
        tree.Match
        (
            x => f(x),
            (l, r) => Branch(l.Bind(f), r.Bind(f))
        );

    // 2, Implement a `LabelTree` type, where each node has a label of type string and a list of 
    // subtrees; this could be used to model a typical navigation tree or a cateory tree in a 
    // website.

    public class LabelTree<T>(T label, List<LabelTree<T>> children)
    {
        public T Label { get; } = label;
        public List<LabelTree<T>> Children { get; } = children;

        public override string ToString() =>
            $"{Label}";
    }

    // 3. Imagine you need to add localization to your navigation tree: you're given a `LabelTree` 
    // where the value of each label is a key, and a dictionary that maps keys to translations 
    // in one of the languages that your site must support (hint: define `Map` for `LabelTree` 
    // and use it to obtain the localized navigation/category tree).

    public static LabelTree<R> Map<T, R>(this LabelTree<T> tree, Func<T, R> f) =>
        new(f(tree.Label), tree.Children.Map(c => c.Map(f)));

    public static LabelTree<string> Localize
    (
        LabelTree<string> tree,
        IDictionary<string, string> cultures
    ) =>
        tree.Map(x => cultures[x]);
}