using static Ch12.BinaryTree;
using static Ch12.FunctionalDataStructures;
using static Ch12.LinkedList;

namespace Ch12.Tests;

public class FunctionalDataStructuresTests
{
    [Fact]
    public void InsertAt_Empty_Throws()
    {
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => List<int>().InsertAt(0, 1));
    }

    [Fact]
    public void InsertAt_OutOfIndex_Throws()
    {
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => List(1).InsertAt(2, 1));
    }

    [Fact]
    public void InsertAt_ValidIndex_ReturnsUpdatedList()
    {
        List<int> actual = List(1, 2, 4, 5).InsertAt(2, 3);
        List<int> expected = List(1, 2, 3, 4, 5);
        Assert.Equal(expected, actual, new ListComparer<int>());
    }

    [Fact]
    public void RemoveAt_Empty_Throws()
    {
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => List<int>().RemoveAt(0));
    }

    [Fact]
    public void RemoveAt_OutOfIndex_Throws()
    {
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => List(1).RemoveAt(2));
    }

    [Fact]
    public void RemoveAt_ValidIndex_ReturnsUpdatedList()
    {
        List<int> actual = List(1, 2, 3, 4).RemoveAt(2);
        List<int> expected = List(1, 2, 4);
        Assert.Equal(expected, actual, new ListComparer<int>());
    }

    [Fact]
    public void TakeWhile_Empty_ReturnsEmpty()
    {
        List<int> actual = List<int>().TakeWhile(_ => true);
        List<int> expected = List<int>();
        Assert.Equal(expected, actual, new ListComparer<int>());
    }

    [Fact]
    public void TakeWhile_NonEmpty_ReturnsListWithMatchingElements()
    {
        List<int> actual = List(1, 2, 4, 3, 5).TakeWhile(i => i < 4);
        List<int> expected = List(1, 2);
        Assert.Equal(expected, actual, new ListComparer<int>());
    }

    [Fact]
    public void DropWhile_Empty_ReturnsEmpty()
    {
        List<int> actual = List<int>().DropWhile(_ => true);
        List<int> expected = List<int>();
        Assert.Equal(expected, actual, new ListComparer<int>());
    }

    [Fact]
    public void TakeWhile_NonEmpty_ReturnsListWithMatchingElementsRemoved()
    {
        List<int> actual = List(1, 2, 4, 3, 5).DropWhile(i => i < 4);
        List<int> expected = List(4, 3, 5);
        Assert.Equal(expected, actual, new ListComparer<int>());
    }

    [Fact]
    public void Bind_Leaf_ReturnsNewTree()
    {
        Tree<int> actual = Leaf(1)
            .Bind(x => Branch(Leaf(x), Leaf(x)));
        Tree<int> expected = Branch(Leaf(1), Leaf(1));
        Assert.Equal(expected, actual, new TreeComparer<int>());
    }

    [Fact]
    public void Bind_Branch_ReturnsNewTree()
    {
        Tree<int> actual = Branch(Leaf(1), Leaf(2))
            .Bind(x => Branch(Leaf(x), Leaf(x)));
        Tree<int> expected = Branch(
            Branch(Leaf(1), Leaf(1)),
            Branch(Leaf(2), Leaf(2))
        );
        Assert.Equal(expected, actual, new TreeComparer<int>());
    }

    private static LabelTree<string> StringLabelTree
    (
        string label,
        List<LabelTree<string>>? children = null
    ) =>
        new(label, children ?? List<LabelTree<string>>());

    [Fact]
    public void TestLabelTree()
    {
        LabelTree<string> tree = StringLabelTree(
            "root",
            List(
                StringLabelTree
                (
                    "footwear",
                    List
                    (
                        StringLabelTree("shoes_cycling"),
                        StringLabelTree("overshoes")
                    )
                ),
                StringLabelTree
                (
                    "accessories",
                    List
                    (
                        StringLabelTree("pumps"),
                        StringLabelTree("sunglasses")
                    )
                )
            )
        );

        // mapping of keys to translations in German
        var localizations = new Dictionary<string, string>
        {
            ["root"] = "Alle Kategorien",
            ["footwear"] = "Shuhe",
            ["shoes_cycling"] = "Fahrradschuhe",
            ["overshoes"] = "Schuhüberzüge",
            ["accessories"] = "Zubehör",
            ["pumps"] = "Pumpen",
            ["sunglasses"] = "Sonnenbrillen"
        };

        LabelTree<string> localizedTree = Localize(tree, localizations);

        LabelTree<string> expected = StringLabelTree
        (
            "Alle Kategorien",
            List
            (
                StringLabelTree
                (
                    "Shuhe",
                    List
                    (
                        StringLabelTree("Fahrradschuhe"),
                        StringLabelTree("Schuhüberzüge")
                    )
                ),
                StringLabelTree
                (
                    "Zubehör",
                    List
                    (
                        StringLabelTree("Pumpen"),
                        StringLabelTree("Sonnenbrillen")
                    )
                )
            )
        );

        Assert.Equal(expected, localizedTree, new LabelTreeComparer<string>());
    }
}