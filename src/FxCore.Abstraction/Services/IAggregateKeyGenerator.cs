using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Services;

public interface IAggregateKeyGenerator<TAggregateRootModel, TAggregateKey> : IDomainService
    where TAggregateRootModel: IAggregateRootModel
    where TAggregateKey : IAggregateKey
{
    TAggregateKey Generate();
}
