namespace VersionControl.Lib.Changes
{
    public class FileChange
    {
        public FileChangeEvent Event { get; }

        public string FilePath { get; }

        public IReadOnlyCollection<LineDiffOperation> LineChanges { get; }

        public FileChange(FileChangeEvent fileChangeEvent, string filePath, IReadOnlyCollection<LineDiffOperation> lineChanges)
        {
            Event = fileChangeEvent;
            FilePath = filePath;
            LineChanges = lineChanges;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not FileChange fc)
            {
                return false;
            }
            return Event == fc.Event && FilePath == fc.FilePath && LineChanges.SequenceEqual(fc.LineChanges);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Event, FilePath, LineChanges.ToArray());
        }
    }
}