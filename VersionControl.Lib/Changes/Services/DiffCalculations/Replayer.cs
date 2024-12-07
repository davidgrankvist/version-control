namespace VersionControl.Lib.Changes.Services.DiffCalculations;
public static class Replayer
{
    public static string[] Replay(string[] snapshot, IReadOnlyCollection<LineDiffOperation> operations)
    {
        var currentLines = snapshot.ToList();

        foreach (var operation in operations)
        {
            switch (operation.OperationType)
            {
                case LineDiffOperationType.Remove:
                    currentLines.RemoveRange(operation.Start, operation.End - operation.Start + 1);
                    break;
                case LineDiffOperationType.Add:
                    currentLines.InsertRange(operation.Start, operation.Data);
                    break;
                default:
                    throw new InvalidOperationException("Unknown line operation type.");
            }
        }

        return currentLines.ToArray();
    }
}
