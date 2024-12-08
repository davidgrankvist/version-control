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

        public string Save(ChangeSet changeSet)
        {
            /*
             * Simple file updates with no backup management for now:
             *
             * 1. append to log
             * 2. append to index
             * 3. update state
             */

            var changeId = Guid.NewGuid().ToString();

            using (var logStream = fileManager.Append(LogPath))
            using (var indexStream = fileManager.Append(LogIndexPath))
            using (var stateStream = fileManager.Write(StatePath))
            {
                var offset = ChangeLogFileManager.Append(logStream, changeSet);
                ChangeLogIndexFileManager.Append(indexStream, changeId, offset);
                VersionControlStateFileManager.WriteState(stateStream, new VersionControlState(changeId));
            }

            return changeId;
        }

        public IReadOnlyCollection<ChangeSet> GetHistory(string? fromChangeId = null, string? toChangeId = null)
        {
            IReadOnlyCollection<ChangeSet> changes = [];
            using (var indexStream = fileManager.ReadFile(LogIndexPath))
            using (var indexSecondaryStream = fileManager.ReadFile(LogIndexPath))
            using (var logStream = fileManager.ReadFile(LogPath))
            {
                var startOffset = fromChangeId == null ? 0 : ChangeLogIndexFileManager.ParseIndex(indexStream, fromChangeId);
                var endOffset = toChangeId == null ? long.MaxValue : 
                    (fromChangeId == toChangeId ? startOffset : ChangeLogIndexFileManager.ParseIndex(indexSecondaryStream, toChangeId));
                if (startOffset != -1 && endOffset != -1)
                {
                    changes = ChangeLogFileManager.ParseLog(logStream, startOffset, endOffset);
                }
            }

            return changes;
        }

        public FileSnapshot GetSnapshot(string filePath, string changeId)
        {
            /*
             * Naive not in the slightest optimized snapshot calculation:
             * 1. retrieve changes from the start to the given change
             * 2. filter out file specific diffs
             * 3. replay
             */

            var changes = GetHistory(null, changeId);
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
            var changeId = string.Empty;
            using (var stream = fileManager.ReadFile(StatePath))
            {
                var state = VersionControlStateFileManager.ParseState(stream);
                changeId = state.CurrentChangeId;
            }

            return changeId;
        }
    }
}