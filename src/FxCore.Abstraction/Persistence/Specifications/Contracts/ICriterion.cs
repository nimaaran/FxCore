// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using System.Linq.Expressions;

namespace FxCore.Abstraction.Persistence.Specifications.Contracts;

/// <summary>
/// Defines a contract for criteria that will be used to make queries.
/// </summary>
/// <typeparam name="TModel">Type of the data model.</typeparam>
public interface ICriterion<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Sets an expression as the main condition. It is necessary to create a query.
    /// </summary>
    /// <param name="condition">The main condition expression.</param>
    /// <returns>The criterion object.</returns>
    ICriterion<TModel> Set(Expression<Func<TModel, bool>> condition);

    /// <summary>
    /// Combines the main condition with an additional criterion by using OR operator.
    /// </summary>
    /// <param name="criterion">The additional condition.</param>
    /// <returns>The criterion object after modification.</returns>
    ICriterion<TModel> Or(ICriterion<TModel> criterion);

    /// <summary>
    /// Combines the main condition with an additional criterion by using AND operator.
    /// </summary>
    /// <param name="criterion">The additional condition.</param>
    /// <returns>The criterion object after modification.</returns>
    ICriterion<TModel> And(ICriterion<TModel> criterion);

    /// <summary>
    /// Converts the criterion to an expression that is created by combining the main condition
    /// and optional additional criteria.
    /// </summary>
    /// <returns>The equivalent condition expression.</returns>
    Expression<Func<TModel, bool>> Export();
}
