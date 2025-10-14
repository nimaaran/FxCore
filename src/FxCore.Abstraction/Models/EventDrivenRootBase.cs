using FxCore.Abstraction.Types;
using System.Collections.ObjectModel;

namespace FxCore.Abstraction.Models;

public abstract class EventDrivenRootBase<TId, TKey>(
    TId id,
    TKey key,
    bool removed,
    AggregateLock @lock)
    : AggregateRootBase<TId, TKey>(id, key, removed, @lock), IEventDrivenRootModel
    where TId : notnull
    where TKey : notnull, IAggregateKey
{
    private readonly List<IDomainEventModel> uncommittedEvents = [];

    IReadOnlyCollection<IDomainEventModel> IEventDrivenRootModel.UncommittedEvents
        => this.uncommittedEvents.AsReadOnly();

    public Result Commit(AggregateLock currentLock)
    {
        if (this.Lock == currentLock)
        {
            if (this.uncommittedEvents.Count == 0)
            {
                ReadOnlyCollection<IDomainEventModel> events = this.uncommittedEvents.AsReadOnly();
                this.uncommittedEvents.Clear();
                this.UpdateLock(events.Count, events[^1].Timestamp);
                return Result.Completed(events);
            }

            return Result.Completed(this.uncommittedEvents);
        }

        return Result.Terminated(
            code: ResultCodes.INCONSISTENCY,
            message: "The optimistic lock has been changed.");
    }

    public Result Rehydrate(string snapshot, IEnumerable<IDomainEventModel> events)
    {
        if (!string.IsNullOrWhiteSpace(snapshot))
        {
            // TODO: Not implemented yet.
            throw new NotImplementedException();
        }

        foreach (var @event in events)
        {
            var result = this.ApplyEvent(@event, false);

            if (result.State is not ResultStates.COMPLETED)
            {
                return result;
            }
        }

        return Result.Completed();
    }

    protected virtual Result DispatchEvent(IDomainEventModel @event)
    {
        return Result.Terminated(
            code: ResultCodes.BAD_REQUEST,
            message: "The event is not supported in this context.");
    }

    protected Result ApplyEvent(IDomainEventModel @event, bool isNew)
    {
        if (@event is null)
        {
            return Result.Terminated(
                code: ResultCodes.BAD_REQUEST,
                message: "The event object is null.");
        }

        var result = this.DispatchEvent(@event);

        if (isNew && result.State is ResultStates.COMPLETED)
        {
            this.uncommittedEvents.Add(@event);
        }
        else
        {
            // An event that has already been applied in the aggregate is unable to be applied
            // again to reproduce the aggregate state. In this case, probably something has
            // changed in the aggregate that prevents the old events from being applied again.
            return Result.Terminated(
                code: ResultCodes.INCONSISTENCY,
                message: "At least one of the events cannot be reapplied.");
        }

        return result;
    }
}
