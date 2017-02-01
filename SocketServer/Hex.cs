using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServer
{
 public   class Hex
 {

     public int x;
     public int y;
     public string name;
     public int TileTypeIndex;

     public  Transform transform = new Transform();
     public Agent Occupier;


     //Which agent occupies the tile
     public void SetOccupier(Agent occupier)
     {
         Occupier = occupier;
     }


 }
}
