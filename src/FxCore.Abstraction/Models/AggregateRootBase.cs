using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

public abstract class AggregateRootBase<TId, TKey>(
    TId id,
    TKey key,
    bool removed,
    AggregateLock @lock)
    : EntityBase<TId>(id, removed), IAggregateRootModel<TId, TKey>
    where TId : notnull
    where TKey : notnull, IAggregateKey
{
    public TKey Key { get; private set; } = key;

    public AggregateLock Lock { get; private set; } = @lock;

    protected void UpdateLock(int count, DateTimeOffset lastChangeTimestamp)
        => this.Lock = this.Lock.Update(count, lastChangeTimestamp);
}
