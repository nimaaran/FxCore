// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Persistence.Repositories.Contracts;

/// <summary>
/// Defines a contract for those repository methods that mark entities in the data context for
/// removing from the data source.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
public interface IRecordRemoverRepository<in TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Marks the object in the data context for removing.
    /// </summary>
    /// <param name="object">The object that should be tracked and removed.</param>
    void Delete(TEntity @object);
}
