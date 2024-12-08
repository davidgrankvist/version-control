using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services.Storage;
using VersionControl.Test.Framework.FakeFiles;

namespace VersionControl.Test.Storage;

[TestClass]
public class VersionControlStateFileManagerTest
{
    [TestMethod]
    public void ShouldWriteRead()
    {
        CheckWriteRead();
    }

    private void CheckWriteRead()
    {
        var changeId = Guid.NewGuid();
        var state = new VersionControlState(changeId.ToString());
        var file = new FakeFile();

        VersionControlStateFileManager.WriteState(file.Write(), state);
        var result = VersionControlStateFileManager.ParseState(file.Read());

        Assert.AreEqual(state, result);
    }
}
