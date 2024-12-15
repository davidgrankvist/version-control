using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services;
public class ChangeWrapper
{
    public string Id { get; }
    public ChangeSet Change { get; }

    public ChangeWrapper(string id, ChangeSet change)
    {
        Id = id;
        Change = change;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ChangeWrapper change)
        {
            return false;
        }
        return Id == change.Id && Change.Equals(change.Change);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Change);
    }
}
