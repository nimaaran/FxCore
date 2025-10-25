// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Services.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Passports;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a contract for secret evaluators.
/// </summary>
/// <typeparam name="TSecret">Type of the passport secret.</typeparam>
public interface ISecretEvaluator<TSecret> : IDomainService
    where TSecret : Secret<TSecret>
{
    /// <summary>
    /// Evaluates the secret data compared to original secret attributes.
    /// </summary>
    /// <param name="secret">A secret data that should be evaluated.</param>
    /// <param name="originalEncodedSecret">The original encoded secret data.</param>
    /// <param name="expireDate">The original secret expire date.</param>
    /// <returns>
    /// <see langword="true"/> means the decret data is valid; othrwise <see langword="false"/>.
    /// </returns>
    bool Evaluate(string secret, string originalEncodedSecret, DateTimeOffset expireDate);
}
