namespace VersionControl.Lib.Changes.Services
{
    public class PathResolver : IPathResolver
    {
        public IReadOnlyCollection<string> Resolve(IReadOnlyCollection<string> paths)
        {
            // assume simple paths for now
            return paths;
        }
    }
}