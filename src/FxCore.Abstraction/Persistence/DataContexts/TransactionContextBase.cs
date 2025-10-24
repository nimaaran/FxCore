// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Persistence.DataContexts.Contracts;

namespace FxCore.Abstraction.Persistence.DataContexts;

/// <summary>
/// Implements a base class for non event-driven-based transaction managers.
/// </summary>
public abstract class TransactionContextBase : ITransactionContext
{
    private readonly IDataContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionContextBase"/> class.
    /// </summary>
    /// <param name="dataContext">A data context provider.</param>
    protected TransactionContextBase(IDataContext dataContext)
    {
        this.context = dataContext;
    }

    /// <inheritdoc/>
    public Task<int> CommitAsync(CancellationToken token)
    {
        return this.context.SaveChangesAsync();
    }
}
