namespace VersionControl.Lib.Changes.Services.DiffCalculations;

public static class InverseLcsDiffCalculator
{
    public static IReadOnlyCollection<LineDiffOperation> CalculateDiff(string[] first, string[] second)
    {
        /*
         * A diff calculation in three steps:
         * 1. find the LCS
         * 2. create remove operations for lines in the LCS
         * 3. create add operations for the remaining lines
         *
         * The result is a sequence of removals followed by a sequence of additions. Not optimal, but simple.
         */
        var lcsIndices = LcsCalculator.FindLcsIndices(first, second);

        // the inverse of the LCS is what to remove/add
        var shouldNotRemove = new bool[first.Length];
        var shouldNotAdd = new bool[second.Length];
        foreach (var lcsIndex in lcsIndices)
        {
            shouldNotRemove[lcsIndex.FirstIndex] = true;
            shouldNotAdd[lcsIndex.SecondIndex] = true;
        }

        var result = new List<LineDiffOperation>();

        // the non-LCS elements of the first part are the removals
        (int Start, int End) currRemoval = (0, -1);
        var upShift = 0;
        for (var i = 0; i < first.Length; i++)
        {
            if (shouldNotRemove[i] || i == first.Length - 1)
            {
                if (!shouldNotRemove[i] && i == first.Length - 1)
                {
                    currRemoval = (currRemoval.Start, i);
                }

                var isValid = currRemoval.Start <= currRemoval.End;
                if (isValid)
                {
                    var relativeRemoval = LineDiffOperation.Removal(currRemoval.Start - upShift, currRemoval.End - upShift);
                    result.Add(relativeRemoval);
                    upShift += currRemoval.End - currRemoval.Start + 1;
                }

                currRemoval = (i + 1, -1);
            }
            else
            {
                currRemoval = (currRemoval.Start, i);
            }
        }

        // the non-LCS elements of the second part are the additions
        (int Start, int End, List<string> Data) currAddition = (0, -1, []);
        var currData = new List<string>();
        for (var i = 0; i < second.Length; i++)
        {
            if (shouldNotAdd[i] || i == second.Length - 1)
            {
                if (!shouldNotAdd[i] && i == second.Length - 1)
                {
                    currAddition = (currAddition.Start, i, currAddition.Data.Append(second[i]).ToList());
                }

                var isValid = currAddition.Start <= currAddition.End;
                if (isValid)
                {
                    var relativeAddition = LineDiffOperation.Addition(currAddition.Start, currAddition.End, currAddition.Data);
                    result.Add(relativeAddition);
                }
                currAddition = (i + 1, -1, []);
            }
            else if (!shouldNotAdd[i])
            {

                currAddition = (currAddition.Start, i, currAddition.Data.Append(second[i]).ToList());
            }
        }

        return result;
    }
}
