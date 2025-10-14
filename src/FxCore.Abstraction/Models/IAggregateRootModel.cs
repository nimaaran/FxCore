// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a contract for defining and identifying aggregate root types.
/// </summary>
public interface IAggregateRootModel : IEntityModel
{
    /// <summary>
    /// Gets the aggregate key.
    /// </summary>
    AggregateLock Lock { get; }
}

/// <summary>
/// Defines a contract for defining aggregate root types.
/// </summary>
/// <typeparam name="TId">Type of the aggregate root id.</typeparam>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
public interface IAggregateRootModel<TId, TKey> : IAggregateRootModel, IEntityModel<TId>
    where TId : notnull
    where TKey : notnull, IAggregateKey
{
    /// <summary>
    /// Gets the aggregate key.
    /// </summary>
    TKey Key { get; }
}
