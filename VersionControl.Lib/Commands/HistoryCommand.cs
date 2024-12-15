
using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
    public class HistoryCommand : IVersionControlCommand
    {
        public const string Name = "history";
        private static readonly CommandDocumentation docs = new(
            Name,
            "View history.",
            "View history.",
            [new CommandArg("format", "f", "Output format.")]);

        private readonly IChangeService? changeService;
        private readonly HistoryQuery? query;

        public HistoryCommand()
        {
        }

        public HistoryCommand(IChangeService changeService, HistoryQuery query)
        {
            this.changeService = changeService;
            this.query = query;
        }

        public bool CanExecute()
        {
            return changeService != null && query != null;
        }

        public CommandResult Execute()
        {
            var changes = changeService!.GetHistory(query!);
            var result = FormatChanges(changes);

            return result;
        }

        private static CommandResult FormatChanges(IReadOnlyCollection<ChangeWrapper> changes)
        {
            var msg = string.Join(Environment.NewLine, changes.Select(change => change.Id + Environment.NewLine + change.Change.Message));

            return new CommandResult(msg);
        }

        public CommandDocumentation Help()
        {
            return docs;
        }

        public override bool Equals(object? obj)
        {
            return obj is HistoryCommand;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}