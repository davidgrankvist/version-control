using VersionControl.Lib.Changes;
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
                    command = ParseHistoryCommand(args);
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

        // TODO(refactor): use generic arg parsing instead of multiple specific, similar ones..
        private IVersionControlCommand ParseHistoryCommand(string[] args)
        {
            IReadOnlyCollection<string> files = Array.Empty<string>();
            string? fromChangeId = null;
            string? toChangeId = null;
            HistoryQueryFormat format = HistoryQueryFormat.Compact;

            var commandArgs = args.Skip(1).Where(s => !IsHelpArg(s)).ToList();
            var iFmt = commandArgs.FindIndex(IsFormatArg);

            if (iFmt == -1) // no format provided
            {
                fromChangeId = commandArgs.Count > 0 ? commandArgs[0] : null;
                toChangeId = commandArgs.Count > 1 ? commandArgs[1] : null;
            }
            else if (iFmt == commandArgs.Count - 1) // format arg given, but no value
            {
                var rem = commandArgs.Take(iFmt).ToList();

                fromChangeId = rem.Count > 0 ? rem[0] : null;
                toChangeId = rem.Count > 1 ? rem[1] : null;
            }
            else // format was provided
            {
                var iFmtContent = iFmt + 1;
                var fmtStr = commandArgs[iFmtContent];

                var argsPart1 = commandArgs.Take(iFmt);
                var argsPart2 = commandArgs.Skip(iFmtContent + 1);
                var rem = argsPart1.Concat(argsPart2).ToList();

                fromChangeId = rem.Count > 0 ? rem[0] : null;
                toChangeId = rem.Count > 1 ? rem[1] : null;
                format = ParseFormat(fmtStr);
            }

            return commandFactory.CreateHistoryCommand(new HistoryQuery(fromChangeId, toChangeId, format));
        }

        private static bool IsHelpArg(string arg)
        {
            return arg == "-h" || arg == "--help";
        }

        private static bool IsMessageArg(string arg)
        {
            return arg == "-m" || arg == "--message";
        }

        private static bool IsFormatArg(string arg)
        {
            return arg == "-f" || arg == "--format";
        }

        private static HistoryQueryFormat ParseFormat(string arg)
        {
            HistoryQueryFormat fmt;
            switch (arg)
            {
                case "compact":
                    fmt = HistoryQueryFormat.Compact;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown history format: {arg}");
            }
            return fmt;
        }
    }
}