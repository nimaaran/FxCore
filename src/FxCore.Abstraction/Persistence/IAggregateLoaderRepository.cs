// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a repository object that can load an aggregate by it's key.
/// </summary>
/// <typeparam name="TAggregateRoot">The type of the aggregate root model.</typeparam>
/// <typeparam name="TKey">The type of the aggregate key.</typeparam>
public interface IAggregateLoaderRepository<TAggregateRoot, in TKey>
    where TAggregateRoot : class, IAggregateRoot
    where TKey : IAggregateKey
{
    /// <summary>
    /// Retrieves an aggregate root.
    /// </summary>
    /// <param name="key">The key of the desired aggregate root.</param>
    /// <param name="cancellationToken">See the <see cref="CancellationToken"/>.</param>
    /// <returns>An aggregate root.</returns>
    Task<TAggregateRoot?> LoadAsync(TKey key, CancellationToken cancellationToken);
}
