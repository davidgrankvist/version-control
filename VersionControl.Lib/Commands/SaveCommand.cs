using VersionControl.Lib.Changes.Services;
using VersionControl.Lib.Documentation;

namespace VersionControl.Lib.Commands
{
    public class SaveCommand : IVersionControlCommand
    {
        public const string Name = "save";
        private static readonly CommandDocumentation docs = new(
            Name,
            "Save changes.",
            "Save changes.",
            [new CommandArg("message", "m", "Add a message.")]);

        private readonly IChangeService? changeService;
        private readonly IReadOnlyCollection<string> files;
        private readonly string message;

        public SaveCommand()
        {
            files = [];
            message = string.Empty;
        }

        public SaveCommand(IChangeService changeService, IReadOnlyCollection<string> files, string message)
        {
            this.changeService = changeService;
            this.files = files;
            this.message = message;
        }

        public bool CanExecute()
        {
            return files.Count > 0 && changeService != null;
        }

        public void Execute()
        {
            changeService!.Save(files, message);
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

            return message == cmd.message && files.SequenceEqual(cmd.files);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(message, files);
        }
    }
}