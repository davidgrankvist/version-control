using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
    public class CompareCommand : IVersionControlCommand
    {
        private readonly IChangeService changeService;
        private readonly string? fromChange;
        private readonly string? toChange;

        public const string Name = "compare";
        private static readonly CommandDocumentation docs = new(Name, "Compare changes.", "Compare changes.");

        public CompareCommand()
        {
        }

        public CompareCommand(IChangeService changeService, string? fromChange, string? toChange)
        {
            this.changeService = changeService;
            this.fromChange = fromChange;
            this.toChange = toChange;
        }

        public bool CanExecute()
        {
            return fromChange != null && toChange != null;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public CommandDocumentation Help()
        {
            return docs;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not CompareCommand cmd)
            {
                return false;
            }

            return fromChange == cmd.fromChange && toChange == cmd.toChange;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), fromChange, toChange);
        }
    }
}