using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trains
{
  public class Node
  {
    public String name;
    public Boolean visited;

    public Node (String name)
    {
      this.name = name;
      visited = false;
    }

    
    public override bool Equals(Object b)
    {
      if (b == null || b.GetType() != GetType())
      {
        return false;
      }
      Node bx = (Node)b;
      return this.name.Equals(bx.name);
    }

    public override int GetHashCode()
    {
      if (this.name == null) return 0;
      return this.name.GetHashCode();
    }
  }
}
