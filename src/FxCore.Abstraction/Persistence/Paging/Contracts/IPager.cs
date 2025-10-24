// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Sorting.Contracts;

namespace FxCore.Abstraction.Persistence.Paging.Contracts;

/// <summary>
/// Defines a contract for all kinds of query result pagers.
/// </summary>
/// <typeparam name="TModel">The type of the query result.</typeparam>
public interface IPager<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Gets the count of records that each page could have.
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// Gets the index of a page that its records should be returned.
    /// </summary>
    int PageIndex { get; }

    /// <summary>
    /// Gets a sorter object that indicates how the result should be ordered before paging.
    /// </summary>
    ISorter<TModel> Sorter { get; }
}
