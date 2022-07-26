using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;

namespace Raiqub.ImplicitGenerics.Tests;

public static class ParamTest
{
    [Fact]
    public static void OfTypeShouldCast()
    {
        var array = new object[] { "Hi", 10, DateTime.Now, StringComparison.Ordinal };
        var result = array.OfType(Param.OutOf<string>());

        result.Should().Equal("Hi");
    }

    [Fact]
    public static void CastShouldCast()
    {
        ReadOnlySpan<char> span = stackalloc char[] { 'A', 'B', 'C', 'D', 'E' };
        var result = span.Cast(Param.OutOf<ushort>());

        result.ToArray().Should().Equal(65, 66, 67, 68, 69);
    }

    [Fact]
    public static void RegisterTypeShouldAddToCollection()
    {
        var provider1 = new ServiceCollection()
            .AddAdapter<int, float, Int32ToFloatAdapter>()
            .BuildServiceProvider(true);
        var provider2 = new ServiceCollection()
            .AddAdapter(Param.OutOf<Int32ToFloatAdapter>())
            .BuildServiceProvider(true);

        var service1 = provider1.GetRequiredService<IAdapter<int, float>>();
        var result1 = service1.Convert(10);

        var service2 = provider2.GetRequiredService<IAdapter<int, float>>();
        var result2 = service2.Convert(20);

        service1.Should().BeOfType<Int32ToFloatAdapter>();
        result1.Should().Be(10);

        service2.Should().BeOfType<Int32ToFloatAdapter>();
        result2.Should().Be(20);
    }

    private static IEnumerable<TOut> OfType<TIn, TOut>(this IEnumerable<TIn> enumerable, IOutParam<TOut> outType)
    {
        outType.ThrowIfNull();

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

    private static ReadOnlySpan<TTo> Cast<TFrom, TTo>(this ReadOnlySpan<TFrom> source, IOutParam<TTo> toType)
        where TFrom : struct
        where TTo : struct
    {
        toType.DebugIfNull();
        return MemoryMarshal.Cast<TFrom, TTo>(source);
    }

    private interface IAdapter<in TIn, out TOut>
    {
        TOut Convert(TIn input);
    }

    private sealed class Int32ToFloatAdapter : IAdapter<int, float>
    {
        public float Convert(int input) => input;
    }

    private static IServiceCollection AddAdapter<TIn, TOut, TAdapter>(this IServiceCollection services)
        where TAdapter : class, IAdapter<TIn, TOut>
    {
        return services.AddSingleton<IAdapter<TIn, TOut>, TAdapter>();
    }

    private static IServiceCollection AddAdapter<TIn, TOut>(
        this IServiceCollection services,
        IOutParam<IAdapter<TIn, TOut>> outParam)
    {
        return services.AddSingleton(typeof(IAdapter<TIn, TOut>), outParam.Type);
    }
}