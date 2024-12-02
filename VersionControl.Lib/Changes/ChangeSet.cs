using VersionControl.Lib.Changes;

namespace VersionControl.Lib.Storage
{
    public class ChangeSet
    {
        public IReadOnlyCollection<FileChange> FileChanges { get; }

        public ChangeSet(IReadOnlyCollection<FileChange> fileChanges)
        {
            FileChanges = fileChanges;
        }
    }
}