namespace FxCore.Abstraction.Types;

public record class AggregateLock(int Version, DateTimeOffset Timestamp)
{
    public static AggregateLock Empty
        => new(Version: default, Timestamp: DateTimeOffset.MinValue);

    public static AggregateLock Create(DateTimeOffset timestamp)
        => new(Version: default, Timestamp: timestamp);

    public AggregateLock Update(int count, DateTimeOffset lastChangeTimestamp)
        => new(Version: Version + count, Timestamp: lastChangeTimestamp);
}
