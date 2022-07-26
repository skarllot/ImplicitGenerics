namespace Raiqub.Any;

/// <summary>
/// Represents a covariant generic type parameter, allowing implicit generics.
/// Use <see cref="Param.OutOf{T}"/> from <see cref="Param"/> class to create an instance.
/// </summary>
/// <typeparam name="T">The generic type.</typeparam>
public interface IOutParam<out T>
{
    /// <summary>Gets the generic type represented by this container.</summary>
    Type Type { get; }
}