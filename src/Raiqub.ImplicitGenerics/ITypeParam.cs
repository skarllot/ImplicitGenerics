namespace Raiqub.ImplicitGenerics;

/// <summary>
/// Represents a generic type parameter, allowing implicit generics.
/// Use <see cref="TypeParam.Of{T}"/> from <see cref="TypeParam"/> class to create an instance.
/// </summary>
/// <typeparam name="T">The generic type.</typeparam>
public interface ITypeParam<T>
{
    /// <summary>Gets the generic type represented by this container.</summary>
    Type Type { get; }
}