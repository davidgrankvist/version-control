using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services;

namespace VersionControl.Test.Mocks
{
    public class ChangeServiceSpy : IChangeService
    {
        public List<(IReadOnlyCollection<string> Files, string Message)> SaveCalls { get; } = [];
        public List<HistoryQuery> HistoryCalls { get; } = [];

        public void Save(IReadOnlyCollection<string> filePaths, string message)
        {
            SaveCalls.Add((filePaths, message));
        }

        public IReadOnlyCollection<ChangeWrapper> GetHistory(HistoryQuery query)
        {
            HistoryCalls.Add(query);
            return [];
        }
    }
}