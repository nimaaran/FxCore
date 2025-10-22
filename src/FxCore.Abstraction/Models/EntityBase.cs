// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a base class for defining entity classes.
/// </summary>
/// <typeparam name="TId">Type of the entity id.</typeparam>
/// <param name="id">The id of an object.</param>
/// <param name="removed">A flag indicating whether the object is removed or not.</param>
public abstract class EntityBase<TId>(TId id, bool removed) : IEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets the Id of the object.
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
