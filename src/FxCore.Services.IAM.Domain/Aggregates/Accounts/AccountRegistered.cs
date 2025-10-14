using FxCore.Abstraction.Models;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed record AccountRegistered(
    string TrackingKey,
    DateTimeOffset Timestamp,
    AccountKey AccountKey,
    string DisplayName,
    AccountTypes AccountType,
    AccountStates AccountState) : IDomainEventModel;

