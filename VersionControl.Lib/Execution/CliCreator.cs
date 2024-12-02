using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Documentation;
using VersionControl.Lib.Parsing;

namespace VersionControl.Lib.Execution
{
	public static class CliCreator
	{
		public static Cli Create()
		{
			var documentationService = new ConsoleDocumentationService();
			var executor = new Executor(documentationService);
			var changeService = new ChangeService(new ChangeStore(), new PathResolver(), new Differ());
			var argumentParser = new ArgumentParser(new CommandFactory(changeService));
			return new Cli(executor, documentationService, argumentParser);
		}
	}
}
