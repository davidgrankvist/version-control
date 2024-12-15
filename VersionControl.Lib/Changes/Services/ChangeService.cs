using VersionControl.Lib.Changes.Services.Storage;
using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services
{
    public class ChangeService : IChangeService
    {
        private readonly IChangeStore store;
        private readonly IPathResolver pathResolver;
        private readonly IDiffer differ;

        public ChangeService(IChangeStore store, IPathResolver pathResolver, IDiffer differ)
        {
            this.store = store;
            this.pathResolver = pathResolver;
            this.differ = differ;
        }

        public void Save(IReadOnlyCollection<string> filePaths, string message)
        {
            var changes = new List<FileChange>();

            var resolvedFilePaths = pathResolver.Resolve(filePaths);
            var currentChangeId = store.GetCurrentChangeId();

            foreach (var filePath in resolvedFilePaths)
            {
                var change = GetChange(filePath, currentChangeId);
                changes.Add(change);
            }

            var changeSet = new ChangeSet(changes, message);
            store.Save(changeSet);
        }

        private FileChange GetChange(string filePath, string currentChangeId)
        {
            var prevSnapshot = store.GetSnapshot(filePath, currentChangeId);
            var newSnapshot = store.GetImmediateSnapShot(filePath);

            var change = differ.CalculateChange(prevSnapshot, newSnapshot);
            return change;
        }
    }
}