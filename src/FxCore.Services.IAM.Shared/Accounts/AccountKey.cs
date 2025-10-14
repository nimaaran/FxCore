using FxCore.Abstraction.Models;

namespace FxCore.Services.IAM.Shared.Accounts;

public record class AccountKey(string Value) : AggregateKeyBase<string>(Value);
