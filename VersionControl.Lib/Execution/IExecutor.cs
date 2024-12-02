using VersionControl.Lib.Commands;

namespace VersionControl.Lib.Execution
{
    public interface IExecutor
    {
        void Execute(IVersionControlCommand command, bool helpMode = false);
    }
}