// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Services.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Passports;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a contract for secret generators.
/// </summary>
/// <typeparam name="TSecret">Type of the passport secret.</typeparam>
public interface ISecretGenerator<TSecret> : IDomainService
    where TSecret : Secret<TSecret>
{
    /// <summary>
    /// Generates a new raw secret data.
    /// </summary>
    /// <returns>The raw secret data.</returns>
    string Generate();
}
