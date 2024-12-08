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

        public override bool Equals(object? obj)
        {
            if (obj is not ChangeSet cs)
            {
                return false;
            }

            return FileChanges.SequenceEqual(cs.FileChanges);
        }

        public override int GetHashCode()
        {
            return FileChanges.ToArray().GetHashCode();
        }
    }
}