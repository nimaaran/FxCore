// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Types;
using System.Collections.ObjectModel;

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a base class for those aggregate root classes that are event-driven.
/// </summary>
/// <typeparam name="TId">Type of the aggregate root id.</typeparam>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
/// <param name="id">The id of the aggregate root object.</param>
/// <param name="key">The aggregate key.</param>
/// <param name="removed">A flag indicating whether the aggregate is removed or not.</param>
/// <param name="lock">The aggregate lock object.</param>
public abstract class EventDrivenRootBase<TId, TKey>(
    TId id,
    TKey key,
    bool removed,
    AggregateLock @lock)
    : AggregateRootBase<TId, TKey>(id, key, removed, @lock), IEventDrivenRoot
    where TId : notnull
    where TKey : notnull, IAggregateKey
{
    private readonly List<IDomainEvent> uncommittedEvents = [];

    /// <summary>
    /// Gets a readonly list of uncommited events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> IEventDrivenRoot.UncommittedEvents
        => this.uncommittedEvents.AsReadOnly();

    /// <summary>
    /// Commits uncommitted events.
    /// </summary>
    /// <param name="currentLock">The aggregate lock before committing.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Commit(AggregateLock currentLock)
    {
        if (this.Lock == currentLock)
        {
            if (this.uncommittedEvents.Count == 0)
            {
                ReadOnlyCollection<IDomainEvent> events = this.uncommittedEvents.AsReadOnly();
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

    /// <summary>
    /// Reloads an aggregate by using an snapshot.
    /// </summary>
    /// <param name="snapshot">An aggregate snapshot in JSON format.</param>
    /// <param name="events">Recent events that happened after latest snapshot creation.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Rehydrate(string snapshot, IEnumerable<IDomainEvent> events)
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

    /// <summary>
    /// Delivers an event to the proper handler method. This method should be overridden in
    /// inherited classes.
    /// </summary>
    /// <param name="event">An event that should be dispatched.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected virtual Result DispatchEvent(IDomainEvent @event)
    {
        return Result.Terminated(
            code: ResultCodes.BAD_REQUEST,
            message: "The event is not supported in this context.");
    }

    /// <summary>
    /// Applies an event by dispatching it and updating the aggregate state.
    /// </summary>
    /// <param name="event">The event that should be processed.</param>
    /// <param name="isNew">A flag indicating whether the event is new or we are reapplying it.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result ApplyEvent(IDomainEvent @event, bool isNew)
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
