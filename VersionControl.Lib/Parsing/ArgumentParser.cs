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
			var helpMode = args.Any(s => s == "-h" || s == "--help");

			var commandName = args.Length == 0 ? string.Empty : args[0];
			var firstArg = args.Length < 2 ? null : args[1];
			var secondArg = args.Length < 3 ? null : args[2];
			switch (commandName)
			{
				case SaveCommand.Name:
					command = commandFactory.CreateSaveCommand(args.Skip(1).ToArray().AsReadOnly());
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
	}
}
