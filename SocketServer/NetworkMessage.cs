using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
   public class NetworkMessage
    {
        public string SendingPlayerName;
        public string ConcernedRoomName;
        public string Command;
        public string CommandArgs;


        public Room GetConcernedRoom()
        {
          return  AsynchronousSocketListener.FindRoomByName(ConcernedRoomName);
        }

        public void AssembleDetailsBasedFromInput(string input)
        {
            string[] args = input.Split(">".ToCharArray());
            string[] commandArgsSplit = input.Split(":".ToCharArray());

            SendingPlayerName = args[0];
            ConcernedRoomName = args[1];
            string[] commandSplit = args[2].Split(":".ToCharArray());
            Command = commandSplit[0];
            Console.WriteLine(Command);

            if (commandArgsSplit[1] != null)
            {
                CommandArgs = commandArgsSplit[1];
            }
        }
    }
}
