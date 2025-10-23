// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Services.Contracts;

namespace FxCore.Abstraction.Aggregates.Contracts;

/// <summary>
/// Defines a contract for sync aggregate key generators.
/// </summary>
/// <typeparam name="TRoot">Type of the aggregate root.</typeparam>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
public interface IAggregateKeyGenerator<TRoot, TKey> : IDomainService
    where TRoot : IAggregateRoot<TKey>
    where TKey : IAggregateKey
{
    /// <summary>
    /// Generates a new aggregate key.
    /// </summary>
    /// <returns>The generated aggregate key.</returns>
    TKey Generate();
}
