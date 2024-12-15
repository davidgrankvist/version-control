namespace VersionControl.Lib.Documentation;
public struct CommandArg
{
    public string Name { get; }

    public string Abbrev { get; }

    public string Description { get; }

    public CommandArg(string name, string abbrev, string description)
    {
        Name = name;
        Abbrev = abbrev;
        Description = description;
    }

    public static CommandArg HelpArg = new CommandArg("help", "h", "Output this text.");
}
