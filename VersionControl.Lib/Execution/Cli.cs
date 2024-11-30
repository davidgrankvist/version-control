using VersionControl.Lib.Documentation;
using VersionControl.Lib.Parsing;

namespace VersionControl.Lib.Execution
{
	public class Cli
	{
		private readonly IDocumentationService documentationService;
		private readonly IExecutor executor;

		public Cli()
		{
			var consoleDocSvc = new ConsoleDocumentationService();
			documentationService = consoleDocSvc;
			executor = new Executor(documentationService);
		}

		public Cli(IExecutor executor, IDocumentationService documentationService)
		{
			this.documentationService = documentationService;
			this.executor = executor;
		}

		public void Run(string[] args)
		{
			var (command, helpMode) = ArgumentParser.Parse(args);

			if (command == null)
			{
				documentationService.ShowGeneralHelp();
			}
			else
			{
				executor.Execute(command, helpMode);
			}
		}
	}
}
