using System.IO;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.IO.GML2;
using NUnit.Framework;

namespace NetTopologySuite.Tests.Various
{
    public class Issue117Tests
    {
        [Test, Category("Issue117")]
        public void Issue117()
        {
            var geometryNts = new WKTReader().Read("POLYGON((0 0,100 0,100 100, 0 100, 0 0 )))");

            //the features must be missing. What should I do there to add the features.Thanks
            string ntsGeometry = GetGeometryUsingNTS(geometryNts);
            using (var sw = new StreamWriter(File.Create("polygon.gml")))
            {
                sw.Write(ntsGeometry);
                sw.Close();
            }
        }

        private static string GetGeometryUsingNTS(Geometry geometry)
        {
            var gmlWriter = new GMLWriter();
            var ms = new MemoryStream();

            gmlWriter.Write(geometry, ms);
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
