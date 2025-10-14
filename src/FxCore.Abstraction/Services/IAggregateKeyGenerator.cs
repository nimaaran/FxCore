// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Services;

/// <summary>
/// Defines a contract for aggregate key generator service providers.
/// </summary>
/// <typeparam name="TAggregateRootModel">Type of the aggregate root.</typeparam>
/// <typeparam name="TAggregateKey">Type of the aggregate key.</typeparam>
public interface IAggregateKeyGenerator<TAggregateRootModel, TAggregateKey> : IDomainService
    where TAggregateRootModel : IAggregateRootModel
    where TAggregateKey : IAggregateKey
{
    /// <summary>
    /// Generates a new aggregate key.
    /// </summary>
    /// <returns>The generated aggregate key.</returns>
    TAggregateKey Generate();
}
