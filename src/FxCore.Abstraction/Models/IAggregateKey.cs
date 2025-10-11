namespace FxCore.Abstraction.Models;

public interface IAggregateKey;

public interface IAggregateKey<out TValue> : IAggregateKey
    where TValue : notnull
{
    TValue Value { get; }
}
