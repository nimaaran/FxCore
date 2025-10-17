// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

/// <summary>
/// Defines the event that is raised when an account is activated.
/// </summary>
/// <param name="TrackingKey">The event tracking key.</param>
/// <param name="Timestamp">The event timestamp.</param>
/// <param name="Key">The account aggregate key.</param>
public sealed record AccountActivated(
    string TrackingKey,
    DateTimeOffset Timestamp,
    AccountKey Key) : IDomainEventModel;
