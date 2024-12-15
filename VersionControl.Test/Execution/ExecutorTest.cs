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
        private ConsoleServiceSpy consoleService;

        [TestInitialize]
        public void TestInitialize()
        {
            documentationService = new DocumentationServiceSpy();
            consoleService = new ConsoleServiceSpy();
            executor = new Executor(documentationService, consoleService);
            command = new CommandSpy();
        }

        [TestMethod]
        public void ShouldExecute()
        {
            command.CanExecuteEnabled = true;

            executor.Execute(command);

            Assert.IsTrue(command.DidExecute);
            Assert.AreEqual(1, consoleService.Messages.Count);
        }

        [TestMethod]
        public void ShouldShowHelpIfHelpModeIsOn()
        {
            command.CanExecuteEnabled = true;

            executor.Execute(command, true);

            Assert.IsFalse(command.DidExecute);
            Assert.IsTrue(command.DidHelp);
            Assert.IsTrue(documentationService.ShownDocs.Contains(command.Docs));
            Assert.AreEqual(1, consoleService.Messages.Count);
        }

        [TestMethod]
        public void ShouldShowHelpIfUnableToExecute()
        {
            command.CanExecuteEnabled = false;

            executor.Execute(command);

            Assert.IsFalse(command.DidExecute);
            Assert.IsTrue(command.DidHelp);
            Assert.IsTrue(documentationService.ShownDocs.Contains(command.Docs));
            Assert.AreEqual(1, consoleService.Messages.Count);
        }

        [TestMethod]
        public void ShouldShowGeneralHelpIfNullCommand()
        {
            executor.Execute(null);

            Assert.IsTrue(documentationService.DidBuildGeneralHelp);
            Assert.AreEqual(1, consoleService.Messages.Count);
        }
    }
}