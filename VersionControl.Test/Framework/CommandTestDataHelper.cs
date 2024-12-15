using VersionControl.Lib.Commands;
using VersionControl.Test.Mocks;

namespace VersionControl.Test.Framework
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
            var changeService = new ChangeServiceSpy();
            yield return (new string[] { "save", "file1", "file2" }, new CommandWrapper(new SaveCommand(changeService, new string[] { "file1", "file2" }, "")));
            yield return (new string[] { "save", "file1", "file2", "--message", "some message" }, new CommandWrapper(new SaveCommand(changeService, new string[] { "file1", "file2" }, "some message")));
            yield return (new string[] { "save", "file1", "file2", "-m", "some message" }, new CommandWrapper(new SaveCommand(changeService, new string[] { "file1", "file2" }, "some message")));
            yield return (new string[] { "history" }, new CommandWrapper(new HistoryCommand()));
            yield return (new string[] { "status" }, new CommandWrapper(new StatusCommand()));
            yield return (new string[] { "goto", "some-change-id" }, new CommandWrapper(new GotoCommand(changeService, "some-change-id")));
            yield return (new string[] { "compare", "first-change-id", "second-change-id" }, new CommandWrapper(new CompareCommand(changeService, "first-change-id", "second-change-id")));
        }
    }
}