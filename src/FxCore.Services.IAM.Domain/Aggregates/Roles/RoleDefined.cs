// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

/// <summary>
/// Defines a domain event model that is raised when a new role is defined in the system.
/// </summary>
/// <param name="TrackingKey">The event tracking key.</param>
/// <param name="Timestamp">The event timestamp.</param>
/// <param name="Key">The role aggregate key.</param>
/// <param name="Title">The role title.</param>
/// <param name="IsSensitive">The role sensitivity flag.</param>
/// <param name="Type">The role type.</param>
/// <param name="State">The role state.</param>
public sealed record RoleDefined(
    string TrackingKey,
    DateTimeOffset Timestamp,
    RoleKey Key,
    string Title,
    bool IsSensitive,
    RoleTypes Type,
    RoleStates State)
    : IDomainEventModel;
