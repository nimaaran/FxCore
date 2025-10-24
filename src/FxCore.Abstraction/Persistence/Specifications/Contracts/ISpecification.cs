// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Persistence.Specifications.Contracts;

/// <summary>
/// Defines a contract for domain specifications.
/// </summary>
/// <typeparam name="TModel">Type of the data model.</typeparam>
public interface ISpecification<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Gets a criterion that should be used to define the specification.
    /// </summary>
    ICriterion<TModel>? Criterion { get; }
}
