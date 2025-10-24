// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Entities.Contracts;
using FxCore.Abstraction.Persistence.DataContexts.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FxCore.Extensions.EF;

/// <summary>
/// As a base class, it defines and implements the required attributes and behaviors of EF-based
/// data contexts.
/// </summary>
public abstract class DataContextBase : DbContext, IDataContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataContextBase"/> class.
    /// </summary>
    public DataContextBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataContextBase"/> class.
    /// </summary>
    /// <param name="options"><inheritdoc/></param>
    public DataContextBase(DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public virtual IReadOnlyCollection<IDataModel> GetTrackedObject()
    {
        var list = new List<IDataModel>();
        var entries = this.ChangeTracker
           .Entries()
           .Select(x => x.Entity)
           .ToList();

        foreach (var entry in entries)
        {
            list.Add((IDataModel)entry);
        }

        return list.AsReadOnly();
    }

    /// <inheritdoc/>
    public virtual IQueryable<TModel> GetModel<TModel>()
        where TModel : class, IDataModel
    {
        return this.Set<TModel>().AsQueryable();
    }

    /// <inheritdoc/>
    public virtual void Create<TEntity>(TEntity entity)
        where TEntity : class, IEntity
    {
        this.Entry(entity).State = EntityState.Added;
    }

    /// <inheritdoc/>
    public virtual void Delete<TEntity>(TEntity entity)
        where TEntity : class, IEntity
    {
        this.Entry(entity).State = EntityState.Deleted;
    }

    /// <inheritdoc/>
    public new virtual void Update<TEntity>(TEntity entity)
        where TEntity : class, IEntity
    {
        this.Entry(entity).State = EntityState.Modified;
    }

    /// <inheritdoc/>
    public virtual Task<List<TModel>> ReadAsync<TModel>(
        IQueryable<TModel> query,
        CancellationToken cancellationToken)
        where TModel : class, IDataModel
    {
        return query.ToListAsync<TModel>(cancellationToken);
    }
}
