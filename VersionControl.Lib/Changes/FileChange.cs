namespace VersionControl.Lib.Changes
{
    public class FileChange
    {
        public FileChangeEvent Event { get; }

        public string FilePath { get; }

        public IReadOnlyCollection<LineChange> LineChanges { get; }

        public FileChange(FileChangeEvent fileChangeEvent, string filePath, IReadOnlyCollection<LineChange> lineChanges)
        {
            Event = fileChangeEvent;
            FilePath = filePath;
            LineChanges = lineChanges;
        }
    }
}