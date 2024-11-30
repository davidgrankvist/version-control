using VersionControl.Lib.Commands;

namespace VersionControl.Test.TestHelpers
{
	using CommandWrapper = TestSerializationWrapper<IVersionControlCommand>;

	public static class CommandTestDataHelper
	{
		public static IEnumerable<object[]> GetSuccessfulParseTestCases()
		{
			foreach (var testCase in GetSuccessfulParseTestCasesAsTuples())
			{
				yield return new object[] { testCase.Args, testCase.Wrapper };
			}
		}

		private static IEnumerable<(string[] Args, CommandWrapper Wrapper)> GetSuccessfulParseTestCasesAsTuples()
		{
			yield return (new string[] { "save", "file1", "file2" }, new CommandWrapper(new SaveCommand(new string[] { "file1", "file2" })));
			yield return (new string[] { "history" }, new CommandWrapper(new HistoryCommand()));
			yield return (new string[] { "status" }, new CommandWrapper(new StatusCommand()));
			yield return (new string[] { "goto", "some-change-id" }, new CommandWrapper(new GotoCommand("some-change-id")));
			yield return (new string[] { "compare", "first-change-id", "second-change-id" }, new CommandWrapper(new CompareCommand("first-change-id", "second-change-id")));
		}
	}
}
