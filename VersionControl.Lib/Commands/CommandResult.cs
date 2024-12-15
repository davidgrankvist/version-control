namespace VersionControl.Lib.Commands;
public readonly struct CommandResult
{
    public string Message { get; }

    public CommandResult()
    {
        Message = string.Empty;
    }

    public CommandResult(string message)
    {
        Message = message;
    }

    public static readonly CommandResult Empty = new();
}
