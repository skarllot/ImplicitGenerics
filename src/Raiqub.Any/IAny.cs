namespace Raiqub.Any;

public interface IAny<out T>
{
    Type Type { get; }
}