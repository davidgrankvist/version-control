namespace VersionControl.Lib.Changes;

/// <summary>
/// An operation to apply a line based text diff. Line numbers are relative to the state before applying the operation, so operations must be applied in order.
/// </summary>
public readonly struct LineDiffOperation
{
    public LineDiffOperationType OperationType { get; }

    public int Start { get; }

    public int End { get; }

    public IReadOnlyCollection<string> Data { get; }

    public LineDiffOperation(LineDiffOperationType operationType, int start, int end, IReadOnlyCollection<string> data)
    {
        OperationType = operationType;
        Start = start;
        End = end;
        Data = data;
    }

    public LineDiffOperation(LineDiffOperationType operationType, int start, int end) : this(operationType, start, end, [])
    {
    }

    public static LineDiffOperation Addition(int start, int end, IReadOnlyCollection<string> data)
    {
        return new LineDiffOperation(LineDiffOperationType.Add, start, end, data);
    }

    public static LineDiffOperation Removal(int start, int end)
    {
        return new LineDiffOperation(LineDiffOperationType.Remove, start, end);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not LineDiffOperation ldo)
        {
            return false;
        }

        return Start == ldo.Start && End == ldo.End && Data.SequenceEqual(ldo.Data);
    }

    public static bool operator ==(LineDiffOperation left, LineDiffOperation right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(LineDiffOperation left, LineDiffOperation right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End, Data);
    }
}
