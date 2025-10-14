namespace FxCore.Abstraction.Models;

public abstract record class AggregateKeyBase<TValue>(TValue Value) 
    : IAggregateKey<TValue>
    where TValue : notnull;
