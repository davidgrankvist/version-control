using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Commands;

namespace VersionControl.Lib.Parsing
{
    public class CommandFactory
    {
        private readonly IChangeService changeService;

        public CommandFactory(IChangeService changeService)
        {
            this.changeService = changeService;
        }

        public IVersionControlCommand CreateSaveCommand(IReadOnlyCollection<string> filePaths, string message)
        {
            return new SaveCommand(changeService, filePaths, message);
        }

        public IVersionControlCommand CreateHistoryCommand(HistoryQuery query)
        {
            return new HistoryCommand(changeService, query);
        }


        public IVersionControlCommand CreateStatusCommand()
        {
            return new StatusCommand(changeService);
        }


        public IVersionControlCommand CreateGotoCommand(string? change)
        {
            return new GotoCommand(changeService, change);
        }

        public IVersionControlCommand CreateCompareCommand(string? fromChange, string? toChange)
        {
            return new CompareCommand(changeService, fromChange, toChange);
        }
    }
}