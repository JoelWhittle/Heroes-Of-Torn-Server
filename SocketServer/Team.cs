using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServer
{
   public class Team
   {
       public string Owner;
       public  List<Agent> Agents = new List<Agent>();
   }
}
