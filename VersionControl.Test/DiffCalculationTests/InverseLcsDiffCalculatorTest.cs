using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services.DiffCalculations;
using VersionControl.Test.TestHelpers;

namespace VersionControl.Test.DiffCalculationTests;

[TestClass]
public class InverseLcsDiffCalculatorTest
{
    [DataTestMethod]
    [DynamicData(nameof(InverseLcsDiffCalculatorTestDataHelper.GetTestCases), typeof(InverseLcsDiffCalculatorTestDataHelper), DynamicDataSourceType.Method)]
    public void ShouldCalculateExpectedDiff(string[] first, string[] second, TestSerializationWrapper<List<LineDiffOperation>> expected)
    {
        var expectedData = Deserialize(expected);
        var operations = InverseLcsDiffCalculator.CalculateDiff(first, second);

        Assert.IsTrue(expectedData.SequenceEqual(operations));
    }

    private static List<LineDiffOperation> Deserialize(TestSerializationWrapper<List<LineDiffOperation>> expected)
    {
        return expected.Data.Select(Listify).ToList();
    }

    private static LineDiffOperation Listify(LineDiffOperation operation)
    {
        return new LineDiffOperation(operation.OperationType, operation.Start, operation.End, operation.Data.ToList());
    }
}
