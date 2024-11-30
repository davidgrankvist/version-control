using VersionControl.Lib.Commands;
using VersionControl.Lib.Parsing;
using VersionControl.Test.TestHelpers;

namespace VersionControl.Test.Parsing
{
	[TestClass]
	public class ArgumentParserTest
	{
		[DataTestMethod]
		[DataRow(new string[] { "status", "--help" })]
		[DataRow(new string[] { "status", "-h" })]
		public void ShouldEnableHelpMode(string[] args)
		{
			var (command, helpMode) = ArgumentParser.Parse(args);

			Assert.IsTrue(helpMode);
		}

		[DataTestMethod]
		[DataRow(new string[] { "somethingUnknown" })]
		[DataRow(new string[] { })]
		public void ShouldNotParseAsCommand(string[] args)
		{
			var (command, _) = ArgumentParser.Parse(args);

			Assert.IsNull(command);
		}

		[DataTestMethod]
		[DynamicData(nameof(CommandTestDataHelper.GetSuccessfulParseTestCasesAsObj), typeof(CommandTestDataHelper), DynamicDataSourceType.Method)]
		public void ShouldParseAsCommand(string[] args, IVersionControlCommand expected)
		{
			var (command, helpMode) = ArgumentParser.Parse(args);

			Assert.AreEqual(expected, command);
		}
	}
}
