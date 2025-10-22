// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a contract for defining query response models.
/// </summary>
/// <typeparam name="TOutcome">Type of the query list model.</typeparam>
public interface IQueryResponse<TOutcome> : IResponse
{
    /// <summary>
    /// Gets the total count of the items available in the data source.
    /// </summary>
    long Total { get; }

    /// <summary>
    /// Gets a readonly list of the response objects.
    /// </summary>
    IReadOnlyCollection<TOutcome> Result { get; }
}
