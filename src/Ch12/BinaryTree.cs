namespace Ch12;

public abstract record Tree<T>;
internal sealed record Leaf<T>(T Value) : Tree<T>;
internal sealed record Branch<T>(Tree<T> Left, Tree<T> Right) : Tree<T>;

public static class BinaryTree
{
    public static Tree<T> Leaf<T>(T value) => new Leaf<T>(value);

    public static Tree<T> Branch<T>(Tree<T> left, Tree<T> right)
       => new Branch<T>(left, right);

    public static R Match<T, R>
    (
        this Tree<T> tree,
        Func<T, R> leaf,
        Func<Tree<T>, Tree<T>, R> branch
    ) =>
    tree switch
    {
        Leaf<T>(T val) => leaf(val),
        Branch<T>(var l, var r) => branch(l, r),
        _ => throw new ArgumentException($"{tree} is not a valid tree")
    };
}