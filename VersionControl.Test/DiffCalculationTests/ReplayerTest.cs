using VersionControl.Lib.Changes;
using VersionControl.Lib.Changes.Services.DiffCalculations;
using VersionControl.Test.Framework;

namespace VersionControl.Test.DiffCalculationTests;

[TestClass]
public class ReplayerTest
{
    [DataTestMethod]
    [DynamicData(nameof(GetTestCases), DynamicDataSourceType.Method)]
    public void ShouldConstructTargetSnapshot(string[] snapshot, TestSerializationWrapper<List<LineDiffOperation>> operations, string[] expected)
    {
        var ops = DiffOperationTestHelper.DeserializeOperations(operations);

        var result = Replayer.Replay(snapshot, ops);

        Assert.IsTrue(expected.SequenceEqual(result));
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        foreach (var testCase in DiffOperationTestHelper.GetAddRemoveTestCases())
        {
            yield return new object[] { testCase.First, testCase.Operations, testCase.Second };
        }
    }
}
