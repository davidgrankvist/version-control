using System.Reflection;
using VersionControl.Lib.Commands;

namespace VersionControl.Lib.Documentation
{
	internal class ConsoleDocumentationService : IDocumentationService
	{
		private static readonly CommandDocumentation metaCommand = new CommandDocumentation("vcs", "A version control tool.", "Control your versions.");

		public void ShowCommandHelp(CommandDocumentation commandDoc)
		{
			Console.Write(BuildLongDescriptionString(commandDoc));
		}

		public void ShowGeneralHelp()
		{
			Console.Write(BuildGeneralHelpString());
		}

		private string BuildGeneralHelpString()
		{
			var desc = BuildLongDescriptionString(metaCommand);
			var cmdList = BuildCommandListString();

			return desc + Environment.NewLine + Environment.NewLine + cmdList;
		}

		private string BuildShortDescriptionString(CommandDocumentation commandDoc, bool withTab = false)
		{
			var tabStr = withTab ? "\t" : " ";
			return $"{commandDoc.Name}{tabStr}- {commandDoc.Description}";
		}

		private string BuildLongDescriptionString(CommandDocumentation commandDoc)
		{
			var shortDesc = BuildShortDescriptionString(commandDoc);
			var longDesc = shortDesc + Environment.NewLine + Environment.NewLine + commandDoc.Summary;

			return longDesc;
		}

		private string BuildCommandListString()
		{
			var title = "Commands:";
			var cmds = GetCommandDocs();

			return title + Environment.NewLine + string.Join(Environment.NewLine, cmds.Select(c => BuildShortDescriptionString(c, true)));
		}

		private IEnumerable<CommandDocumentation> GetCommandDocs()
		{
			var docs = new List<CommandDocumentation>();

			var asm = Assembly.GetExecutingAssembly();
			var cmdTypes = asm.GetTypes().Where(t => typeof(IVersionControlCommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
			foreach (var cmdType in cmdTypes)
			{
				var obj = Activator.CreateInstance(cmdType);
				if (obj is not IVersionControlCommand cmd)
				{
					continue;
				}

				var doc = cmd.Help();
				docs.Add(doc);
			}

			return docs;
		}
	}
}
