// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a data context object that provides data access services to repositories.
/// </summary>
public interface IDataContext
{
    /// <summary>
    /// Gets the data set for the specified model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
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
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to add.</param>
    void Create<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <summary>
    /// Updates an entity in the context.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to update.</param>
    void Update<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <summary>
    /// Removes an entity from the context.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to remove.</param>
    void Delete<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

    /// <summary>
    /// Queries the context for a result.
    /// </summary>
    /// <typeparam name="TModel">The type of the data model.</typeparam>
    /// <param name="query">The query to execute.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The result of the query.</returns>
    Task<List<TModel>> ReadAsync<TModel>(IQueryable<TModel> query, CancellationToken token)
        where TModel : class, IDataModel;

    /// <summary>
    /// Saves the changes to the context.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The number of changes saved.</returns>
    Task<int> SaveChangesAsync(CancellationToken token = default);
}
