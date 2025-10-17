// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

/// <summary>
/// Defines an event model for account registration.
/// </summary>
/// <param name="TrackingKey">The event tracking key.</param>
/// <param name="Timestamp">The event timestamp.</param>
/// <param name="Key">The account aggregate key.</param>
/// <param name="DisplayName">The account display name.</param>
/// <param name="Type">Type of the account.</param>
/// <param name="State">State of the account.</param>
public sealed record AccountRegistered(
    string TrackingKey,
    DateTimeOffset Timestamp,
    AccountKey Key,
    string DisplayName,
    AccountTypes Type,
    AccountStates State) : IDomainEventModel;
