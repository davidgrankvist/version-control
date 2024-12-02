using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
    public interface IVersionControlCommand
    {
        void Execute();

        bool CanExecute();

        CommandDocumentation Help();
    }
}