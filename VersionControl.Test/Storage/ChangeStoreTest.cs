using System.Text;

using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services.Storage;
using VersionControl.Lib.Storage;
using VersionControl.Test.Mocks;

namespace VersionControl.Test.Storage;

/// <summary>
/// Integration tests to make sure that the change store interacts with the files correctly. Uses in-memory fake files.
/// </summary>
[TestClass]
public class ChangeStoreTest
{
    private TestChangeStoreFileManager fileManager;
    private ChangeStore store;

    [TestInitialize]
    public void TestInitialize()
    {
        fileManager = new TestChangeStoreFileManager();
        store = new ChangeStore(fileManager);
    }

    [TestMethod]
    public void ShouldReadCurrentChangeId()
    {
        var changeId = Guid.NewGuid().ToString();
        var state = new VersionControlState(changeId);
        VersionControlStateFileManager.WriteState(fileManager.Write(ChangeStore.StatePath), state);

        var result = store.GetCurrentChangeId();
        
        Assert.AreEqual(changeId, result);
    }

    [TestMethod]
    public void ShouldSaveAndGetOneChange()
    {
        var filePath = "some_file.txt";
        string[] lines = ["1"];
        var change = CreateAddOneLineChanges(filePath, lines).First();

        store.Save(change);
        var result = store.GetHistory().FirstOrDefault();

        Assert.AreEqual(change, result);
    }

    [TestMethod]
    public void ShouldSaveAndGetOneChangeById()
    {
        var filePath = "some_file.txt";
        string[] lines = ["1"];
        var change = CreateAddOneLineChanges(filePath, lines).First();

        var changeId = store.Save(change);
        var result = store.GetHistory(changeId, changeId).FirstOrDefault();

        Assert.AreEqual(change, result);
    }

    [TestMethod]
    public void ShouldSaveAndReadFullHistory()
    {
        var filePath = "some_file.txt";
        string[] lines = ["1", "2", "3", "4"];
        var changes = CreateAddOneLineChanges(filePath, lines);

        SaveAll(changes);
        var result = store.GetHistory();

        Assert.IsTrue(changes.SequenceEqual(result));
    }

    [TestMethod]
    public void ShouldSaveAndGetIndividualChangesById()
    {
        var filePath = "some_file.txt";
        string[] lines = ["1", "2", "3", "4"];
        var changes = CreateAddOneLineChanges(filePath, lines);

        var changeIds = SaveAll(changes);
        for (var i = 0; i < changes.Count; i++)
        {
            var changeId = changeIds[i];
            var result = store.GetHistory(changeId, changeId).FirstOrDefault();
            var change = changes[i];

            Assert.AreEqual(change, result);
        }
    }

    [TestMethod]
    public void ShouldSaveAndReadPartOfHistory()
    {
        var filePath = "some_file.txt";
        string[] lines = ["1", "2", "3", "4"];
        var changes = CreateAddOneLineChanges(filePath, lines);
        var fromIndex = 1;
        var toIndex = 2;
        var numChanges = toIndex - fromIndex + 1;

        var changeIds = SaveAll(changes);
        var result = store.GetHistory(changeIds[fromIndex], changeIds[toIndex]);

        var changesArr = changes.ToArray();
        var resultArr = result.ToArray();
        Assert.AreEqual(numChanges, result.Count);
        for (var i = 0; i < numChanges ; i++)
        {
            Assert.AreEqual(changesArr[i + fromIndex], resultArr[i]);
        }
    }

    [TestMethod]
    public void ShouldSaveAndReplay()
    {
        var filePath = "some_file.txt";
        string[] lines = ["1", "2", "3", "4"];
        var expected = CreateSnapshot(filePath, lines.Reverse().ToArray());
        var changes = CreateAddOneLineChanges(filePath, lines);

        SaveAll(changes);
        var changeId = store.GetCurrentChangeId();
        var result = store.GetSnapshot(expected.FilePath, changeId.ToString());

        Assert.AreEqual(expected, result);
    }

    private List<string> SaveAll(IReadOnlyCollection<ChangeSet> changes)
    {
        var changeIds = new List<string>();
        foreach (var change in changes)
        {
            var changeId = store.Save(change);
            changeIds.Add(changeId);
        }

        return changeIds;
    }

    private static List<ChangeSet> CreateAddOneLineChanges(string filePath, string[] lines)
    {
        var changes = new List<ChangeSet>();
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var msg = $"Change {i}";
            var operations = LineDiffOperation.Addition(0, 0, [line]);
            var fileChange = new FileChange(FileChangeEvent.Update, filePath, [operations]);
            var changeSet = new ChangeSet([fileChange], msg);
            changes.Add(changeSet);
        }

        return changes;
    }

    private static FileSnapshot CreateSnapshot(string filePath, string[] lines)
    {
        var data = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines));
        return new FileSnapshot(filePath, data);
    }
}
