namespace VersionControl.Test.Framework.FakeFiles;

/// <summary>
/// Memory stream wrapper to simulate an append-only file.
/// </summary>
public class AppendableFakeFile : IFakeFile
{
    private NoCloseMemoryStream? stream;

    public Stream Write()
    {
        if (stream == null)
        {
            stream = new NoCloseMemoryStream();
        }
        return stream;
    }

    private byte[] GetBuffer()
    {
        return stream!.Collect();
    }

    public Stream Read()
    {
        return new MemoryStream(GetBuffer());
    }

    public byte[] ReadAllBytes()
    {
        return GetBuffer();
    }
}
