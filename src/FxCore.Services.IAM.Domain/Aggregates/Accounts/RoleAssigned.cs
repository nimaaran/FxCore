using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed record RoleAssigned(
    string TrackingKey,
    DateTimeOffset Timestamp,
    AccountKey AccountKey,
    RoleKey RoleKey) : IDomainEventModel;
