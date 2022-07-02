namespace Raiqub.Any;

public static class Any
{
    public static IAny<T> Value<T>() => new Any<T>();
}

internal sealed class Any<T> : IAny<T>
{
    public Type Type => typeof(T);
}