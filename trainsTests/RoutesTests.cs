using Microsoft.VisualStudio.TestTools.UnitTesting;
using trains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trains.Tests
{
  [TestClass()]
  public class RoutesTests
  {
    static Routes graph;
    static Node a, b, c, d, e;

    [TestInitialize]
    public void setUpData()
    {
      graph = new Routes();
      a = new Node("A");
      b = new Node("B");
      c = new Node("C");
      d = new Node("D");
      e = new Node("E");

      graph.routeTable.Add(a, new Edge(a, b, 5).nextRoute(new Edge(a, d, 5).nextRoute(new Edge(a, e, 7))));
      graph.routeTable.Add(b, new Edge(b, c, 4));
      graph.routeTable.Add(c, new Edge(c, d, 8).nextRoute(new Edge(c, e, 2)));
      graph.routeTable.Add(d, new Edge(d, c, 8).nextRoute(new Edge(d, e, 6)));
      graph.routeTable.Add(e, new Edge(e, b, 3));

    }

    // Test distance between route A-B-C
    [TestMethod()]
    public void TestDistanceBetween_ABC()
    {
      List<Node> route = new List<Node>();
      route.Add(a);
      route.Add(b);
      route.Add(c);
      Assert.AreEqual(9, graph.getDistanceAlongRoute(route));
    }

    // Test distance between route A-E-D
    [TestMethod()]
    [ExpectedException(typeof(Exception), "ROUTE DOESNT EXIST")]
    public void TestDistanceBetween_AED()
    {
      List<Node> route = new List<Node>();
      route.Add(a);
      route.Add(e);
      route.Add(d);
      Assert.AreEqual(-1, graph.getDistanceAlongRoute(route));
    }

    // Test shortest route between town A and town C
    [TestMethod]
    public void TestShortestRoute_AC()
    {
      int shortestRoute = graph.getShortestRoute(a, c);
      Assert.AreEqual(9, shortestRoute);
    }

    // Test shortest route between town B and town B
    [TestMethod]
    public void TestShortestRoute_BB()
    {
      int shortestRoute = graph.getShortestRoute(b, b);
      Assert.AreEqual(9, shortestRoute);
    }

    // Test number of routes between town A and town C with exactly 4 stops
    [TestMethod]
    public void TestNumberOfStops_AC4()
    {
      int numberOfRoutesWithin = graph.getNumberOfStops(a, c, 4);
      Assert.AreEqual(4, numberOfRoutesWithin);
    }

    // Test number of routes between town C and town C with distance less than 30
    [TestMethod]
    public void TestNumberOfRoutes_CC30()
    {
      int numberOfRoutesWithin = graph.getNumberOfRoutesWithin(c, c, 30);
      Assert.AreEqual(7, numberOfRoutesWithin);
    }

  }
}