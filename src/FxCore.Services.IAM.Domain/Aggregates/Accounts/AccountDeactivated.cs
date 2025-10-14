using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed record AccountDeactivated(
    string TrackingKey,
    DateTimeOffset Timestamp,
    AccountKey AccountKey) : IDomainEventModel;
