using VersionControl.Lib.Commands;

namespace VersionControl.Test.TestHelpers
{
	public static class CommandTestDataHelper
	{
		public static IEnumerable<object[]> GetSuccessfulParseTestCasesAsObj()
		{
			foreach (var testCase in GetSuccessfulParseTestCases())
			{
				yield return new object[] { testCase.Args, testCase.Command };
			}
		}

		private static IEnumerable<(string[] Args, IVersionControlCommand Command)> GetSuccessfulParseTestCases()
		{
			yield return (new string[] { "save", "file1", "file2" }, new SaveCommand(new string[] { "file1", "file2" }));
			yield return (new string[] { "history" }, new HistoryCommand());
			yield return (new string[] { "status" }, new StatusCommand());
			yield return (new string[] { "goto", "some-change-id" }, new GotoCommand("some-change-id"));
			yield return (new string[] { "compare", "first-change-id", "second-change-id" }, new CompareCommand("first-change-id", "second-change-id"));
		}
	}
}
