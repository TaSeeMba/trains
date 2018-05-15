using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trains
{
  class Node
  {
    public String name;
    public Boolean visited;

    public Node (String name)
    {
      this.name = name;
      this.visited = false;
    }
  }
}
