namespace VersionControl.Lib.Documentation
{
    public class CommandDocumentation
    {
        public CommandDocumentation(string name, string description, string summary)
            : this(name, description, summary, [])
        {

        }

        public CommandDocumentation(string name, string description, string summary, IReadOnlyCollection<CommandArg> args)
        {
            Name = name;
            Description = description;
            Summary = summary;
            Args = [..args, CommandArg.HelpArg];
        }

        public string Name { get; }

        public string Description { get; }

        public string Summary { get; }

        public IReadOnlyCollection<CommandArg> Args { get; }
    }
}