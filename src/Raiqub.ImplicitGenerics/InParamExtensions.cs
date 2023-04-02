using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Raiqub.ImplicitGenerics;

/// <summary>Provides extension methods for <see cref="IInParam{T}"/> objects.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class InParamExtensions
{
#pragma warning disable CS8777 // Parameter must have a non-null value when exiting
    /// <summary>Displays a debug message if <paramref name="inType"/> is null.</summary>
    /// <param name="inType">The instance to validate as non-null.</param>
    /// <typeparam name="T">The generic type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Conditional("DEBUG")]
    [ContractAnnotation("inType:null=>halt")]
    public static void DebugIfNull<T>([NotNull] this IInParam<T>? inType)
    {
        Debug.Assert(inType is not null);
    }
#pragma warning restore CS8777

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="inType"/> is null.
    /// </summary>
    /// <param name="inType">The instance to validate as non-null.</param>
    /// <param name="paramName">The name of the parameter with which argument corresponds.</param>
    /// <typeparam name="T">The generic type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [ContractAnnotation("inType:null=>halt")]
    public static void ThrowIfNull<T>(
        [NotNull] this IInParam<T>? inType,
        [CallerArgumentExpression("inType")] string? paramName = null)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(inType, paramName);
#else
        if (inType is null)
            Throw(paramName);
#endif
    }

#if !NET6_0_OR_GREATER
    [DoesNotReturn]
    private static void Throw(string? paramName) =>
        throw new ArgumentNullException(paramName);
#endif
}