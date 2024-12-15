using VersionControl.Lib.Changes.Services;

namespace VersionControl.Test.Mocks
{
    public class ChangeServiceSpy : IChangeService
    {
        public List<(IReadOnlyCollection<string> Files, string Message)> Calls { get; } = [];

        public void Save(IReadOnlyCollection<string> filePaths, string message)
        {
            Calls.Add((filePaths, message));
        }
    }
}