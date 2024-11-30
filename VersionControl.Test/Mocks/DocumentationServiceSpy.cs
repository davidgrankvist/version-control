using VersionControl.Lib.Documentation;

namespace VersionControl.Test.Mocks
{
	internal class DocumentationServiceSpy : IDocumentationService
	{
		public bool DidShowGeneralHelp { get; private set; }

		public List<CommandDocumentation> ShownDocs { get; } = new List<CommandDocumentation>();

		public void ShowCommandHelp(CommandDocumentation commandDoc)
		{
			ShownDocs.Add(commandDoc);
		}

		public void ShowGeneralHelp()
		{
			DidShowGeneralHelp = true;
		}
	}
}
