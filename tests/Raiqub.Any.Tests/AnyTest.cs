using FluentAssertions;

namespace Raiqub.Any.Tests;

public static class AnyTest
{
    [Fact]
    public static void OfTypeShouldCast()
    {
        var array = new object[] { "Hi", 10, DateTime.Now, StringComparison.Ordinal };
        var result = array.OfType(Any.Value<string>());

        result.Should().Equal("Hi");
    }

    private static IEnumerable<TOut> OfType<TIn, TOut>(this IEnumerable<TIn> enumerable, IAny<TOut> any)
    {
        if (any == null) throw new ArgumentNullException(nameof(any));
        return Iterator(enumerable);

        static IEnumerable<TOut> Iterator(IEnumerable<TIn> enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item is TOut @out)
                    yield return @out;
            }
        }
    }
}