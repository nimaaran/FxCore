// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Services;

/// <summary>
/// Defines a contract for a date-time service that provides date and time functionalities. For
/// testing purpose developers can implement their date and time service providers for mocking.
/// </summary>
public interface IDateTimeService : IService
{
    /// <summary>
    /// Normally this method is a wrapper for <see cref="DateTimeOffset.Now"/>.
    /// </summary>
    /// <returns>The current date and time.</returns>
    DateTimeOffset Now();
}
