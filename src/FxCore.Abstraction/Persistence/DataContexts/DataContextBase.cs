// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Entities.Contracts;

using FxCore.Abstraction.Persistence.DataContexts.Contracts;

namespace FxCore.Abstraction.Persistence.DataContexts;

/// <summary>
/// As a base class, it implements the common attributes and behaviors of data contexts.
/// </summary>
public abstract class DataContextBase : IDataContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataContextBase"/> class.
    /// </summary>
    protected DataContextBase()
    {
    }

    /// <inheritdoc/>
    public abstract IReadOnlyCollection<IDataModel> GetTrackedObject();

    /// <inheritdoc/>
    public abstract Task<int> SaveChangesAsync(CancellationToken token = default);

    /// <inheritdoc/>
    public abstract void Create<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <inheritdoc/>
    public abstract void Delete<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <inheritdoc/>
    public abstract IQueryable<TModel> GetModel<TModel>()
        where TModel : class, IDataModel;

    /// <inheritdoc/>
    public abstract Task<List<TModel>> ReadAsync<TModel>(
        IQueryable<TModel> query,
        CancellationToken token)
        where TModel : class, IDataModel;

    /// <inheritdoc/>
    public abstract void Update<TEntity>(TEntity entity)
        where TEntity : class, IEntity;
}
