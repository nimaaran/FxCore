// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events.Contracts;

namespace FxCore.Abstraction.Persistence.DataContexts.Contracts;

/// <summary>
/// Desines a contract for event-driven transaction managers according to the unity-of-work
/// pattern.
/// </summary>
public interface IEventDrivenTransactionContext
{
    /// <summary>
    /// Commits the transaction.
    /// </summary>
    /// <param name="token">See <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// An async operation that returns a tupple that contains number of affected objects and
    /// applied events in the transaction.
    /// </returns>
    Task<(int AffectedObjects, List<IDomainEvent> Events)> CommitAsync(CancellationToken token);
}
