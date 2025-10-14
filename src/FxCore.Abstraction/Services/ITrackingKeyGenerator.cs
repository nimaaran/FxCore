// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Services;

/// <summary>
/// Defines a contract for tracking keys generators.
/// </summary>
/// <remarks>For more info see <see cref="Auditing.ITrackable"/> objects.</remarks>
public interface ITrackingKeyGenerator : IService
{
    /// <summary>
    /// Generates a new tracking key.
    /// </summary>
    /// <returns>The generated tracking key.</returns>
    string Generate();
}
