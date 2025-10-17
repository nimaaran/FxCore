// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

/// <summary>
/// Defines an event that is triggered when a role is assigned to an account.
/// </summary>
/// <param name="TrackingKey">The event tracking key.</param>
/// <param name="Timestamp">The event timestamp.</param>
/// <param name="AccountKey">The account aggregate key.</param>
/// <param name="RoleKey">The role aggregate key.</param>
public sealed record RoleAssigned(
    string TrackingKey,
    DateTimeOffset Timestamp,
    AccountKey AccountKey,
    RoleKey RoleKey) : IDomainEventModel;
