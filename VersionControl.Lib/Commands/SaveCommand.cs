using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
	public class SaveCommand : IVersionControlCommand
	{
		public const string Name = "save";
		private static readonly CommandDocumentation docs = new(Name, "Save changes.", "Save changes.");

		private readonly IChangeService changeService;
		private readonly IReadOnlyCollection<string> files;

		public SaveCommand()
		{
			files = [];
		}

		public SaveCommand(IChangeService changeService, IReadOnlyCollection<string> files)
		{
			this.changeService = changeService;
			this.files = files;
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
			if (obj is not SaveCommand cmd)
			{
				return false;
			}

			return files.SequenceEqual(cmd.files);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(base.GetHashCode(), files.GetHashCode());
		}
	}
}
