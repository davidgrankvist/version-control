namespace VersionControl.Lib.Changes.Services.Storage;
public static class ChangeLogIndexFileManager
{
    public static long ParseIndex(Stream stream, string changeId)
    {
        var result = -1L;
        var id = Guid.Parse(changeId);
        var end = stream.Length;
        using (var reader = new BinaryReader(stream))
        {
            var pos = reader.BaseStream.Position;
            while (pos < end)
            {
                var parsedId = ParseId(reader);
                var offset = reader.ReadInt64();
                if (parsedId == id)
                {
                    result = offset;
                    break;
                }
            }
        }

        return result;
    }

    private static Guid ParseId(BinaryReader reader)
    {
        var bytes = reader.ReadBytes(16);
        return new Guid(bytes);
    }

    public static void Append(Stream stream, string changeId, long offset)
    {
        var idBytes = Guid.Parse(changeId).ToByteArray();
        using (var writer = new BinaryWriter(stream))
        {
            writer.BaseStream.Position = stream.Length;
            writer.Write(idBytes);
            writer.Write(offset);
        }
    }
}
