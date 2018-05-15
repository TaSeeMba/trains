using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trains
{
  class Edge
  {
    // Origin town
    public Node origin;
    // Destination town
    public Node destination;
    // Weight of route to destination
    public int weight;
    // Next possible route
    public Edge next;

    public Edge(Node origin, Node destination, int weight)
    {
      this.origin = origin;
      this.destination = destination;
      this.weight = weight;
      this.next = null;
    }

    public Edge nextRoute(Edge edge)
    {
      this.next = edge;
      return this;
    }

  }
}
