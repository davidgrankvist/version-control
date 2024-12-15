
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
    public Stream Write(string path)
    {
        if (!Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            return File.Create(path);
        }
        else
        {
            return File.OpenWrite(path);
        }
    }

    public Stream Append(string path)
    {
        if (!Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            return File.Create(path);
        }
        else
        {
            return File.Open(path, FileMode.Append);
        }
    }

    public bool Exists(string path)
    {
        return File.Exists(path);
    }
}