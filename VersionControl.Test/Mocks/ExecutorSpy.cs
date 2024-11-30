using VersionControl.Lib.Commands;
using VersionControl.Lib.Execution;

namespace VersionControl.Test.Mocks
{
	public class ExecutorSpy : IExecutor
	{
		public List<(IVersionControlCommand Command, bool HelpMode)> Calls { get; } = [];

		public void Execute(IVersionControlCommand command, bool helpMode = false)
		{
			Calls.Add((command, helpMode));
		}
	}
}
