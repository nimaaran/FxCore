// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Persistence.Specifications;

/// <summary>
/// Represents a criterion object that configs the query to return only active (non-removed)
/// records.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
public sealed class OnlyActiveRecords<TEntity> : Criterion<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OnlyActiveRecords{TEntity}"/> class.
    /// </summary>
    public OnlyActiveRecords() => this.Set(r => !r.Removed);
}
