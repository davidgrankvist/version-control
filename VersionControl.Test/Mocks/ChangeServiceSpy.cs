using VersionControl.Lib.Changes.Services;

namespace VersionControl.Test.Mocks
{
    public class ChangeServiceSpy : IChangeService
    {
        public List<IReadOnlyCollection<string>> Calls { get; } = [];

        public void Save(IReadOnlyCollection<string> filePaths)
        {
            Calls.Add(filePaths);
        }
    }
}