// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Events.Accounts;

/// <summary>
/// Defines an event model for when an account is registered.
/// </summary>
public sealed record AccountRegistered : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountRegistered"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="accountKey">The relevant account aggregate key.</param>
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
    /// Gets the relevant account aggregate key.
    /// </summary>
    public AccountKey AccountKey { get; }

    /// <summary>
    /// Gets the account display name.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Gets the type of the account.
    /// </summary>
    public AccountTypes Type { get; }

    /// <summary>
    /// Gets the state of the account.
    /// </summary>
    public AccountStates State { get; }
}
