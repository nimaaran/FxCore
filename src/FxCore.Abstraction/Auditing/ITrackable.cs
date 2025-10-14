// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Auditing;

/// <summary>
/// Defines a contract for defining trackable and auditable objects.
/// </summary>
public interface ITrackable
{
    /// <summary>
    /// Gets the unique tracking key for the entity.
    /// </summary>
    string TrackingKey { get; }

    /// <summary>
    /// Gets the timestamp indicating when the entity was created or last modified.
    /// </summary>
    DateTimeOffset Timestamp { get; }
}
