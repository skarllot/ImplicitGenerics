namespace Raiqub.ImplicitGenerics;

/// <summary>A factory to create <see cref="IOutParam{T}"/> instances.</summary>
public static class Param
{
    /// <summary>Creates a covariant generic type parameter.</summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <returns>An instance representing the generic type parameter.</returns>
    public static IOutParam<T> OutOf<T>() => OutParam<T>.Instance;
}