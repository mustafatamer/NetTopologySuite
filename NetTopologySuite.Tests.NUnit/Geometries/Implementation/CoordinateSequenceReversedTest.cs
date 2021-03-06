using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace NetTopologySuite.Tests.NUnit.Geometries.Implementation
{
    public class CoordinateSequenceReversedTest
    {
        [Test]
        public void TestCoordinateArraySequence()
        {
            var csf = new NetTopologySuite.Geometries.Implementation.CoordinateArraySequence(
                new[] {new Coordinate(0, 0), new Coordinate(10, 10), new Coordinate(10, 0), new Coordinate(0, 0),});
            var csr = csf.Reversed();
            DoTest(csf, csr);
        }

        [Test]
        public void TestDotSpatialAffineCoordinateSequence()
        {
            var csf = new NetTopologySuite.Geometries.Implementation.DotSpatialAffineCoordinateSequence(
                new[] { 0d, 0d, 10d, 10d, 10d, 0d, 0d, 0d, }, new []{ 1d, 2, 3, 4 }, new [] { 4, 3, 2, 1d} );
            var csr = csf.Reversed();
            DoTest(csf, csr);
        }

        [Test]
        public void TestPackedDoubleCoordinateSequence()
        {
            var csf = new NetTopologySuite.Geometries.Implementation.PackedDoubleCoordinateSequence(
                new[] { 0d, 0d, 1d, 4d, 10d, 10d, 2d, 3d, 10d, 0d, 3d, 2d, 0d, 0d, 4d, 1d}, 4, 1);
            var csr = csf.Reversed();
            DoTest(csf, csr);
        }

        [Test]
        public void TestPackedFloatCoordinateSequence()
        {
            var csf = new NetTopologySuite.Geometries.Implementation.PackedFloatCoordinateSequence(
                new[] { 0f, 0f, 1f, 4f, 10f, 10f, 2f, 3f, 10f, 0f, 3f, 2f, 0d, 0d, 4d, 1d }, 4, 1);
            var csr = csf.Reversed();
            DoTest(csf, csr);
        }

        private static void DoTest(CoordinateSequence forward, CoordinateSequence reversed)
        {
            const double eps = 1e-12;

            Assert.AreEqual(forward.Count, reversed.Count, "Coordinate sequences don't have same size");
            Assert.AreEqual(forward.Ordinates, reversed.Ordinates, "Coordinate sequences don't serve same ordinate values");

            var ordinates = ToOrdinateArray(forward.Ordinates);
            int j = forward.Count;
            for (int i = 0; i < forward.Count; i++)
            {
                j--;
                foreach(var ordinate in ordinates)
                    Assert.AreEqual(forward.GetOrdinate(i, ordinate), reversed.GetOrdinate(j, ordinate), eps, string.Format("{0} values are not within tolerance", ordinate));
                var cf = forward.GetCoordinate(i);
                var cr = reversed.GetCoordinate(j);

                Assert.IsFalse(ReferenceEquals(cf, cr), "Coordinate sequences deliver same coordinate instances");
                Assert.IsTrue(cf.Equals(cr), "Coordinate sequences do not provide equal coordinates");
            }
        }

        private static Ordinate[] ToOrdinateArray(Ordinates ordinates)
        {
            var result = new Ordinate[OrdinatesUtility.OrdinatesToDimension(ordinates)];

            int nextResultIndex = 0;
            for (int i = 0; i < 32; i++)
            {
                if (ordinates.HasFlag((Ordinates)(1 << i)))
                {
                    result[nextResultIndex++] = (Ordinate)i;
                }
            }

            return result;
        }
    }
}
