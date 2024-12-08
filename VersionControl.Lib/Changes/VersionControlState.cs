namespace VersionControl.Lib.Changes;
public readonly struct VersionControlState
{
    public string CurrentChangeId { get; }

    public VersionControlState(string currentChangeId)
    {
        CurrentChangeId = currentChangeId;
    }
}