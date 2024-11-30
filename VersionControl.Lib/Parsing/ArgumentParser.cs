using VersionControl.Lib.Commands;

namespace VersionControl.Lib.Parsing
{
	public static class ArgumentParser
	{
		public static (IVersionControlCommand? Command, bool HelpMode) Parse(string[] args)
		{
			IVersionControlCommand? command = null;
			var helpMode = args.Any(s => s == "-h" || s == "--help");

			var commandName = args.Length == 0 ? string.Empty : args[0];
			var firstArg = args.Length < 2 ? null : args[1];
			var secondArg = args.Length < 3 ? null : args[2];
			switch (commandName)
			{
				case SaveCommand.Name:
					command = new SaveCommand(args.Skip(1).ToArray().AsReadOnly());
					break;
				case HistoryCommand.Name:
					command = new HistoryCommand();
					break;
				case StatusCommand.Name:
					command = new StatusCommand();
					break;
				case GotoCommand.Name:
					command = new GotoCommand(firstArg);
					break;
				case CompareCommand.Name:
					command = new CompareCommand(firstArg, secondArg);
					break;
				default:
					break;
			}

			return (command, helpMode);
		}
	}
}
