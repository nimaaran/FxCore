// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Services.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Passports;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a contract for secret data encoders.
/// </summary>
/// <typeparam name="TSecret">Type of the passport secret.</typeparam>
public interface ISecretEncoder<TSecret> : IDomainService
    where TSecret : Secret<TSecret>
{
    /// <summary>
    /// Encodes a secret data.
    /// </summary>
    /// <param name="secret">The raw secret data.</param>
    /// <returns>Encoded secret data.</returns>
    string Encode(string secret);
}
