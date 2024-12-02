namespace VersionControl.Test.DiffCalculationTests
{
	public static class LcsCalculatorTestDataHelper
	{

		public static IEnumerable<object[]> GetLcsLengthTestCases()
		{
			foreach (var testCase in GetLcsTestCasesInternal())
			{
				yield return new object[] { testCase.First, testCase.Second, testCase.Result.Length };
			}
		}

		public static IEnumerable<object[]> GetLcsTestCases()
		{
			foreach (var testCase in GetLcsTestCasesInternal())
			{
				yield return new object[] { testCase.First, testCase.Second, testCase.Result };
			}
		}

		private static IEnumerable<(string[] First, string[] Second, string[] Result)> GetLcsTestCasesInternal()
		{
			List<(string[] First, string[] Second, string[] Result)> testCases = [
				(["A"], ["B"], []),
				(["A"], ["A"], ["A"]),
				(["A", "B"], ["A", "C"], ["A"]),
				(["A", "B"], ["C", "A"], ["A"]),
				(["A", "A", "A", "A"], ["A"], ["A"]),
				(["A", "1", "2", "A", "A", "3"], ["B", "B", "1", "B", "B", "3"], ["1", "3"]),
				(["1", "2", "A", "A", "A", "A"], ["B", "B", "B", "B", "1", "2"], ["1", "2"]),
			];

			return testCases;
		}
	}
}
