using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Raiqub.ImplicitGenerics;

/// <summary>Provides extension methods for <see cref="IOutParam{T}"/> objects.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class OutParamExtensions
{
    /// <summary>Displays a debug message if <paramref name="outType"/> is null.</summary>
    /// <param name="outType">The instance to validate as non-null.</param>
    /// <typeparam name="T">The generic type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Conditional("DEBUG")]
    public static void DebugIfNull<T>([NotNull] this IOutParam<T>? outType)
    {
        Debug.Assert(outType is not null);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="outType"/> is null.
    /// </summary>
    /// <param name="outType">The instance to validate as non-null.</param>
    /// <param name="paramName">The name of the parameter with which argument corresponds.</param>
    /// <typeparam name="T">The generic type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull<T>(
        [NotNull] this IOutParam<T>? outType,
        [CallerArgumentExpression("outType")] string? paramName = null)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(outType, paramName);
#else
        if (outType is null)
            Throw(paramName);
#endif
    }

#if !NET6_0_OR_GREATER
    [DoesNotReturn]
    private static void Throw(string? paramName) =>
        throw new ArgumentNullException(paramName);
#endif
}