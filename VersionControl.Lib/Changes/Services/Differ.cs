using System.Text;

using VersionControl.Lib.Changes.Services.DiffCalculations;

namespace VersionControl.Lib.Changes.Services
{
    public class Differ : IDiffer
    {
        public FileChange CalculateChange(FileSnapshot prevSnapshot, FileSnapshot newSnapshot)
        {
            // assume text for now
            var prevLines = ToTextLines(prevSnapshot);
            var newLines = ToTextLines(newSnapshot);
            var operations = InverseLcsDiffCalculator.CalculateDiff(prevLines, newLines);
            var change = new FileChange(FileChangeEvent.Update, prevSnapshot.FilePath, operations);

            return change;
        }

        private static string[] ToTextLines(FileSnapshot snapshot)
        {
            if (snapshot.Data.Length == 0)
            {
                return [];
            }

            var dataStr = Encoding.UTF8.GetString(snapshot.Data).ReplaceLineEndings();
            return dataStr.Split(Environment.NewLine);
        }

    }
}