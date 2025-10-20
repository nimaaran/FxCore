// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
///     Implements a base class for transaction managers.
/// </summary>
/// <param name="dataContext">A data context service provider.</param>
public abstract class TransactionContextBase(IDataContext dataContext) : ITransactionContext
{
    /// <inheritdoc/>
    public virtual async Task<int> CommitAsync(CancellationToken token)
    {
        var trackedObjects = dataContext.GetTrackedObject();

        List<IDomainEventModel> events = [];

        foreach (var obj in trackedObjects)
        {
            if (obj is IEventDrivenRootModel aggregateRoot)
            {
                events.AddRange(aggregateRoot.UncommittedEvents);
            }
        }

        var affectedRowsCount = await dataContext.SaveChangesAsync(token);

        if (affectedRowsCount > 0)
        {
            // TODO: raise the events
            throw new NotImplementedException();
        }

        return affectedRowsCount;
    }
}
