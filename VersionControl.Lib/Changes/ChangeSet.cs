using VersionControl.Lib.Changes;

namespace VersionControl.Lib.Storage
{
    public class ChangeSet
    {
        public string Message { get; }

        public IReadOnlyCollection<FileChange> FileChanges { get; }

        public ChangeSet(IReadOnlyCollection<FileChange> fileChanges, string message)
        {
            FileChanges = fileChanges;
            Message = message;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ChangeSet cs)
            {
                return false;
            }

            return Message == cs.Message && FileChanges.SequenceEqual(cs.FileChanges);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Message, FileChanges);
        }
    }
}