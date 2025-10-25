// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Events.Passports;

/// <summary>
/// Defines an event model for when a new passport is issued.
/// </summary>
public sealed record class PassportIssued : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PassportIssued"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="passportKey">See <see cref="PassportKey"/>.</param>
    /// <param name="accountKey">See <see cref="AccountKey"/>.</param>
    /// <param name="identity">See <see cref="Identity"/>.</param>
    /// <param name="type">See <see cref="Type"/>.</param>
    /// <param name="state">See <see cref="State"/>.</param>
    public PassportIssued(
        IEventDependenciesProvider dependencies,
        PassportKey passportKey,
        AccountKey accountKey,
        string identity,
        PassportTypes type,
        PassportStates state)
        : base(dependencies)
    {
        this.PassportKey = passportKey;
        this.AccountKey = accountKey;
        this.Identity = identity;
        this.Type = type;
        this.State = state;
    }

    /// <summary>
    /// Gets the relevant passport key.
    /// </summary>
    public PassportKey PassportKey { get; private set; } = new(string.Empty);

    /// <summary>
    /// Gets the relevant account key.
    /// </summary>
    public AccountKey AccountKey { get; private set; } = new(string.Empty);

    /// <summary>
    /// Gets the passport identity.
    /// </summary>
    public string Identity { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the passport type.
    /// </summary>
    public PassportTypes Type { get; private set; } = PassportTypes.BASIC;

    /// <summary>
    /// Gets the passport state.
    /// </summary>
    public PassportStates State { get; private set; } = PassportStates.DEACTIVATED;
}
