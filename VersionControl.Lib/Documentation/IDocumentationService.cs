namespace VersionControl.Lib.Documentation
{
    public interface IDocumentationService
    {
        string BuildCommandHelp(CommandDocumentation commandDoc);

        string BuildGeneralHelp();
    }
}