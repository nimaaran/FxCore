// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;
using FxCore.Abstraction.Entities;
using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Aggregates;

/// <summary>
/// As a base class, it will be used for implementing aggregate roots.
/// </summary>
/// <typeparam name="TId">Type of the aggregate root Id.</typeparam>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
/// <param name="id">See <see cref="IEntity{TId}.Id"/>.</param>
/// <param name="key">See <see cref="Key"/>.</param>
/// <param name="removed">See <see cref="IEntity.Removed"/>.</param>
/// <param name="lock">See <see cref="Lock"/>.</param>
public abstract class AggregateRootBase<TId, TKey>(
    TId id,
    TKey key,
    bool removed,
    AggregateLock @lock) : EntityBase<TId>(id, removed), IAggregateRoot<TId, TKey>
    where TId : notnull
    where TKey : notnull, IAggregateKey
{
    /// <summary>
    /// Gets the aggregate key.
    /// </summary>
    public TKey Key { get; private set; } = key;

    /// <summary>
    /// Gets the aggregate lock.
    /// </summary>
    public AggregateLock Lock { get; private set; } = @lock;

    /// <summary>
    /// It should be called after every change in the aggregate to keep the lock updated.
    /// </summary>
    /// <param name="count">The number of changes that happened.</param>
    /// <param name="lastChangeTimestamp">The last change date and time.</param>
    protected void UpdateLock(int count, DateTimeOffset lastChangeTimestamp)
        => this.Lock = this.Lock.Update(count, lastChangeTimestamp);
}
