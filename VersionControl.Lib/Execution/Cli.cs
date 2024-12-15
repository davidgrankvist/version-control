using VersionControl.Lib.Documentation;
using VersionControl.Lib.Parsing;

namespace VersionControl.Lib.Execution
{
    public class Cli
    {
        private readonly IExecutor executor;
        private readonly ArgumentParser argumentParser;

        public Cli(IExecutor executor, ArgumentParser argumentParser)
        {
            this.executor = executor;
            this.argumentParser = argumentParser;
        }

        public void Run(string[] args)
        {
            var (command, helpMode) = argumentParser.Parse(args);
            executor.Execute(command, helpMode);
        }
    }
}