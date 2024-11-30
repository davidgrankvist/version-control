using VersionControl.Lib.Execution;
using VersionControl.Test.Mocks;

namespace VersionControl.Test.Execution
{
	[TestClass]
	public class ExecutorTest
	{
		private Executor executor;
		private CommandSpy command;
		private DocumentationServiceSpy documentationService;

		[TestInitialize]
		public void TestInitialize()
		{
			documentationService = new DocumentationServiceSpy();
			executor = new Executor(documentationService);
			command = new CommandSpy();
		}

		[TestMethod]
		public void ShouldExecute()
		{
			command.CanExecuteEnabled = true;

			executor.Execute(command);

			Assert.IsTrue(command.DidExecute);
		}

		[TestMethod]
		public void ShouldShowHelpIfHelpModeIsOn()
		{
			command.CanExecuteEnabled = true;

			executor.Execute(command, true);

			Assert.IsFalse(command.DidExecute);
			Assert.IsTrue(command.DidHelp);
			Assert.IsTrue(documentationService.ShownDocs.Contains(command.Docs));
		}

		[TestMethod]
		public void ShouldShowHelpIfUnableToExecute()
		{
			command.CanExecuteEnabled = false;

			executor.Execute(command);

			Assert.IsFalse(command.DidExecute);
			Assert.IsTrue(command.DidHelp);
			Assert.IsTrue(documentationService.ShownDocs.Contains(command.Docs));
		}
	}
}
