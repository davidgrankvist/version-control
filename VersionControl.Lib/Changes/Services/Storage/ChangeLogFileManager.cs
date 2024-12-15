using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services.ChangeLogs;
public static class ChangeLogFileManager
{
    public static IReadOnlyCollection<ChangeSet> ParseLog(Stream stream, long fromOffset = 0, long toOffset = long.MaxValue)
    {
        var changesets = new List<ChangeSet>();
        using (var reader = new BinaryReader(stream))
        {
            var end = Math.Min(stream.Length, toOffset == long.MaxValue ? toOffset : toOffset + 1);
            reader.BaseStream.Position = fromOffset;

            while (reader.BaseStream.Position < end)
            {
                var changeSet = ParseChangeSet(reader);
                changesets.Add(changeSet);
            }
        }

        return changesets;
    }

    private static ChangeSet ParseChangeSet(BinaryReader reader)
    {
        var fileChanges = new List<FileChange>();

        var message = reader.ReadString();
        var numFileChanges = reader.ReadInt32();
        for (var i = 0; i < numFileChanges; i++)
        {
            var fileChange = ParseFileChange(reader);
            fileChanges.Add(fileChange);
        }

        return new ChangeSet(fileChanges, message);
    }

    private static FileChange ParseFileChange(BinaryReader reader)
    {
        var filePath = reader.ReadString();
        var operations = new List<LineDiffOperation>();

        var numOperations = reader.ReadInt32();

        for (var i = 0; i < numOperations; i++)
        {
            var operation = ParseDiffOperation(reader);
            operations.Add(operation);
        }

        return new FileChange(FileChangeEvent.Update, filePath, operations);
    }

    private static LineDiffOperation ParseDiffOperation(BinaryReader reader)
    {
        var opType = (LineDiffOperationType)reader.ReadByte();
        var start = reader.ReadInt32();
        var end = reader.ReadInt32();

        var lines = new List<string>();
        if (opType == LineDiffOperationType.Add)
        {
            var numLines = reader.ReadInt32();
            for (var i = 0; i < numLines; i++)
            {
                var line = reader.ReadString();
                lines.Add(line);
            }
        }
        return new LineDiffOperation(opType, start, end, lines);
    }

    public static long Append(Stream stream, ChangeSet changeSet)
    {
        var result = -1L;
        using (var writer = new BinaryWriter(stream))
        {
            writer.BaseStream.Position = stream.Length;
            result = writer.BaseStream.Position;
            WriteChangeSet(writer, changeSet);
        }

        return result;
    }

    private static void WriteChangeSet(BinaryWriter writer, ChangeSet changeSet)
    {
        var numFileChanges = changeSet.FileChanges.Count;
        writer.Write(changeSet.Message);
        writer.Write(numFileChanges);

        foreach (var fileChange in changeSet.FileChanges)
        {
            WriteFileChange(writer, fileChange);
        }
    }

    private static void WriteFileChange(BinaryWriter writer, FileChange fileChange)
    {
        writer.Write(fileChange.FilePath);

        var numOperations = fileChange.LineChanges.Count;
        writer.Write(numOperations);

        foreach (var operation in fileChange.LineChanges)
        {
            WriteLineChange(writer, operation);
        }
    }

    private static void WriteLineChange(BinaryWriter writer, LineDiffOperation operation)
    {
        writer.Write((byte)operation.OperationType);
        writer.Write(operation.Start);
        writer.Write(operation.End);

        var numDatas = operation.Data.Count;

        if (numDatas > 0)
        {
            writer.Write(numDatas);

            foreach (var data in operation.Data)
            {
                writer.Write(data);
            }
        }

    }
}
