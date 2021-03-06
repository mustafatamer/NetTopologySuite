using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.Operation.Relate;
using NUnit.Framework;

namespace NetTopologySuite.Tests.NUnit.Operation.Relate
{
    /**
     * Tests {@link Geometry#relate}.
     *
     * @author Martin Davis
     * @version 1.7
     */

    public class RelateTest
    {
        private static readonly WKTReader Reader = new WKTReader(new GeometryFactory());

        /**
         * From GEOS #572
         *
         * The cause is that the longer line nodes the single-segment line.
         * The node then tests as not lying precisely on the original longer line.
         *
         * @throws Exception
         */

        [Test]
        [Ignore("Known to fail")]
        public void TestContainsIncorrectIntersectionMatrix()
        {
            string a = "LINESTRING (1 0, 0 2, 0 0, 2 2)";
            string b = "LINESTRING (0 0, 2 2)";
            RunRelateTest(a, b, "101F00FF2");
        }

        private static void RunRelateTest(string wkt1, string wkt2, string expectedIM)
        {
            var g1 = Reader.Read(wkt1);
            var g2 = Reader.Read(wkt2);
            var im = RelateOp.Relate(g1, g2);
            string imStr = im.ToString();
            //System.Console.WriteLine("expected: {0}", expectedIM);
            //System.Console.WriteLine("result:   {0}", imStr);
            Assert.IsTrue(im.Matches(expectedIM));
        }
    }
}