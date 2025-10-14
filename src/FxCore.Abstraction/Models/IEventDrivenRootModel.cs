using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

public interface IEventDrivenRootModel : IAggregateRootModel
{
    IReadOnlyCollection<IDomainEventModel> UncommittedEvents { get; }

    Result Commit(AggregateLock currentLock);

    Result Rehydrate(string snapshot, IEnumerable<IDomainEventModel> events);
}
