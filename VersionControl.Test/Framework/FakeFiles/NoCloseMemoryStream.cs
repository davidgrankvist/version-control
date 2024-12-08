namespace VersionControl.Test.Framework.FakeFiles;

/// <summary>
/// A non-closing memory stream. This allows for reading the position after the stream has ended.
/// </summary>
public class NoCloseMemoryStream : MemoryStream
{
    public override void Close()
    {
        // intentionally do nothing
    }

    /// <summary>
    /// Collect the portion of the stream buffer that has been populated with data.
    /// </summary>
    public byte[] Collect()
    {
        return GetBuffer().Take((int)Position).ToArray();
    }
}
