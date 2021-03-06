using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NUnit.Framework;

namespace NetTopologySuite.Tests.NUnit.Algorithm
{
    //Tests are exposed by AbstractPointInRingTest type
    public class RayCrossingCounterTest : AbstractPointInRingTest
    {
        private WKTReader reader = new WKTReader();

        protected override void RunPtInRing(Location expectedLoc, Coordinate pt, string wkt)
        {
            var geom = reader.Read(wkt);
            Assert.AreEqual(expectedLoc, RayCrossingCounter.LocatePointInRing(pt, geom.Coordinates));
            var poly = geom as Polygon;
            if (poly == null)
                return;

            Assert.AreEqual(expectedLoc, RayCrossingCounter.LocatePointInRing(pt, poly.ExteriorRing.CoordinateSequence));
        }

    }
}
