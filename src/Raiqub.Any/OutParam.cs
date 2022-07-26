namespace Raiqub.Any;

internal sealed class OutParam<T> : IOutParam<T>
{
    public static readonly OutParam<T> Instance = new();

    public Type Type => typeof(T);
}