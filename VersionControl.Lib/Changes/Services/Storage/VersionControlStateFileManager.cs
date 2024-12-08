namespace VersionControl.Lib.Changes.Services.Storage;
public static class VersionControlStateFileManager
{
    public static VersionControlState ParseState(Stream stream)
    {
        Guid currentChangeId;
        using (var reader = new BinaryReader(stream))
        {
            var changeIdBytes = reader.ReadBytes(16);
            currentChangeId = new Guid(changeIdBytes);
        }

        return new VersionControlState(currentChangeId.ToString());
    }

    public static void WriteState(Stream stream, VersionControlState state)
    {
        var changeIdBytes = Guid.Parse(state.CurrentChangeId).ToByteArray();
        using (var writer = new BinaryWriter(stream))
        {
            writer.Write(changeIdBytes);
        }
    }
}
