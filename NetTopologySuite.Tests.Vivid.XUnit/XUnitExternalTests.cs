namespace NetTopologySuite.Tests.XUnit
{
    public abstract class ExternalXUnitRunner : XUnitRunner
    {
        protected ExternalXUnitRunner(string testFile) : base(testFile) { }

        private const string testLocation = "..\\..\\..\\NetTopologySuite.TestRunner.Tests\\external";

        protected override string TestLocation => testLocation;
    }

    public class TestGeosBuffer : ExternalXUnitRunner
    {
        public TestGeosBuffer() : base("GEOSBuffer.xml") { }
    }

    public class TestBufferExternal : ExternalXUnitRunner
    {
        public TestBufferExternal() : base("TestBufferExternal.xml") { }
    }

    public class TestBufferExternal2 : ExternalXUnitRunner
    {
        public TestBufferExternal2() : base("TestBufferExternal2.xml") { }
    }

    public class TestOverlay : ExternalXUnitRunner
    {
        public TestOverlay() : base("TestOverlay.xml") { }
    }
}