
namespace VersionControl.Lib.IO;
public class FileManager : IFileManager
{
    public byte[] ReadAllBytes(string path)
    {
        return File.ReadAllBytes(path);
    }

    public Stream ReadFile(string path)
    {
        return File.OpenRead(path);
    }

    public Stream Append(string path)
    {
        return File.Open(path, FileMode.Append);
    }
}
