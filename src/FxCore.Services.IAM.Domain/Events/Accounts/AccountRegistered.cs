// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Events.Accounts;

/// <summary>
/// Defines the event that is raised when an account is registered.
/// </summary>
public sealed record AccountRegistered : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountRegistered"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="accountKey">See <see cref="AccountKey"/>.</param>
    /// <param name="displayName">The account display name.</param>
    /// <param name="type">Type of the account.</param>
    /// <param name="state">State of the account.</param>
    public AccountRegistered(
        IEventDependenciesProvider dependencies,
        AccountKey accountKey,
        string displayName,
        AccountTypes type,
        AccountStates state)
        : base(dependencies)
    {
        this.AccountKey = accountKey;
        this.DisplayName = displayName;
        this.Type = type;
        this.State = state;
    }

    /// <summary>
    /// Gets the relevant account's aggregate key.
    /// </summary>
    public AccountKey AccountKey { get; }

    /// <summary>
    /// Gets the relevant account's display name.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Gets the relevant account's type.
    /// </summary>
    public AccountTypes Type { get; }

    /// <summary>
    /// Gets the relevant account's state.
    /// </summary>
    public AccountStates State { get; }
}
