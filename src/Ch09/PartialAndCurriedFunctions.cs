using LaYumba.Functional;

namespace Ch09;

public static class PartialAndCurriedFunctions
{
    // 1. Partial application with a binary arithmethic function:
    // ◦ Write a function `Remainder`, that calculates the remainder of 
    // integer division (and works for negative input values!). 
    // Notice how the expected order of parameters is not the
    // one that is most likely to be required by partial application
    // (you are more likely to partially apply the divisor).
    public static Func<int, int, int> Remainder => (dividend, divisor)
         => dividend - (((int)Math.Floor((double)dividend / divisor)) * divisor);

    // ◦ Write an `ApplyR` function, that gives the rightmost parameter to
    // a given binary function (try to write it without looking at the implementation for `Apply`).
    // Write the signature of `ApplyR` in arrow notation, both in curried and non-curried forms.
    public static Func<T1, R> ApplyR<T1, T2, R>(this Func<T1, T2, R> fn, T2 t2) =>
        t1 => fn(t1, t2);

    // ◦ Use `ApplyR` to create a function that returns the
    // remainder of dividing any number by 5. 
    public static int DivideBy5Remainder(int x) =>
        ApplyR(Remainder, 5)(x);

    // ◦ Write an overload of `ApplyR` that gives the rightmost argument to a ternary function.
    public static Func<T1, T2, R> ApplyR<T1, T2, T3, R>(this Func<T1, T2, T3, R> func, T3 t3)
          => (t1, t2) => func(t1, t2, t3);

    // 2. Ternary functions:
    // ◦ Define a class `PhoneNumber` with 3 fields: number type(home, mobile, ...), 
    // country code('it', 'uk', ...), and number.
    // `CountryCode` should be a custom type with implicit conversion to and from string.
    public enum NumberType
    {
        Home, Mobile
    }

    public enum CountryCode
    {
        It, Uk
    }

    public record PhoneNumber(NumberType Type, CountryCode Code, int Number) { }

    // ◦ Define a ternary function that creates a new number, given values for these fields.
    // What's the signature of your factory function? 
    public static Func<CountryCode, NumberType, int, PhoneNumber> CreatePhoneNumber =>
        (country, type, number) => new PhoneNumber(type, country, number);

    // ◦ Use partial application to create a binary function that creates a UK number, 
    // and then again to create a unary function that creates a UK mobile.
    public static Func<NumberType, int, PhoneNumber> CreateUkNumber =>
        CreatePhoneNumber.Apply(CountryCode.Uk);

    public static Func<int, PhoneNumber> CreateUkMobileNumber =>
        CreateUkNumber.Apply(NumberType.Mobile);

    // 3. Functions everywhere. You may still have a feeling that objects are ultimately 
    // more powerful than functions. Surely, a logger object should expose methods 
    // for related operations such as Debug, Info, Error? 
    // To see that this is not necessarily so, challenge yourself to write 
    // a very simple logging mechanism that does not involve any classes or structs. 
    // You should still be able to inject a Log value into a consumer class/function, 
    // exposing operations like Debug, Info, and Error.

    public enum Level { Debug, Info, Error }

    // A delegate is a variable that holds a reference to a method or function. 
    // This feature allows us to pass around chunks of executable code as though 
    // it were simple data. It looks like a method declaration, aside from the 
    // delegate keyword.
    public delegate void Log(Level level, string message);

    public static readonly Log ConsoleLogger = (level, message) =>
        Console.WriteLine($"[{level}]: {message}");

    // Each of the following methods can be invoked with a method
    // that conforms to the delegate signature, including return type;
    // ConsoleLogger fits the bill. 
    public static void Debug(this Log log, string message) =>
        log(Level.Debug, message);

    public static void Info(this Log log, string message) =>
        log(Level.Info, message);

    public static void Error(this Log log, string message) =>
        log(Level.Error, message);

    // 4. Open exercise: in your day-to-day coding, start paying more attention to the signatures 
    // of the functions you write and consume. Does the order of arguments make sense; do they go 
    // from general to specific? Is there some argument that you always invoke with the same value
    // so that you could partially apply it? Do you sometimes write similar variations of the same 
    // code, and could these be generalized into a parameterized function?

    // 5. Implement Map, Where, and Bind for IEnumerable in terms of Aggregate.
    public static IEnumerable<R> Map<T, R>(this IEnumerable<T> ts, Func<T, R> fn) =>
        // Define `Map` in terms of `Bind`.
        ts.Bind<T, R>(t => [fn(t)]);

    public static IEnumerable<T> Where<T>(this IEnumerable<T> ts, Predicate<T> p) =>
        ts.Aggregate(new List<T>(), (acc, t) =>
        {
            if (p(t))
            {
                acc.Add(t);
            }
            return acc;
        });

    public static IEnumerable<R> Bind<T, R>(this IEnumerable<T> ts, Func<T, IEnumerable<R>> fn) =>
        ts.Aggregate(new List<R>(), (acc, t) =>
        {
            acc.AddRange(fn(t));
            return acc;
        });
}