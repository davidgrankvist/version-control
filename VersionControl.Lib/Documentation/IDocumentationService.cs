namespace VersionControl.Lib.Documentation
{
    public interface IDocumentationService
    {
        void ShowCommandHelp(CommandDocumentation commandDoc);

        void ShowGeneralHelp();
    }
}