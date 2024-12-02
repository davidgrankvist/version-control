namespace VersionControl.Lib.Changes
{
    public readonly struct LineChange
    {
        public int LineNumber { get; }

        public byte[] Update { get; }

        public LineChange(int lineNumber, byte[] update)
        {
            LineNumber = lineNumber;
            Update = update;
        }
    }
}