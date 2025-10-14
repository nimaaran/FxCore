using FxCore.Services.IAM.Domain.Aggregates.Accounts;

namespace FxCore.Services.IAM.Domain.Services;

public interface IMachineAccountKeyGenerator : IAccountKeyGenerator<MachineAccount>;
