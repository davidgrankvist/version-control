using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Changes.Services.Storage;
using VersionControl.Lib.Documentation;
using VersionControl.Lib.IO;
using VersionControl.Lib.Parsing;

namespace VersionControl.Lib.Execution
{
    public static class CliCreator
    {
        public static Cli Create()
        {
            var documentationService = new DocumentationService();
            var executor = new Executor(documentationService, new ConsoleService());
            var changeService = new ChangeService(new ChangeStore(new FileManager()), new PathResolver(), new Differ());
            var argumentParser = new ArgumentParser(new CommandFactory(changeService));
            return new Cli(executor, argumentParser);
        }
    }
}