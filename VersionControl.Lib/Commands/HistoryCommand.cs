using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
    public class HistoryCommand : IVersionControlCommand
    {
        public const string Name = "history";
        private static readonly CommandDocumentation docs = new(Name, "View history.", "View history.");

        private readonly IChangeService changeService;

        public HistoryCommand()
        {
        }

        public HistoryCommand(IChangeService changeService)
        {
            this.changeService = changeService;
        }

        public bool CanExecute()
        {
            return false;
        }

        public CommandResult Execute()
        {
            throw new NotImplementedException();
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