using System.Diagnostics.CodeAnalysis;

using LaYumba.Functional;

using static LaYumba.Functional.F;

using Candidate = System.String;

namespace Ch08;

public static class FunctionalErrors
{
    // 1. Write a `ToOption` extension method to convert an `Either` into an
    // `Option`. Then write a `ToEither` method to convert an `Option` into an
    // `Either`, with a suitable parameter that can be invoked to obtain the
    // appropriate `Left` value, if the `Option` is `None`. (Tip: start by writing
    // the function signatures in arrow notation)
    public static Option<R> ToOption<L, R>(this Either<L, R> either) =>
        either.Match
        (
            l => None,
            r => Some(r)
        );

    public static Either<L, R> ToEither<L, R>(this Option<R> opt, Func<L> leftSupplier) =>
        opt.Match<Either<L, R>>(
            Some: (data) => Right(data),
            None: () => Left(leftSupplier())
        );

    // 2. Take a workflow where 2 or more functions that return an `Option`
    // are chained using `Bind`.

    // Then change the first one of the functions to return an `Either`.

    // This should cause compilation to fail. Since `Either` can be
    // converted into an `Option` as we have done in the previous exercise,
    // write extension overloads for `Bind`, so that
    // functions returning `Either` and `Option` can be chained with `Bind`,
    // yielding an `Option`.

    private static readonly Func<Candidate, bool> IsEligible =
        _ => throw new NotImplementedException();
    private static readonly Func<Candidate, Either<Error, Candidate>> TechTest =
        _ => throw new NotImplementedException();
    private static readonly Func<Candidate, Option<Candidate>> Interview =
        _ => throw new NotImplementedException();

    public static Option<T> Bind<L, R, T>(this Either<L, R> either, Func<R, Option<T>> f) =>
        either.Match(
            l => None,
            r => f(r)
        );

    [SuppressMessage("CodeQuality", "IDE0051: Remove unused private member")]
    private static Option<Candidate> Recruit(Candidate c)
    => Some(c)
        .Where(IsEligible)
        .Bind(c => TechTest(c).Bind(Interview));

    // 3. Write a function `TryRun` of type (() -> T) -> Exceptional<T> that will
    // run the given function in a `try/catch`, returning an appropriately
    // populated `Exceptional`.
    public static Exceptional<T> TryRun<T>(Func<T> supplier)
    {
        try
        {
            return supplier();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    // 4. Write a function `Safely` of type ((() -> R), (Exception -> L)) -> Either<L, R> 
    // that will run the given function in a `try/catch`, returning an appropriately
    // populated `Either`.
    public static Either<L, R> Safely<L, R>(Func<R> supplier, Func<Exception, L> handler)
    {
        try
        {
            return supplier();
        }
        catch (Exception ex)
        {
            return handler(ex);
        }
    }
}