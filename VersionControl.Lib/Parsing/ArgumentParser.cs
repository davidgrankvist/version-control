using VersionControl.Lib.Commands;

namespace VersionControl.Lib.Parsing
{
    public class ArgumentParser
    {
        private readonly CommandFactory commandFactory;

        public ArgumentParser(CommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public (IVersionControlCommand? Command, bool HelpMode) Parse(string[] args)
        {
            IVersionControlCommand? command = null;
            var helpMode = args.Any(IsHelpArg);

            var commandName = args.Length == 0 ? string.Empty : args[0];
            var firstArg = args.Length < 2 ? null : args[1];
            var secondArg = args.Length < 3 ? null : args[2];
            switch (commandName)
            {
                case SaveCommand.Name:
                    command = ParseSaveCommand(args);
                    break;
                case HistoryCommand.Name:
                    command = commandFactory.CreateHistoryCommand();
                    break;
                case StatusCommand.Name:
                    command = commandFactory.CreateStatusCommand();
                    break;
                case GotoCommand.Name:
                    command = commandFactory.CreateGotoCommand(firstArg);
                    break;
                case CompareCommand.Name:
                    command = commandFactory.CreateCompareCommand(firstArg, secondArg);
                    break;
                default:
                    break;
            }

            return (command, helpMode);
        }

        private IVersionControlCommand ParseSaveCommand(string[] args)
        {
            IReadOnlyCollection<string> files = Array.Empty<string>();
            var msg = string.Empty;

            var commandArgs = args.Skip(1).Where(s => !IsHelpArg(s)).ToList();
            var iMsg = commandArgs.FindIndex(IsMessageArg);

            if (iMsg == -1) // no message provided
            {
                files = commandArgs;
            }
            else if (iMsg == commandArgs.Count - 1) // message arg given, but no message
            {
                files = commandArgs.Take(iMsg).ToList();
            }
            else // message arg given with message
            {
                var iMsgContent = iMsg + 1;
                msg = commandArgs[iMsgContent];

                var filesPart1 = commandArgs.Take(iMsg);
                var filesPart2 = commandArgs.Skip(iMsgContent + 1);
                files = filesPart1.Concat(filesPart2).ToList();
            }

            return commandFactory.CreateSaveCommand(files, msg);
        }

        private static bool IsHelpArg(string arg)
        {
            return arg == "-h" || arg == "--help";
        }

        private static bool IsMessageArg(string arg)
        {
            return arg == "-m" || arg == "--message";
        }
    }
}