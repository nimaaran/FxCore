// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Implements a base class for specification objects.
/// </summary>
/// <typeparam name="TModel">The type of the data model.</typeparam>
public abstract class SpecificationBase<TModel> : ISpecification<TModel>
    where TModel : class, IDataModel
{
    /// <inheritdoc/>
    public ICriterion<TModel>? Criterion { get; protected set; }
}
