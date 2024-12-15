using VersionControl.Lib.Documentation;

namespace VersionControl.Test.Mocks
{
    internal class DocumentationServiceSpy : IDocumentationService
    {
        public bool DidBuildGeneralHelp { get; private set; }

        public List<CommandDocumentation> ShownDocs { get; } = new List<CommandDocumentation>();

        public string BuildCommandHelp(CommandDocumentation commandDoc)
        {
            ShownDocs.Add(commandDoc);
            return string.Empty;
        }

        public string BuildGeneralHelp()
        {
            DidBuildGeneralHelp = true;
            return string.Empty;
        }
    }
}