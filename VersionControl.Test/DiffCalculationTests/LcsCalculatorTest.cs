using VersionControl.Lib.Changes.Services.DiffCalculations;

namespace VersionControl.Test.DiffCalculationTests
{
    [TestClass]
    public class LcsCalculatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(LcsCalculatorTestDataHelper.GetLcsLengthTestCases), typeof(LcsCalculatorTestDataHelper), DynamicDataSourceType.Method)]
        public void ShouldCalculateLcsLength(string[] first, string[] second, int expected)
        {
            Assert.AreEqual(expected, LcsCalculator.FindLcsLength(first, second));
        }

        [DataTestMethod]
        [DynamicData(nameof(LcsCalculatorTestDataHelper.GetLcsTestCases), typeof(LcsCalculatorTestDataHelper), DynamicDataSourceType.Method)]
        public void ShouldCalculateLcs(string[] first, string[] second, string[] expected)
        {
            Assert.IsTrue(expected.SequenceEqual(LcsCalculator.FindLcs(first, second)));
        }
    }
}