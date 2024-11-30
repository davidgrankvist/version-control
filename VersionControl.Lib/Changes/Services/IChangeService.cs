namespace VersionControl.Lib.Changes.Services
{
	public interface IChangeService
	{
		public void Save(IReadOnlyCollection<string> filePaths);
	}
}
