// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a contract for defining event-driven aggregate root models.
/// </summary>
public interface IEventDrivenRoot : IAggregateRoot
{
    /// <summary>
    /// Gets a readonly list of uncommitted domain events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> UncommittedEvents { get; }

    /// <summary>
    /// Commits the current lock and clears the uncommitted events.
    /// </summary>
    /// <param name="currentLock">The current aggregate lock before committing events.</param>
    /// <returns>An object as type of <see cref="Result"/>.</returns>
    Result Commit(AggregateLock currentLock);

    /// <summary>
    /// Reloads an object by parsing a JSON snapshot and applies newer events after that.
    /// </summary>
    /// <param name="snapshot">An snapshot of the aggregate as type of JSON.</param>
    /// <param name="events">Those event that were committed after generating snapshot.</param>
    /// <returns>An object as type of <see cref="Result"/>.</returns>
    Result Rehydrate(string snapshot, IEnumerable<IDomainEvent> events);
}
