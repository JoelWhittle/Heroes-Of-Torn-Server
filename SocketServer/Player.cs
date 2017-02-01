using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    //Object for holding Player Info
    public class Player
    {
      
      
            public string Name;
        public int SelectedDeckIndex;
        public Room myRoom;

        public void LoadAgents()
        {
            List<UnitSpawnPoint> spawnPoints = new List<UnitSpawnPoint>();

            foreach (UnitSpawnPoint sp in myRoom.RoomMap.SpawnPoints)
            {
                if (sp.iPlayerTeamId == myRoom.Players.Count - 1)
                {
                    spawnPoints.Add(sp);
                }
            }

            hotEntities he = new hotEntities();

            List<int> idsToLoad = new List<int>();

            user user = he.users.First(o => o.Username == Name);
            string deckString = "";

            switch (SelectedDeckIndex)
            {
                case (0):
                    deckString = user.Deck0;
                    break;
                case (1):
                    deckString = user.Deck1;
                    break;
                case (2):
                    deckString = user.Deck2;
                    break;
                case (3):
                    deckString = user.Deck3;
                    break;
                case (4):
                    deckString = user.Deck4;
                    break;
                case (5):
                    deckString = user.Deck5;
                    break;
                case (6):
                    deckString = user.Deck6;
                    break;
                case (7):
                    deckString = user.Deck7;
                    break;
                case (8):
                    deckString = user.Deck8;
                    break;
            }
            string[] deckArgs = deckString.Split("(".ToCharArray());

        string[]   idArgs = deckArgs[3].Split("*".ToCharArray());
           
            foreach (string s in idArgs)
            {
                if (s != "")
                {
                    idsToLoad.Add(int.Parse(s));
                }
            }
        }

    }

    
}
