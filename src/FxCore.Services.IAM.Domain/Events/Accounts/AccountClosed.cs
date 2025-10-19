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
/// Defines an event model for when an account is closed.
/// </summary>
public sealed record AccountClosed : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountClosed"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="accountKey">The relevant account aggregate key.</param>
    public AccountClosed(IEventDependenciesProvider dependencies, AccountKey accountKey)
        : base(dependencies)
    {
        this.AccountKey = accountKey;
    }

    /// <summary>
    /// Gets the relevant account aggregate key.
    /// </summary>
    public AccountKey AccountKey { get; }
}
