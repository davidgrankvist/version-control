namespace VersionControl.Lib.Changes.Services
{
	public interface IPathResolver
	{
		/// <summary>
		/// Resolve patterns and directories to a concrete list of file paths
		/// </summary>
		IReadOnlyCollection<string> Resolve(IReadOnlyCollection<string> paths);
	}
}
