// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Entities.Contracts;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Defines a marker interface for identifying secret models.
/// </summary>
public interface ISecret : IEntity
{
    /// <summary>
    /// Gets an encoded secret value.
    /// </summary>
    string EncodedValue { get; }

    /// <summary>
    /// Gets the expire date of the secret data.
    /// </summary>
    DateTimeOffset ExpireDate { get; }

    /// <summary>
    /// Gets the secret type.
    /// </summary>
    SecretTypes Type { get; }
}

/// <summary>
/// Defines a marker interface for identifying secret models.
/// </summary>
/// <typeparam name="TSecret">Type of the passport secret.</typeparam>
public interface ISecret<TSecret> : ISecret
    where TSecret : Secret<TSecret>;
