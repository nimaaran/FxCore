// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Entities;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Defines a class for any kind of passport secrets.
/// </summary>
/// <typeparam name="TSecret">Type of the passport secret.</typeparam>
public abstract class Secret<TSecret> : EntityBase<long>, ISecret
    where TSecret : Secret<TSecret>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Secret{TSecret}"/> class.
    /// </summary>
    protected Secret()
        : base(id: default, removed: false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret{TSecret}"/> class.
    /// </summary>
    /// <param name="encodedValue">See <see cref="EncodedValue"/>.</param>
    /// <param name="expireDate">See <see cref="ExpireDate"/>.</param>
    /// <param name="type">See <see cref="Type"/>.</param>
    protected Secret(string encodedValue, DateTimeOffset expireDate, SecretTypes type)
        : this()
    {
        this.EncodedValue = encodedValue;
        this.ExpireDate = expireDate;
        this.Type = type;
    }

    /// <summary>
    /// Gets an encoded secret value.
    /// </summary>
    public string EncodedValue { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the expire date of the secret data.
    /// </summary>
    public DateTimeOffset ExpireDate { get; private set; } = DateTimeOffset.MinValue;

    /// <summary>
    /// Gets the secret type.
    /// </summary>
    public SecretTypes Type { get; private set; } = SecretTypes.PASSWORD;

    /// <summary>
    /// Removes the passport secret.
    /// </summary>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    internal new Result Remove()
    {
        return base.Remove();
    }
}
