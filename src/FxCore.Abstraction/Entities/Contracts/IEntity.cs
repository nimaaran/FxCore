// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Entities.Contracts;

/// <summary>
/// Defines a contract for all entity models.
/// </summary>
public interface IEntity : IDataModel
{
    /// <summary>
    /// Gets a value indicating whether the object is removed or not.
    /// </summary>
    bool Removed { get; }
}

/// <summary>
/// Defines a contract for defining all entity models.
/// </summary>
/// <typeparam name="TId">Type of the entity id.</typeparam>
public interface IEntity<TId> : IEntity
    where TId : notnull
{
    /// <summary>
    /// Gets the entity id.
    /// </summary>
    TId Id { get; }
}
