using Microsoft.Testing.Platform.Extensions.Messages;

using VersionControl.Lib.Changes;
using VersionControl.Test.TestHelpers;

namespace VersionControl.Test.DiffCalculationTests;

using Ldo = LineDiffOperation;
using Tw = TestSerializationWrapper<List<LineDiffOperation>>;

public static class InverseLcsDiffCalculatorTestDataHelper
{
    public static IEnumerable<object[]> GetTestCases()
    {
        foreach (var testCase in GetTestCasesInternal())
        {
            yield return new object[] { testCase.First, testCase.Second, testCase.Expected };
        }
    }

    private static List<(string[] First, string[] Second, Tw Expected)> GetTestCasesInternal()
    {
        List<(string[] First, string[] Second, List<Ldo> Expected)> testCases = [
            // no diff
            ([], [], []),
            (["A"], ["A"], []),
            (["A", "A", "A"], ["A", "A", "A"], []),
            (["A", "B", "C"], ["A", "B", "C"], []),
            // removal only
            (["A"], [], [Ldo.Removal(0, 0)]),
            (["A", "B"], ["A"], [Ldo.Removal(1, 1)]),
            (["A", "B"], [], [Ldo.Removal(0, 1)]),
            (["A", "B", "C"], ["A"], [Ldo.Removal(1, 2)]),
            (["A", "B", "C", "D"], ["A", "D"], [Ldo.Removal(1, 2)]),
            (["A", "B", "C", "D"], ["A", "C"], [Ldo.Removal(1, 1), Ldo.Removal(2, 2)]),
            (["A", "B", "C", "D", "E", "F", "G"], ["A", "D", "G"], [Ldo.Removal(1, 2), Ldo.Removal(2, 3)]),
            // addition only
            ([], ["A"], [Ldo.Addition(0, 0, ["A"])]),
            ([], ["A", "B"], [Ldo.Addition(0, 1, ["A", "B"])]),
            (["A", "B"], ["A", "B", "C", "D"], [Ldo.Addition(2, 3, ["C", "D"])]),
            (["A", "C"], ["A", "B", "C", "D"], [Ldo.Addition(1, 1, ["B"]), Ldo.Addition(3, 3, ["D"])]),
            (["A", "D", "G"], ["A", "B", "C", "D", "E", "F", "G"], [Ldo.Addition(1, 2, ["B", "C"]), Ldo.Addition(4, 5, ["E", "F"])]),
            // mixed
            (["A", "B"], ["A" ,"C"], [Ldo.Removal(1, 1), Ldo.Addition(1, 1, ["C"])]),
            (["A", "D", "F"], ["A", "B", "C", "E", "F"], [Ldo.Removal(1, 1), Ldo.Addition(1, 3, ["B", "C", "E"])]),
            (["A", "B", "C"], ["X", "B", "Y"], [Ldo.Removal(0, 0), Ldo.Removal(1, 1), Ldo.Addition(0, 0, ["X"]), Ldo.Addition(2, 2, ["Y"])]),
            ];

        return testCases.Select(x => (x.First, x.Second, new Tw(x.Expected))).ToList();
    }
}
