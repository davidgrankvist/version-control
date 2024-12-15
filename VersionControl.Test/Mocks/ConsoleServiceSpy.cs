using VersionControl.Lib.IO;

namespace VersionControl.Test.Mocks;
internal class ConsoleServiceSpy : IConsoleService
{
    public List<string> Messages { get; } = [];

    public void Write(string message)
    {
        Messages.Add(message);
    }

    public void WriteLine(string message)
    {
        Messages.Add(message);
    }
}
