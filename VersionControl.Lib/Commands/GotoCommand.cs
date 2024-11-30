using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
	public class GotoCommand : IVersionControlCommand
	{
		private readonly string? change;

		public const string Name = "goto";
		private static readonly CommandDocumentation docs = new(Name, "Time travel.", "Move to a point in history.");

		private readonly IChangeService changeService;

		public GotoCommand()
		{
		}

		public GotoCommand(IChangeService changeService, string? change)
		{
			this.changeService = changeService;
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
