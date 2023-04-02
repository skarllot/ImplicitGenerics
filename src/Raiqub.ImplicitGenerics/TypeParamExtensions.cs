using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Raiqub.ImplicitGenerics;

/// <summary>Provides extension methods for <see cref="ITypeParam{T}"/> objects.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class TypeParamExtensions
{
#pragma warning disable CS8777 // Parameter must have a non-null value when exiting
    /// <summary>Displays a debug message if <paramref name="type"/> is null.</summary>
    /// <param name="type">The instance to validate as non-null.</param>
    /// <typeparam name="T">The generic type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Conditional("DEBUG")]
    [ContractAnnotation("type:null=>halt")]
    public static void DebugIfNull<T>([NotNull] this ITypeParam<T>? type)
    {
        Debug.Assert(type is not null);
    }
#pragma warning restore CS8777

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="type"/> is null.
    /// </summary>
    /// <param name="type">The instance to validate as non-null.</param>
    /// <param name="paramName">The name of the parameter with which argument corresponds.</param>
    /// <typeparam name="T">The generic type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [ContractAnnotation("type:null=>halt")]
    public static void ThrowIfNull<T>(
        [NotNull] this ITypeParam<T>? type,
        [CallerArgumentExpression("type")] string? paramName = null)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(type, paramName);
#else
        if (type is null)
            Throw(paramName);
#endif
    }

#if !NET6_0_OR_GREATER
    [DoesNotReturn]
    private static void Throw(string? paramName) =>
        throw new ArgumentNullException(paramName);
#endif
}