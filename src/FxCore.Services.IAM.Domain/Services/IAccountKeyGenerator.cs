using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Services;

public interface IAccountKeyGenerator<TAggregateRootModel>
    : IAggregateKeyGenerator<TAggregateRootModel, AccountKey>
    where TAggregateRootModel : IAggregateRootModel;
