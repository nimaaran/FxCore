// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Services.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Passports;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a generic contract for dynamic secret builders.
/// </summary>
/// <typeparam name="TSecret">Type of the desired secret type.</typeparam>
public interface ISecretBuilder<TSecret> : IDomainService
    where TSecret : Secret<TSecret>
{
    /// <summary>
    /// Builds a secret object as the specified concrete secret type.
    /// </summary>
    /// <param name="secretValue">A raw value that should be used for building secret.</param>
    /// <returns>A secret object as the desired type.</returns>
    ISecret<TSecret> Build(string secretValue);
}
