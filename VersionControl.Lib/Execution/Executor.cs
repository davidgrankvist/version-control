using VersionControl.Lib.Commands;
using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Execution
{
    public class Executor : IExecutor
    {
        private readonly IDocumentationService documentationService;

        public Executor(IDocumentationService documentationService)
        {
            this.documentationService = documentationService;
        }

        public void Execute(IVersionControlCommand command, bool helpMode = false)
        {
            if (!helpMode && command.CanExecute())
            {
                command.Execute();
            }
            else
            {
                documentationService.ShowCommandHelp(command.Help());
            }
        }
    }
}