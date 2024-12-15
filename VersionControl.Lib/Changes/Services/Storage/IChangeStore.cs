using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services.Storage
{
    public interface IChangeStore
    {
        string Save(ChangeSet changeSet);

        IReadOnlyCollection<ChangeWrapper> GetHistory(string? fromChangeId, string? toChangeId, bool withDiffs = true);

        /// <summary>
        /// Retrieve file contents at a given point in history.
        /// </summary>
        FileSnapshot GetSnapshot(string filePath, string changeId);

        /// <summary>
        /// Retrieve file contents currently stored on disk, regardless of whether its changes are saved.
        /// </summary>
        FileSnapshot GetImmediateSnapShot(string filePath);

        string GetCurrentChangeId();
    }
}