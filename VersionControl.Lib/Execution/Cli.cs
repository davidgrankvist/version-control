using VersionControl.Lib.Documentation;
using VersionControl.Lib.Parsing;

namespace VersionControl.Lib.Execution
{
	public class Cli
	{
		private readonly IDocumentationService documentationService;
		private readonly IExecutor executor;
		private readonly ArgumentParser argumentParser;

		public Cli(IExecutor executor, IDocumentationService documentationService, ArgumentParser argumentParser)
		{
			this.documentationService = documentationService;
			this.executor = executor;
			this.argumentParser = argumentParser;
		}

		public void Run(string[] args)
		{
			var (command, helpMode) = argumentParser.Parse(args);

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
