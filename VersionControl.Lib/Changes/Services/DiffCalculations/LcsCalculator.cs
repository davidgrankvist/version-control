namespace VersionControl.Lib.Changes.Services.DiffCalculations
{
    /// <summary>
    /// Longest Common Subsequence (LCS) calculator.
    /// </summary>
    public static class LcsCalculator
    {
        public static string[] FindLcs(string[] first, string[] second)
        {
            // Step 1. Calculate LCS of all leading substrings
            var lcs = FindLcsLengthOfSubStrings(first, second);
            var lcsLength = lcs[first.Length, second.Length];

            // Step 2. Start in the bottom right corner of the result matrix and backtrack the maximum path
            var result = new string[lcsLength];
            var i = first.Length;
            var j = second.Length;
            var iResult = lcsLength - 1;
            while (iResult >= 0)
            {
                if (first[i - 1] == second[j - 1])
                {
                    result[iResult] = first[i - 1];
                    i--;
                    j--;
                    iResult--;
                }
                else if (lcs[i - 1, j] > lcs[i, j - 1])
                {
                    i--;
                }
                else
                {
                    j--;
                }
            }

            return result;
        }

        public static int FindLcsLength(string[] first, string[] second)
        {
            var lcs = FindLcsLengthOfSubStrings(first, second);
            return lcs[first.Length, second.Length];
        }

        private static int[,] FindLcsLengthOfSubStrings(string[] first, string[] second)
        {
            /*
			 * LCS can be found recursively by calculating it for the leading substrings.
			 * The implementation below uses this recursion (optimized with dynamic programming, iterative).
			 */
            var lcs = new int[first.Length + 1, second.Length + 1];

            for (var i = 1; i <= first.Length; i++)
            {
                for (var j = 1; j <= second.Length; j++)
                {
                    if (first[i - 1] == second[j - 1])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        lcs[i, j] = Math.Max(lcs[i - 1, j], lcs[i, j - 1]);
                    }
                }
            }

            return lcs;
        }
    }
}