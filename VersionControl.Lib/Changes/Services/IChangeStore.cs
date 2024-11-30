using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services
{
	public interface IChangeStore
	{
		void Save(ChangeSet changeSet);

		ChangeSet GetChange(string changeId);

		FileSnapshot GetSnapshot(string fileId, string? changeId = null);

		string GetCurrentChangeId();
	}
}
