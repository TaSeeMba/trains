using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trains
{
  class Program
  {
    static void Main(string[] args)
    {
      // read nodes and edges plain text data from file system
      List<String> nodesPT = readTextFile("nodes.json");
      List<String> edgesPT = readTextFile("edges.json");

      // TODO : convert plain text data of nodes and edges to models applied in this project 

      // TODO : generate graph from nodes and edges data

      // TODO : start interactive console
    }

    // reads text file from JSON file from file system.
    public static List<String> readTextFile(String fileName)
    {
      string reader = File.ReadAllText(@".\" + fileName);
      return JsonConvert.DeserializeObject<List<string>>(reader);
    }

    // generate a list of edges from user input of edges supplied in input text file
    public static List<Edge> generateEdgesFromJsonInput(List<string> input)
    {
      if (input == null || !input.Any())
      {
        return null;
      }
      else
      {
        List<Edge> edges = new List<Edge>();
        foreach (var e in input)
        {
          var o = new Node(e[0].ToString());
          var d = new Node(e[1].ToString());
          var w = int.Parse(e.Substring(2));

          Edge edge = new Edge(o, d, w);
          edges.Add(edge);
        }
        return edges;
      }
    }

    // generate a list of nodes from user input of nodes supplied in input text file
    public static List<Node> generateNodesFromJsonInput(List<string> input)
    {
      if (input == null || !input.Any())
      {
        return null;
      }
      else
      {
        List<Node> nodes = new List<Node>();
        foreach (var n in input)
        {
          var node = new Node(n[0].ToString());
          nodes.Add(node);
        }
        return nodes;
      }
    }
  }
}
