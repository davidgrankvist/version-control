namespace VersionControl.Test.Framework.FakeFiles;

/// <summary>
/// Memory stream wrapper to simulate an append-only file.
/// </summary>
public class AppendableFakeFile : IFakeFile
{
    private NoCloseMemoryStream stream;
    private List<byte[]> appended = [];
    private byte[] buffer;
    private bool isDirty;

    public Stream Write()
    {
        if (stream == null)
        {
            stream = new NoCloseMemoryStream();
        }
        return stream;
    }

    public void CollectAppended()
    {
        var toCollect = stream.Collect();
        appended.Add(toCollect);
        isDirty = true;
    }

    private byte[] GetBuffer()
    {
        if (isDirty)
        {
            buffer = appended.ToArray().Aggregate((first, second) => first.Concat(second).ToArray());
            isDirty = false;
        }

        return buffer;
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
