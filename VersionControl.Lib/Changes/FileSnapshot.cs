namespace VersionControl.Lib.Changes
{
    public struct FileSnapshot
    {
        public string FilePath { get; }

        public byte[] Data { get; }

        public FileSnapshot(string filePath) : this(filePath, [])
        {
        }

        public FileSnapshot(string filePath, byte[] data)
        {
            FilePath = filePath;
            Data = data;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not FileSnapshot fs)
            {
                return false;
            }

            return FilePath == fs.FilePath && Data.SequenceEqual(fs.Data);
        }

        public static bool operator ==(FileSnapshot left, FileSnapshot right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FileSnapshot left, FileSnapshot right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FilePath, Data);
        }
    }
}