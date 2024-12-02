using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services
{
    public interface IChangeStore
    {
        void Save(ChangeSet changeSet);

        ChangeSet GetChange(string changeId);

        FileSnapshot GetSnapshot(string filePath, string? changeId = null);

        string GetCurrentChangeId();
    }
}