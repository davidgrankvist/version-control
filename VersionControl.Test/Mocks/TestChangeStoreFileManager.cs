using VersionControl.Lib.Changes.Services.Storage;
using VersionControl.Lib.IO;
using VersionControl.Test.Framework.FakeFiles;

namespace VersionControl.Test.Mocks;

/// <summary>
/// Sets up fake files for the change store.
/// </summary>
public class TestChangeStoreFileManager : IFileManager
{
    private readonly AppendableFakeFile log;
    private readonly AppendableFakeFile logIndex;
    private readonly FakeFile stateFile;

    public TestChangeStoreFileManager()
    {
        log = new AppendableFakeFile();
        logIndex = new AppendableFakeFile();
        stateFile = new FakeFile();
    }

    public Stream Append(string path)
    {
        // append behavior is handled by the fake file implementation
        return Write(path);
    }

    public Stream Write(string path)
    {
        var file = GetFile(path);
        return file.Write();
    }

    public byte[] ReadAllBytes(string path)
    {
        throw new NotImplementedException();
    }

    public Stream ReadFile(string path)
    {
        var file = GetFile(path);
        return file.Read();
    }

    private IFakeFile GetFile(string path)
    {
        IFakeFile file;

        if (path == ChangeStore.LogPath)
        {
            file = log;
        }
        else if (path == ChangeStore.LogIndexPath)
        {
            file = logIndex;
        }
        else if (path == ChangeStore.StatePath)
        {
            file = stateFile;
        }
        else
        {
            throw new InvalidOperationException($"No fake file provided for path {path}");
        }

        return file;
    }

    /// <summary>
    /// Ensure that appended data is ready for reading.
    /// </summary>
    public void CollectAppended()
    {
        log.CollectAppended();
        logIndex.CollectAppended();
    }
}
