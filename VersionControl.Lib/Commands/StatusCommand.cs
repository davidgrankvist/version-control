using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
	public class StatusCommand : IVersionControlCommand
	{
		public const string Name = "status";
		private static readonly CommandDocumentation docs = new(Name, "List changes.", "List changes.");

		public StatusCommand()
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
			return obj is StatusCommand;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
