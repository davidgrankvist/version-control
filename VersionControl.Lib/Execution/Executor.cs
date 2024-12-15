using VersionControl.Lib.Commands;
using VersionControl.Lib.Documentation;
using VersionControl.Lib.IO;

namespace VersionControl.Lib.Execution
{
    public class Executor : IExecutor
    {
        private readonly IDocumentationService documentationService;
        private readonly IConsoleService consoleService;

        public Executor(IDocumentationService documentationService, IConsoleService consoleService)
        {
            this.documentationService = documentationService;
            this.consoleService = consoleService;
        }

        public void Execute(IVersionControlCommand? command, bool helpMode = false)
        {
            string message;
            if (!helpMode && command != null && command.CanExecute())
            {
                var result = command.Execute();
                message = result.Message;
            }
            else if (command != null)
            {
                message = documentationService.BuildCommandHelp(command.Help());
            }
            else
            {
                message = documentationService.BuildGeneralHelp();
            }

            consoleService.Write(message);
        }
    }
}