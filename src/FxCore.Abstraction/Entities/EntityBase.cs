// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Entities;

/// <summary>
/// As a base class, it implements the common attributes and behaviors of entity models.
/// </summary>
/// <typeparam name="TId">Type of the entity id.</typeparam>
/// <param name="id">See <see cref="Id"/>.</param>
/// <param name="removed">See <see cref="Removed"/>.</param>
public abstract class EntityBase<TId>(TId id, bool removed) : IEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets the entity id.
    /// </summary>
    public TId Id { get; private set; } = id;

    /// <summary>
    /// Gets a value indicating whether the object is removed or not.
    /// </summary>
    public bool Removed { get; private set; } = removed;

    /// <summary>
    /// Removes the object and set the flag.
    /// </summary>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected virtual Result Remove()
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.Removed = true;

        return Result.Completed();
    }
}
