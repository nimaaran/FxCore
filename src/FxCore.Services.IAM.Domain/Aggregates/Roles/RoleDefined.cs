using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

public sealed record RoleDefined(
    string TrackingKey, 
    DateTimeOffset Timestamp,
    RoleKey RoleKey,
    string RoleTitle,
    bool TwoFactorRequired,
    RoleTypes RoleType,
    RoleStates RoleState) 
    : IDomainEventModel;
