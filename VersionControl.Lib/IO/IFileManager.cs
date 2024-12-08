namespace VersionControl.Lib.IO;
public interface IFileManager
{
    Stream ReadFile(string path);

    byte[] ReadAllBytes(string path);

    Stream Write(string path);

    Stream Append(string path);
}
