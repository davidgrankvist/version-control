using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
	public class HistoryCommand : IVersionControlCommand
	{
		public const string Name = "history";
		private static readonly CommandDocumentation docs = new(Name, "View history.", "View history.");

		public HistoryCommand()
		{
		}

		public bool CanExecute()
		{
			return false;
		}

		public void Execute()
		{
			throw new NotImplementedException();
		}

		public CommandDocumentation Help()
		{
			return docs;
		}

		public override bool Equals(object? obj)
		{
			return obj is HistoryCommand;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
