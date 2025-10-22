// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a repository object that can create a record.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRecordCreatorRepository<in TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Sets the object in the data context for creating.
    /// </summary>
    /// <param name="object">The object that should be tracked and created.</param>
    void Create(TEntity @object);
}
