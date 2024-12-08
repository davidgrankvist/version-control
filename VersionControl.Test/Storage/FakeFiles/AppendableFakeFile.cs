namespace VersionControl.Test.Storage.FakeFiles;

/// <summary>
/// Memory stream wrapper to simulate an append-only file.
/// </summary>
public class AppendableFakeFile
{
    private NoCloseMemoryStream stream;
    private List<byte[]> appended = [];
    private byte[] buffer;
    private bool isDirty;

    public Stream Append()
    {
        stream = new NoCloseMemoryStream();
        return stream;
    }

    public void CollectAppended()
    {
        var toCollect = stream.GetBuffer().Take((int)stream.Position).ToArray();
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
}
