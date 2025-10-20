// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Services;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a contract for secret data encoders.
/// </summary>
public interface ISecretEncoder : IDomainService
{
    /// <summary>
    /// Encodes a secret data.
    /// </summary>
    /// <param name="secret">The raw secret data.</param>
    /// <returns>Encoded secret data.</returns>
    string Encode(string secret);
}
