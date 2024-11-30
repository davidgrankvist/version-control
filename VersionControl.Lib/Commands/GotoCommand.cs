using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
	public class GotoCommand : IVersionControlCommand
	{
		private readonly string? change;

		public const string Name = "goto";
		private static readonly CommandDocumentation docs = new(Name, "Some description.", "Some summary");


		public GotoCommand(string? change)
		{
			this.change = change;
		}

		public bool CanExecute()
		{
			return change != null;
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
			if (obj is not GotoCommand cmd)
			{
				return false;
			}

			return change == cmd.change;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(base.GetHashCode(), change);
		}
	}
}
