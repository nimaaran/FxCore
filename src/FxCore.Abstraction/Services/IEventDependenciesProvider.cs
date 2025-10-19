// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Services;

/// <summary>
/// Defines a contract for dependencies that are required for creating events.
/// </summary>
public interface IEventDependenciesProvider : IDomainService
{
    /// <summary>
    /// Gets the tracking key generator service.
    /// </summary>
    ITrackingKeyGenerator TrackingKeyGenerator { get; }

    /// <summary>
    /// Gets the date time service.
    /// </summary>
    IDateTimeService DateTimeService { get; }
}
