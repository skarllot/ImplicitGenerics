namespace Raiqub.ImplicitGenerics;

/// <summary>A factory to create <see cref="IOutParam{T}"/> instances.</summary>
public static class OutParam
{
    /// <summary>Creates a covariant generic type parameter.</summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <returns>An instance representing the generic type parameter.</returns>
    public static IOutParam<T> Of<T>() => OutParam<T>.Instance;
}

internal sealed class OutParam<T> : IOutParam<T>
{
    public static readonly OutParam<T> Instance = new();

    public Type Type => typeof(T);
}