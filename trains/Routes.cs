using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trains
{
  public class Routes
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

      for (int i = 0; i < towns.Count - 1; i++)
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
          throw new Exception("ROUTE DOESNT EXIST");
        }
      }

      // If edge depth is not equal to vertex - 1, it is safe to assume that one or more routes do not exist
      if (depth != towns.Count - 1)
      {
        throw new Exception("ROUTE DOESNT EXIST");
      }
      return distance;
    }

    public int getNumberOfStops(Node start, Node end, int maxStops)
    {
      //Wrapper to maintain depth of traversal
      return findNumberOfRoutes(start, end, 0, maxStops);
    }

    public int findNumberOfRoutes(Node origin, Node destination, int depth, int maxStops)
    {
      int routes = 0;
      // Sanity check to verify that origin and destination nodes exist in the route table
      if (this.routeTable.ContainsKey(origin) && this.routeTable.ContainsKey(destination))
      {
        depth++;
        if (depth > maxStops)
        {
          return 0;
        }
        origin.visited = true;
        Edge edge;
        bool hasValue = this.routeTable.TryGetValue(origin, out Edge value);
        if (hasValue)
        {
          edge = value;
          while (edge != null)
          {
            if (edge.destination.Equals(destination))
            {
              routes++;
              edge = edge.next;
              continue;
            }
            else if (!edge.destination.visited)
            {
              routes += findNumberOfRoutes(edge.destination, destination, depth, maxStops);
              depth--;
            }
            edge = edge.next;
          }
        }
      }
      else
      {
        throw new Exception("ROUTE DOESNT EXIST");
      }
      origin.visited = false;
      return routes;
    }

    public int getShortestRoute(Node start, Node end)
    {
      return computeShortestRoute(start, end, 0, 0);
    }

    public int computeShortestRoute(Node origin, Node destination, int weight, int shortestRoute)
    {
      // Sanity check to verify that origin and destination nodes exist in the route table
      if (this.routeTable.ContainsKey(origin) && this.routeTable.ContainsKey(destination))
      {
        origin.visited = true;
        Edge edge;
        bool hasValue = this.routeTable.TryGetValue(origin, out Edge value);
        if (hasValue)
        {
          edge = value;
          while (edge != null)
          {
            if (edge.destination == destination || !edge.destination.visited)
            {
              weight += edge.weight;
            }

            if (edge.destination.Equals(destination))
            {
              if (shortestRoute == 0 || weight < shortestRoute)
              {
                shortestRoute = weight;
              }
              origin.visited = false;
              return shortestRoute;
            }
            else if (!edge.destination.visited)
            {
              shortestRoute = computeShortestRoute(edge.destination, destination, weight, shortestRoute);
              weight -= edge.weight;
            }
            edge = edge.next;
          }
        }
      }
      else
      {
        throw new Exception("ROUTE DOESNT EXIST");
      }
      origin.visited = false;
      return shortestRoute;
    }

    public int getNumberOfRoutesWithin(Node start, Node end, int maxDistance)
    {
      return computeNumberOfRoutesWithin(start, end, 0, maxDistance);
    }

    public int computeNumberOfRoutesWithin(Node origin, Node destination, int weight, int maxDistance)
    {
      int routes = 0;
      // Sanity check to verify that origin and destination nodes exist in the route table
      if (this.routeTable.ContainsKey(origin) && this.routeTable.ContainsKey(destination))
      {
        Edge edge;
        bool hasValue = this.routeTable.TryGetValue(origin, out Edge value);
        if (hasValue)
        {
          edge = value;
          while (edge != null)
          {
            weight += edge.weight;
            if (weight <= maxDistance)
            {
              if (edge.destination.Equals(destination))
              {
                routes++;
                routes += computeNumberOfRoutesWithin(edge.destination, destination, weight, maxDistance);
                edge = edge.next;
                continue;
              }
              else
              {
                routes += computeNumberOfRoutesWithin(edge.destination, destination, weight, maxDistance);
                weight -= edge.weight;
              }
            }
            else
              weight -= edge.weight;

            edge = edge.next;
          }
        }
      }
      else
      {
        throw new Exception("ROUTE DOESNT EXIST");
      }
      return routes;
    }
  }
}
