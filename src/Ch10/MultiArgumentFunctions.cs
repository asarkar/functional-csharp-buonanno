using LaYumba.Functional;

using static LaYumba.Functional.F;

using Food = System.String;
using Ingredients = System.String;
using Reason = System.String;
using Unit = System.ValueTuple;

namespace Ch10;

public static class MultiArgumentFunctions
{
    // 1. Implement `Apply` for `Either` and `Exceptional`.
    public static Either<TLeft, TResult> Apply<TLeft, TRight, TResult>
    (
        this Either<TLeft, Func<TRight, TResult>> eitherF,
        Either<TLeft, TRight> either
    )
        => eitherF.Bind(f => either.Map(r => f(r)));

    public static Exceptional<R> Apply<T, R>
    (
        this Exceptional<Func<T, R>> exF,
        Exceptional<T> ex
    )
        => exF.Bind(f => ex.Map(r => f(r)));

    // 2. Implement the query pattern for `Either` and `Exceptional`. Try to
    // write down the signatures for `Select` and `SelectMany` without
    // looking at any examples. For the implementation, just follow the
    // types--if it type checks, it’s probably right!
    public static Either<TLeft, TResult> Select<TLeft, TRight, TResult>
    (
        this Either<TLeft, TRight> either,
        Func<TRight, TResult> f
    ) =>
        either.Map(f);

    public static Exceptional<R> Select<T, R>(
        this Exceptional<T> ex,
        Func<T, R> f
    ) =>
        ex.Map(f);

    public static Either<TLeft, TResult> SelectMany<TLeft, TRight, TResult>
    (
        this Either<TLeft, TRight> either,
        Func<TRight, Either<TLeft, TResult>> f
    ) =>
        either.Bind(f);

    public static Exceptional<R> SelectMany<T, R>
    (
        this Exceptional<T> ex,
        Func<T, Exceptional<R>> f
    ) =>
        ex.Bind(f);

    // 3. Come up with a scenario in which various `Either`-returning
    // operations are chained with `Bind`. (If you’re short of ideas, you can
    // use the favorite-dish example from Examples/Chapter08/CookFavouriteFood.)
    // Rewrite the code using a LINQ expression.

    // private sealed record Reason { }
    // private sealed record Ingredients { }
    // private sealed record Food { }

    private static readonly Func<Either<Reason, Unit>> WakeUpEarly =
        () => Right<Unit>(default);
    private static readonly Func<Either<Reason, Ingredients>> ShopForIngredients =
        () => throw new NotImplementedException();
    private static readonly Func<Ingredients, Either<Reason, Food>> CookRecipe =
        _ => throw new NotImplementedException();

    private static readonly Action<Food> EnjoyTogether =
        _ => throw new NotImplementedException();
    private static readonly Action<Reason> ComplainAbout =
        _ => throw new NotImplementedException();
    private static readonly Action OrderPizza =
        () => throw new NotImplementedException();

#pragma warning disable IDE0051
    private static Unit PrepareFavoriteDishUsingEitherChaining() =>
        WakeUpEarly()
            .Bind(_ => ShopForIngredients())
            .Bind(CookRecipe)
            .Match
            (
                Right: dish => EnjoyTogether(dish),
                Left: reason =>
            {
                ComplainAbout(reason);
                OrderPizza();
            }
        );

    private static Either<Reason, Food> PrepareFavoriteDishUsingLinq() =>
        from _ in WakeUpEarly()
        from ingredients in ShopForIngredients()
        from dish in CookRecipe(ingredients)
        select dish;

    private static void ConsumeFavoriteDish() =>
        PrepareFavoriteDishUsingLinq()
        .Match
            (
                Left: ComplainAbout,
                Right: EnjoyTogether
            );
#pragma warning restore IDE0051
}