using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services
{
    public class ChangeStore : IChangeStore
    {
        public void Save(ChangeSet changeSet)
        {
            throw new NotImplementedException();
        }

        public ChangeSet GetChange(string changeId)
        {
            throw new NotImplementedException();
        }

        public FileSnapshot GetSnapshot(string filePath, string? changeId = null)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentChangeId()
        {
            throw new NotImplementedException();
        }
    }
}