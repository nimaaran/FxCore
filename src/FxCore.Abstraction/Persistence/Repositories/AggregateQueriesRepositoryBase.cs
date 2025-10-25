// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;
using FxCore.Abstraction.Persistence.DataContexts.Contracts;
using FxCore.Abstraction.Persistence.Specifications.Contracts;

namespace FxCore.Abstraction.Persistence.Repositories;

/// <summary>
/// As a base class, it implements the required attributes and behaviors of queries repositories of
/// aggregate roots.
/// </summary>
/// <typeparam name="TRoot">Type of the aggregate root.</typeparam>
public abstract class AggregateQueriesRepositoryBase<TRoot>
    : QueriesRepositoryBase<TRoot>
    where TRoot : class, IAggregateRoot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateQueriesRepositoryBase{TRoot}"/> class.
    /// </summary>
    /// <inheritdoc/>
    protected AggregateQueriesRepositoryBase(
        IDataContext dataContext,
        IQueryBuilder queryBuilder)
        : base(dataContext, queryBuilder)
    {
    }
}
