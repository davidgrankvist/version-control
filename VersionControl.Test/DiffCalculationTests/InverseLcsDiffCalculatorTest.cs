using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services.DiffCalculations;
using VersionControl.Test.Framework;

namespace VersionControl.Test.DiffCalculationTests;

[TestClass]
public class InverseLcsDiffCalculatorTest
{
    [DataTestMethod]
    [DynamicData(nameof(GetTestCases), DynamicDataSourceType.Method)]
    public void ShouldCalculateExpectedDiff(string[] first, string[] second, TestSerializationWrapper<List<LineDiffOperation>> expected)
    {
        var expectedData = DiffOperationTestHelper.DeserializeOperations(expected);
        var operations = InverseLcsDiffCalculator.CalculateDiff(first, second);

        Assert.IsTrue(expectedData.SequenceEqual(operations));
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        foreach (var testCase in DiffOperationTestHelper.GetAddRemoveTestCases())
        {
            yield return new object[] { testCase.First, testCase.Second, testCase.Operations };
        }
    }
}
