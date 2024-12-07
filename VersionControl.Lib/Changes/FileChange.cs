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
    }
}