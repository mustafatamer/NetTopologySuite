using NetTopologySuite.Geometries;

namespace NetTopologySuite.SnapRound
{
    public static class SnapRoundFunctions
    {
        /// <summary>Reduces precision pointwise, then snap-rounds.
        /// <para/>
        /// Note that output set may not contain non-unique linework
        /// (and thus cannot be used as input to Polygonizer directly).
        /// <c>UnaryUnion</c> is one way to make the linework unique.
        /// </summary>
        /// <param name="geom">A Geometry containing linework to node</param>
        /// <param name="scaleFactor">The precision model scale factor to use</param>
        /// <returns>The noded, snap-rounded linework</returns>
        public static Geometry SnapRoundLines(
            Geometry geom, double scaleFactor)
        {
            var pm = new PrecisionModel(scaleFactor);
            var gsr = new GeometrySnapRounder(pm);
            gsr.LineworkOnly =true;
            var snapped = gsr.Execute(geom);
            return snapped;
        }

        public static Geometry SnapRound(
            Geometry geomA, Geometry geomB,
            double scaleFactor)
        {
            var pm = new PrecisionModel(scaleFactor);

            var geom = geomA;

            if (geomB != null)
            {
                geom = geomA.Factory.CreateGeometryCollection(new Geometry[] { geomA, geomB });
            }

            var gsr = new GeometrySnapRounder(pm);
            var snapped = gsr.Execute(geom);
            return snapped;
        }

    }
}