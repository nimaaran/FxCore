// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Services.Contracts;

namespace FxCore.Abstraction.Common.Models.Contracts;

/// <summary>
/// Defines a contract for keys generators of <see cref="ITrackable"/> objects.
/// </summary>
public interface ITrackingKeyGenerator : IService
{
    /// <summary>
    /// Generates a new tracking key.
    /// </summary>
    /// <returns>The generated tracking key.</returns>
    string Generate();
}
