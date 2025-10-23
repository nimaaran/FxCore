// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Requests.Contracts;

/// <summary>
/// Defines a contract for defining query requests.
/// </summary>
public interface IQuery : IRequest
{
    /// <summary>
    /// Gets a value indicating how many records should be skipped from the start of top of the
    /// list.
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Gets a value indicating how many records should be taken from the list.
    /// </summary>
    int Take { get; }
}
