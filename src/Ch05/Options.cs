using System.Text.RegularExpressions;

using LaYumba.Functional;

using static LaYumba.Functional.F;

namespace Ch05;

public static partial class Options
{
    // 1. Write a generic function that takes a string and parses it as a value of an enum. 
    // It should be usable as follows:

    // Enum.Parse<DayOfWeek>("Friday") // => Some(DayOfWeek.Friday)
    // Enum.Parse<DayOfWeek>("Freeday") // => None
    public static Option<TEnum> Parse<TEnum>(string value, bool ignoreCase = true)
        where TEnum : struct, System.Enum
    {
        return System.Enum.TryParse(value, ignoreCase, out TEnum result) ?
            Some(result) :
            None;
    }

    // 2. Write a Lookup function that will take an IEnumerable and a predicate, and
    // return the first element in the IEnumerable that matches the predicate, or None
    // if no matching element is found. Write its signature in arrow notation:

    // bool isOdd(int i) => i % 2 == 1;
    // new List<int>().Lookup(isOdd) // => None
    // new List<int> { 1 }.Lookup(isOdd) // => Some(1)
    public static Option<T> Lookup<T>(this List<T> ts, Predicate<T> p)
    {
        foreach (T t in ts)
        {
            if (p(t))
            {
                return Some(t);
            }
        }
        return None;
    }

    // 3. Write a type Email that wraps an underlying string, enforcing that it's in a valid
    // format. Ensure that you include the following:
    // - A smart constructor
    // - Implicit conversion to string, so that it can easily be used with the typical API
    //   for sending emails
    public readonly partial struct Email
    {
        [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        private static partial Regex IsValidEmailRegex();

        private string Value { get; }

        public static Option<Email> Create(string email) =>
            IsValid(email) ? Some(new Email(email)) : None;

        private Email(string email) =>
            Value = email;

        private static bool IsValid(string email) =>
            IsValidEmailRegex().IsMatch(email);

        public static implicit operator string(Email email) =>
            email.Value;
    }

    // 4. Take a look at the extension methods defined on IEnumerable in System.LINQ.Enumerable.
    // Which ones could potentially return nothing, or throw some
    // kind of not-found exception, and would therefore be good candidates for
    // returning an Option<T> instead?
}