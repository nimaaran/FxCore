// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a base class for defining aggregate root classes.
/// </summary>
/// <typeparam name="TId">Type of the aggregate root Id.</typeparam>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
/// <param name="id">The aggregate root id.</param>
/// <param name="key">The aggregate key.</param>
/// <param name="removed">A flag indicating whether the aggregate is removed or not.</param>
/// <param name="lock">The aggregate lock object.</param>
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
    /// Gets the aggregate lock object.
    /// </summary>
    public AggregateLock Lock { get; private set; } = @lock;

    /// <summary>
    /// It should be called after changes in the aggregate to update the aggregate lock.
    /// </summary>
    /// <param name="count">The number of changes that happened.</param>
    /// <param name="lastChangeTimestamp">The last change date and time.</param>
    protected void UpdateLock(int count, DateTimeOffset lastChangeTimestamp)
        => this.Lock = this.Lock.Update(count, lastChangeTimestamp);
}
