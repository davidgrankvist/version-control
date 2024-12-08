using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services.ChangeLogs;
using VersionControl.Lib.Storage;
using VersionControl.Test.Framework.FakeFiles;

namespace VersionControl.Test.Mocks;

[TestClass]
public class ChangeLogFileManagerTest
{
    [TestMethod]
    public void ShouldWriteRead()
    {
        CheckWriteRead();
    }

    private static void CheckWriteRead()
    {
        var data = CreateTestData(5, 10, 5, 3);

        var file = new AppendableFakeFile();
        var offsets = new List<long>();
        foreach (var change in data)
        {
            var appendStream = file.Write();
            var offset = ChangeLogFileManager.Append(appendStream, change);
            offsets.Add(offset);
        }
        file.CollectAppended();

        for (var i = 0; i < data.Count; i++)
        {
            var change = data[i];
            var offset = offsets[i];
            var parsed = ChangeLogFileManager.ParseLog(file.Read(), offset, offset);

            Assert.AreEqual(1, parsed.Count);
            Assert.AreEqual(change, parsed.First());
        }
    }

    private static List<ChangeSet> CreateTestData(int numChanges, int numFiles, int numOperations, int numRows)
    {
        var random = new Random(6421);

        var changes = new List<ChangeSet>();
        for (var i = 0; i < numChanges; i++)
        {
            var fileChanges = new List<FileChange>();
            for (var j = 0; j < numFiles; j++)
            {
                var fileEvent = FileChangeEvent.Update;
                var filePath = "file_" + random.Next(0, 1000).ToString();
                var operations = CreateTestDataOperations(random, numOperations, numRows);

                fileChanges.Add(new FileChange(fileEvent, filePath, operations));
            }

            changes.Add(new ChangeSet(fileChanges));
        }

        return changes;
    }

    private static List<LineDiffOperation> CreateTestDataOperations(Random random, int numOperations, int numRows)
    {
        var operations = new List<LineDiffOperation>();

        for (var i = 0; i < numOperations; i++)
        {
            var op = (random.Next() % 2) == 0 ? LineDiffOperationType.Add : LineDiffOperationType.Remove;

            var start = random.Next();
            var end = random.Next();

            var lines = new List<string>();
            if (op == LineDiffOperationType.Add)
            {
                for (var j = 0; j < numRows; j++)
                {
                    lines.Add(random.Next().ToString());
                }
            }

            operations.Add(new LineDiffOperation(op, start, end, lines));
        }

        return operations;
    }
}
