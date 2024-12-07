using VersionControl.Lib.Changes;
using VersionControl.Test.TestHelpers;

namespace VersionControl.Test.DiffCalculationTests;

using Ldo = LineDiffOperation;
using Tw = TestSerializationWrapper<List<LineDiffOperation>>;

public static class DiffOperationTestHelper
{
    public static List<(string[] First, string[] Second, Tw Operations)> GetAddRemoveTestCases()
    {
        List<(string[] First, string[] Second, List<Ldo> Operations)> testCases = [
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

        return testCases.Select(x => (x.First, x.Second, new Tw(x.Operations))).ToList();
    }

    public static List<LineDiffOperation> DeserializeOperations(TestSerializationWrapper<List<LineDiffOperation>> expected)
    {
        return expected.Data.Select(Listify).ToList();
    }

    private static LineDiffOperation Listify(LineDiffOperation operation)
    {
        return new LineDiffOperation(operation.OperationType, operation.Start, operation.End, operation.Data.ToList());
    }
}
