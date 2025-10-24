// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Persistence.DataContexts.Contracts;

/// <summary>
/// Defines a contract for all kinds of data contexts.
/// </summary>
public interface IDataContext
{
    /// <summary>
    /// Gets the relevant data set according to the specified data model.
    /// </summary>
    /// <typeparam name="TModel">The type of the desired model.</typeparam>
    /// <returns>The queryable data set for the specified model.</returns>
    IQueryable<TModel> GetModel<TModel>()
        where TModel : class, IDataModel;

    /// <summary>
    /// Gets the tracked objects in the data context.
    /// </summary>
    /// <returns>A list of tracked objects in the data context.</returns>
    IReadOnlyCollection<IDataModel> GetTrackedObject();

    /// <summary>
    /// Adds an entity to the context.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <param name="entity">The entity that should be added.</param>
    void Create<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <summary>
    /// Updates an entity in the context with a newer version of the object.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <param name="entity">The entity that should be updated.</param>
    void Update<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <summary>
    /// Removes an entity from the context.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <param name="entity">The entity that should be removed.</param>
    void Delete<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <summary>
    /// Queries the context for a result.
    /// </summary>
    /// <typeparam name="TModel">Type of the data model.</typeparam>
    /// <param name="query">The query that should be executed.</param>
    /// <param name="token">See <see cref="CancellationToken"/>.</param>
    /// <returns>The result of the query.</returns>
    Task<List<TModel>> ReadAsync<TModel>(IQueryable<TModel> query, CancellationToken token)
        where TModel : class, IDataModel;

    /// <summary>
    /// Saves the changes to the context.
    /// </summary>
    /// <param name="token">See <see cref="CancellationToken"/>.</param>
    /// <returns>The number of affected objects.</returns>
    Task<int> SaveChangesAsync(CancellationToken token = default);
}
