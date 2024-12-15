namespace VersionControl.Lib.Changes;
public class HistoryQuery
{
    public HistoryQueryFormat Format { get; }

    public string? FromChangeId { get; }

    public string? ToChangeId { get; }

    public HistoryQuery() : this(null, null, HistoryQueryFormat.Compact)
    {
    }

    public HistoryQuery(string? fromChangeId, string? toChangeId, HistoryQueryFormat format)
    {
        FromChangeId = fromChangeId;
        ToChangeId = toChangeId;
        Format = format;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not HistoryQuery query)
        {
            return false;
        }
        return FromChangeId == query.FromChangeId && ToChangeId == query.ToChangeId && Format == query.Format;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FromChangeId, ToChangeId, Format);
    }
}
