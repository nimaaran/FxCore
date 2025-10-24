// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using System.Linq.Expressions;

namespace FxCore.Abstraction.Persistence.Sorting.Contracts;

/// <summary>
/// Defines a contract for all kinds of query result sorters.
/// </summary>
public interface ISorter;

/// <summary>
/// Defines a contract for all kinds of query result sorters.
/// </summary>
/// <typeparam name="TModel">The type of the query result.</typeparam>
public interface ISorter<TModel> : ISorter
    where TModel : class, IDataModel
{
    /// <summary>
    /// Gets a value indicating whether the result should be sorted in the ascending order or
    /// not (descending order).
    /// </summary>
    bool Ascending { get; }

    /// <summary>
    /// Gets an expression that indicates which column should be used for sorting.
    /// </summary>
    Expression<Func<TModel, object>> Column { get; }

    /// <summary>
    /// Gets a sorter object that should be applied over the query result when more than one column
    /// should be used in the sorting.
    /// </summary>
    ISorter<TModel>? Next { get; }

    /// <summary>
    /// Sets the next column that should be used for sorting.
    /// </summary>
    /// <param name="next">The next sorter object.</param>
    /// <returns>The complete sorting configuration object.</returns>
    ISorter<TModel> ThenBy(ISorter<TModel> next);
}
