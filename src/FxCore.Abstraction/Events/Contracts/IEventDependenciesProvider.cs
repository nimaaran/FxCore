// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Common.Services.Contracts;

namespace FxCore.Abstraction.Events.Contracts;

/// <summary>
/// Defines a contract for dependencies that are required for creating events.
/// </summary>
public interface IEventDependenciesProvider : IService
{
    /// <summary>
    /// Gets a tracking key generator.
    /// </summary>
    ITrackingKeyGenerator TrackingKeyGenerator { get; }

    /// <summary>
    /// Gets the date time service provider.
    /// </summary>
    IDateTimeService DateTimeService { get; }
}
