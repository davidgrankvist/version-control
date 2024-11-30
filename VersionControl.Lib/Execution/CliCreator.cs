using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Execution
{
	public static class CliCreator
	{
		public static Cli Create()
		{
			var documentationService = new ConsoleDocumentationService();
			var executor = new Executor(documentationService);
			return new Cli(executor, documentationService);
		}
	}
}
