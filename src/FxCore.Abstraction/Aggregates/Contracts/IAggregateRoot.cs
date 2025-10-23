// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Aggregates.Contracts;

/// <summary>
/// Defines a contract for all kinds of aggregate roots.
/// </summary>
public interface IAggregateRoot : IEntity
{
    /// <summary>
    /// Gets the aggregate lock.
    /// </summary>
    AggregateLock Lock { get; }
}

/// <summary>
/// Defines a contract for all kinds of aggregate roots.
/// </summary>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
public interface IAggregateRoot<TKey> : IAggregateRoot, IEntity
    where TKey : notnull, IAggregateKey
{
    /// <summary>
    /// Gets the aggregate key.
    /// </summary>
    TKey Key { get; }
}

/// <summary>
/// Defines a contract for all kinds of aggregate roots.
/// </summary>
/// <typeparam name="TId">Type of the aggregate root id.</typeparam>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
public interface IAggregateRoot<TId, TKey> : IAggregateRoot<TKey>, IEntity<TId>
    where TId : notnull
    where TKey : notnull, IAggregateKey;
