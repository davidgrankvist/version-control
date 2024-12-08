namespace VersionControl.Test.Storage.FakeFiles;

/// <summary>
/// A non-closing memory stream. This allows for reading the position after the stream has ended.
/// </summary>
public class NoCloseMemoryStream : MemoryStream
{
    public override void Close()
    {
        // intentionally do nothing
    }
}
