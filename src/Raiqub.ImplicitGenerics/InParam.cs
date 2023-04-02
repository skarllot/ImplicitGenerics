namespace Raiqub.ImplicitGenerics;

/// <summary>A factory to create <see cref="IInParam{T}"/> instances.</summary>
public static class InParam
{
    /// <summary>Creates a contravariant generic type parameter.</summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <returns>An instance representing the generic type parameter.</returns>
    public static IInParam<T> Of<T>() => InParam<T>.Instance;
}

internal sealed class InParam<T> : IInParam<T>
{
    public static readonly InParam<T> Instance = new();

    public Type Type => typeof(T);
}