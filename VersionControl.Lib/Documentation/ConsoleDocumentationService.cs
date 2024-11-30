namespace VersionControl.Lib.Documentation
{
	internal class ConsoleDocumentationService : IDocumentationService
	{
		public void ShowCommandHelp(CommandDocumentation commandDoc)
		{
			Console.WriteLine(commandDoc.Name);
			Console.WriteLine();
			Console.WriteLine(commandDoc.Summary);
		}

		public void ShowGeneralHelp()
		{
			Console.WriteLine("Some general help.");
		}
	}
}
