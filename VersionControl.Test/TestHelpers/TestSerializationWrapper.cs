namespace VersionControl.Test.TestHelpers
{
    /// <summary>
    /// This is a workaround to make data driven test work. MsTest needs to serialize the objects.
    /// During that process it uses the default constructor if it is available, so the previously
    /// passed in parameters may be lost. This class intentionally has no default constructor to avoid that behavior.
    /// </summary>
    public class TestSerializationWrapper<T>
    {
        public T Data { get; }

        public TestSerializationWrapper(T command)
        {
            Data = command;
        }
    }
}