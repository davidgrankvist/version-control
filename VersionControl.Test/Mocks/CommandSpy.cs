using VersionControl.Lib.Commands;
using VersionControl.Lib.Documentation;

namespace VersionControl.Test.Mocks
{
    internal class CommandSpy : IVersionControlCommand
    {
        private static readonly CommandDocumentation docs = new CommandDocumentation("spy", "Spy desc.", "Spy summary.");

        public bool DidExecute { get; set; }
        public bool CanExecuteEnabled { get; set; }
        public bool DidHelp { get; set; }

        public CommandDocumentation Docs => docs;

        public bool CanExecute()
        {
            return CanExecuteEnabled;
        }

        public void Execute()
        {
            DidExecute = true;
        }

        public CommandDocumentation Help()
        {
            DidHelp = true;
            return docs;
        }
    }
}