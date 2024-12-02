namespace VersionControl.Lib.Changes.Services
{
	public interface IDiffer
	{
		FileChange CalculateChange(FileSnapshot prevSnapshot, FileSnapshot newSnapshot);
	}
}
