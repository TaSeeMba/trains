using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trains
{
  class Routes
  {
    public Dictionary<Node, Edge> routeTable;

    public Routes()
    {
      this.routeTable = new Dictionary<Node, Edge>(); 
    }

    // Calculate distance of a specific path
    public int getDistanceAlongRoute(List<Node> towns) 
    {
      // there is no distance between null, zero or one towns
      if (towns == null || towns.Count < 2)
      {
        return 0;
      }

      int distance, depth; 
      distance = depth = 0;

      for (int i = 0; i < towns.Count; i++)
      {
        Node town = towns.ElementAt(i); 
        if (this.routeTable.ContainsKey(town))
        {
          Edge route; 
          bool hasValue = this.routeTable.TryGetValue(town, out Edge value);
          if (hasValue)
          {
            route = value;
            // check if route from current town to next city exists
            while (route != null)
            {
              if (route.destination.Equals(towns.ElementAt(i + 1)))
              {
                distance += route.weight;
                depth++;
                break;
              }
              route = route.next;
            }
          }
        }
        else
        {
          // throw an exception
        }
      }

      // If edge depth is not equal to vertex - 1, it is safe to assume that one or more routes do not exist
      if (depth != towns.Count - 1)
      {
        // throw an exception
      }
      return distance;
    }

  }
}
