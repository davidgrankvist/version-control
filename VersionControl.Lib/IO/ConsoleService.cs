namespace VersionControl.Lib.IO;
public class ConsoleService : IConsoleService
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
