using VersionControl.Lib.Storage;

namespace VersionControl.Lib.Changes.Services
{
	public class ChangeService : IChangeService
	{
		private readonly IChangeStore store;
		private readonly IPathResolver pathResolver;

		public ChangeService(IChangeStore store, IPathResolver pathResolver)
		{
			this.store = store;
			this.pathResolver = pathResolver;
		}

		public void Save(IReadOnlyCollection<string> filePaths)
		{
			var changes = new List<FileChange>();

			var resolvedFilePaths = pathResolver.Resolve(filePaths);
			var currentChangeId = store.GetCurrentChangeId();

			foreach (var filePath in filePaths)
			{
				var change = GetChange(filePath, currentChangeId);
				changes.Add(change);
			}

			var changeSet = new ChangeSet(changes);
			store.Save(changeSet);
		}

		private FileChange GetChange(string filePath, string currentChangeId)
		{
			var prevSnapshot = store.GetSnapshot(filePath, currentChangeId);
			var newSnapshot = store.GetSnapshot(filePath);

			var change = Differ.CalculateChange(prevSnapshot, newSnapshot);
			return change;
		}
	}
}
