namespace VersionControl.Lib.Changes
{
	public struct FileSnapshot
	{
		public string FilePath { get; }

		public byte[][] Data { get; }

		public FileSnapshot(string filePath, byte[][] data)
		{
			FilePath = filePath;
			Data = data;
		}
	}
}
