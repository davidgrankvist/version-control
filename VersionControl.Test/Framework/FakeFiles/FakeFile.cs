namespace VersionControl.Test.Framework.FakeFiles;

public class FakeFile : IFakeFile
{
    private NoCloseMemoryStream writeStream;

    public Stream Write()
    {
        writeStream = new NoCloseMemoryStream();
        return writeStream;
    }

    public Stream Read()
    {
        var buffer = writeStream.Collect();
        return new MemoryStream(buffer);
    }

    public byte[] ReadAllBytes()
    {
        return writeStream.Collect();
    }
}
