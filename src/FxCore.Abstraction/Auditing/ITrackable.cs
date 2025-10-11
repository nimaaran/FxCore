namespace FxCore.Abstraction.Auditing;

public interface ITrackable
{
    string TrackingKey { get; }
    DateTimeOffset Timestamp { get; }
}
