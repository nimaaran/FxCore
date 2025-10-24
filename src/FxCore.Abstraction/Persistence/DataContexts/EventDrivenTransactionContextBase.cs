// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Abstraction.Persistence.DataContexts.Contracts;

namespace FxCore.Abstraction.Persistence.DataContexts;

/// <summary>
/// Implements a base class for event-driven-based transaction managers.
/// </summary>
public abstract class EventDrivenTransactionContextBase : IEventDrivenTransactionContext
{
    private readonly IDataContext dataContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventDrivenTransactionContextBase"/> class.
    /// </summary>
    /// <param name="dataContext">A data context provider.</param>
    protected EventDrivenTransactionContextBase(IDataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    /// <inheritdoc/>
    public async Task<(int AffectedObjects, List<IDomainEvent> Events)> CommitAsync(
        CancellationToken token)
    {
        var trackedObjects = this.dataContext.GetTrackedObject();

        List<IDomainEvent> events = [];

        var affectedRowsCount = await this.dataContext.SaveChangesAsync(token);

        foreach (var obj in trackedObjects)
        {
            if (obj is IEventDrivenRoot aggregateRoot)
            {
                var commitResult = ((IEventDrivenRoot)obj).Commit(((IEventDrivenRoot)obj).Lock);

                if (commitResult.TryGetOutcome<List<IDomainEvent>>(out List<IDomainEvent>? committedEvents))
                {
                    events.AddRange(committedEvents!);
                }
            }
        }

        return new(affectedRowsCount, events);
    }
}
