using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NUnit.Framework;

namespace NetTopologySuite.Tests.NUnit.Operation.Relate
{
    public class ContainsTest
{
  private readonly WKTReader _reader = new WKTReader(new GeometryFactory());

  /**
   * From GEOS #572
   * A case where B is contained in A, but
   * the JTS relate algorithm fails to compute this correctly.
   *
   * The cause is that the long segment in A nodes the single-segment line in B.
   * The node location cannot be computed precisely.
   * The node then tests as not lying precisely on the original long segment in A.
   *
   * The solution is to change the relate algorithm so that it never computes
   * new intersection points, only ones which occur at existing vertices.
   * (The topology of the implicit intersections can still be computed
   * to contribute to the intersection matrix result).
   * This will require a complete reworking of the relate algorithm.
   */
  [Test, Ignore("Known to fail")]
    public void TestContainsIncorrect()
    {
        var a = _reader.Read("LINESTRING (1 0, 0 2, 0 0, 2 2)");
        var b = _reader.Read("LINESTRING (0 0, 2 2)");
        Assert.IsTrue(a.Contains(b));
    }
}}