using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

public sealed record RoleRemoved(
    string TrackingKey,
    DateTimeOffset Timestamp,
    RoleKey RoleKey)
    : IDomainEventModel;
