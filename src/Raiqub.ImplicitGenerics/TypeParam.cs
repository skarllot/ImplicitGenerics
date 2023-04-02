namespace Raiqub.ImplicitGenerics;

/// <summary>A factory to create <see cref="ITypeParam{T}"/> instances.</summary>
public static class TypeParam
{
    /// <summary>Creates a generic type parameter.</summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <returns>An instance representing the generic type parameter.</returns>
    public static ITypeParam<T> Of<T>() => TypeParam<T>.Instance;
}

internal sealed class TypeParam<T> : ITypeParam<T>
{
    public static readonly TypeParam<T> Instance = new();

    public Type Type => typeof(T);
}