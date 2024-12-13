using VersionControl.Lib.Changes.Services.Storage;
using VersionControl.Test.Framework.FakeFiles;

namespace VersionControl.Test.Storage;

[TestClass]
public partial class ChangeLogIndexFileManagerTest
{
    [TestMethod]
    public void ShouldWriteReadIndex()
    {
        CheckWriteRead(20);
    }

    private static void CheckWriteRead(int size)
    {
        var data = CreateTestData(size);

        var file = new AppendableFakeFile();
        foreach (var item in data)
        {
            var appendStream = file.Write();
            ChangeLogIndexFileManager.Append(appendStream, item.Id.ToString(), item.Offset);
        }

        foreach (var item in data)
        {
            var readStream = file.Read();
            var offset = ChangeLogIndexFileManager.ParseIndex(readStream, item.Id.ToString());
            Assert.AreEqual(item.Offset, offset);
        }
    }

    private static List<(Guid Id, long Offset)> CreateTestData(int size)
    {
        var random = new Random(1234);

        var data = new List<(Guid, long)>();

        for (var i = 0; i < size; i++)
        {
            var buffer = new byte[16];
            random.NextBytes(buffer);
            var id = new Guid(buffer);

            var offset = random.NextInt64();

            data.Add((id, offset));
        }

        return data;
    }
}
