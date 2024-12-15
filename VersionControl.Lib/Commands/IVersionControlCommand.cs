using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
    public interface IVersionControlCommand
    {
        CommandResult Execute();

        bool CanExecute();

        CommandDocumentation Help();
    }
}