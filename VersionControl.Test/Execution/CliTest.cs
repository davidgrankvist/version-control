using VersionControl.Lib.Commands;
using VersionControl.Lib.Execution;
using VersionControl.Test.Mocks;
using VersionControl.Test.TestHelpers;

namespace VersionControl.Test.Execution
{
	[TestClass]
	public class CliTest
	{
		private Cli cli;
		private ExecutorSpy executor;
		private DocumentationServiceSpy documentationService;

		[TestInitialize]
		public void TestInitialize()
		{
			documentationService = new DocumentationServiceSpy();
			executor = new ExecutorSpy();
			cli = new Cli(executor, documentationService);
		}

		[DataTestMethod]
		[DataRow(new string[] { "asdfadsf" })]
		[DataRow(new string[] { })]
		public void ShouldShowGeneralHelp(string[] args)
		{
			cli.Run(args);

			Assert.IsTrue(documentationService.DidShowGeneralHelp);
			Assert.AreEqual(0, executor.Calls.Count);
		}

		[DataTestMethod]
		[DataRow(new string[] { "status", "--help" })]
		[DataRow(new string[] { "status", "-h" })]
		public void ShouldShowCommandHelp(string[] args)
		{
			cli.Run(args);

			Assert.AreEqual(1, executor.Calls.Count);
			Assert.IsTrue(executor.Calls.First().HelpMode);
		}

		[DataTestMethod]
		[DynamicData(nameof(CommandTestDataHelper.GetSuccessfulParseTestCasesAsObj), typeof(CommandTestDataHelper), DynamicDataSourceType.Method)]
		public void ShouldExecuteCommand(string[] args, IVersionControlCommand command)
		{
			cli.Run(args);

			Assert.AreEqual(1, executor.Calls.Count);
			Assert.AreEqual(command, executor.Calls.First().Command);
			Assert.IsFalse(executor.Calls.First().HelpMode);
		}
	}
}
