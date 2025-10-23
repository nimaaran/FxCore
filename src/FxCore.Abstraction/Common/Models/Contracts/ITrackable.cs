// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Common.Models.Contracts;

/// <summary>
/// Defines the required attributes and behaviors of trackable objects.
/// </summary>
public interface ITrackable
{
    /// <summary>
    /// Gets the unique tracking key for the object.
    /// </summary>
    string TrackingKey { get; }

    /// <summary>
    /// Gets the timestamp indicating when the object was created or modified last time.
    /// </summary>
    DateTimeOffset Timestamp { get; }
}
