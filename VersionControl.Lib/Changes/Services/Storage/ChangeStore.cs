using System.Text;

using VersionControl.Lib.Changes.Services.ChangeLogs;
using VersionControl.Lib.Changes.Services.DiffCalculations;
using VersionControl.Lib.IO;
using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services.Storage
{
    public class ChangeStore : IChangeStore
    {
        public const string StoreRoot = ".vcs_store";
        public static readonly string LogPath = Path.Combine(StoreRoot, "main.log");
        public static readonly string LogIndexPath = Path.Combine(StoreRoot, "main.log.index");
        public static readonly string StatePath = Path.Combine(StoreRoot, "current.state");

        private readonly IFileManager fileManager;

        public ChangeStore(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public void Save(ChangeSet changeSet)
        {
            throw new NotImplementedException();
        }

        public ChangeSet GetChange(string changeId)
        {
            throw new NotImplementedException();
        }

        public FileSnapshot GetSnapshot(string filePath, string changeId)
        {
            /*
             * Naive not in the slightest optimized snapshot calculation:
             * 1. retrieve changes from the start to the given change
             * 2. filter out file specific diffs
             * 3. replay
             */

            IReadOnlyCollection<ChangeSet> changes;
            using (var indexStream = fileManager.ReadFile(LogIndexPath))
            using (var logStream = fileManager.ReadFile(LogPath))
            {
                var logOffset = ChangeLogIndexFileManager.ParseIndex(logStream, changeId);
                if (logOffset == -1)
                {
                    throw new InvalidOperationException($"Unable to find offset for change {changeId}");
                }
                changes = ChangeLogFileManager.ParseLog(logStream, logOffset);
            }
            var fileChanges = changes
                .SelectMany(c => c.FileChanges)
                .Where(fc => fc.FilePath == filePath)
                .SelectMany(fc => fc.LineChanges).ToList();

            var snapshot = Replayer.Replay([], fileChanges);
            var snapshotStr = string.Join(Environment.NewLine, snapshot);
            var snapshotBytes = Encoding.UTF8.GetBytes(snapshotStr);
            return new FileSnapshot(filePath, snapshotBytes);
        }

        public FileSnapshot GetImmediateSnapShot(string filePath)
        {
            var bytes = fileManager.ReadAllBytes(filePath);
            return new FileSnapshot(filePath, bytes);
        }

        public string GetCurrentChangeId()
        {
            throw new NotImplementedException();
        }
    }
}