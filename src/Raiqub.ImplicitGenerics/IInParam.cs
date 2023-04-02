namespace Raiqub.ImplicitGenerics;

/// <summary>
/// Represents a contravariant generic type parameter, allowing implicit generics.
/// Use <see cref="InParam.Of{T}"/> from <see cref="InParam"/> class to create an instance.
/// </summary>
/// <typeparam name="T">The generic type.</typeparam>
public interface IInParam<in T>
{
    /// <summary>Gets the generic type represented by this container.</summary>
    Type Type { get; }
}