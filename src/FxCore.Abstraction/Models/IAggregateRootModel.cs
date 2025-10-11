using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

public interface IAggregateRootModel : IEntityModel
{
    AggregateLock Lock { get; }
}

public interface IAggregateRootModel<TId, TKey> : IAggregateRootModel, IEntityModel<TId>
    where TId : notnull
    where TKey : notnull, IAggregateKey
{
    TKey Key { get; }
}
