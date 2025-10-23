// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Common.Services.Contracts;

/// <summary>
/// Defines a contract for those services that provide date and time functionalities. For testing
/// purpose, developers should implement their own date and time service providers for mocking by
/// implementing this contract.
/// </summary>
public interface IDateTimeService : IService
{
    /// <summary>
    /// See <see cref="DateTimeOffset.UtcNow"/>.
    /// </summary>
    /// <returns>The current date and time of the system in UTC.</returns>
    DateTimeOffset UtcNow();
}
