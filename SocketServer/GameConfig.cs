using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
   public static class GameConfig
    {

       public static string GetBeginnersCollection()
       {
           string s =
"0,1,2,3,4,5,1,2,3,4,5,6,7,8,9,7,8,9#0,2,2,3,3,4#0,2,3,4,5,1#0,2,3,4,4,5#0,2,3,3,4,5#0,2,2,5,5,1#0,1,2,3,3,4#0,2,3,4,5,1#0,2,3,4,4,5#6,7,7,8,8,9";

           return s;
       }
    }

}
