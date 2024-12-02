namespace VersionControl.Lib.Documentation
{
    public class CommandDocumentation
    {
        public CommandDocumentation(string name, string description, string summary)
        {
            Name = name;
            Description = description;
            Summary = summary;
        }

        public string Name { get; }

        public string Description { get; }

        public string Summary { get; }
    }
}