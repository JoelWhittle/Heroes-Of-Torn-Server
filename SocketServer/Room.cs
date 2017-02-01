using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
 public   class Room
    {
        
       public string RoomName = "";
      public int Rank = 0 ;
      public int  MaxPlayerCount =  2;

     public Player MasterClient;

     public List<Player> Players = new List<Player>();

     public List<string> Log = new List<string>();

     public Map RoomMap;

     public List<Team> Teams = new List<Team>(); 


     public bool WaitingForPlayers()
     {
         if (Players.Count >= MaxPlayerCount)
         {
             return false;
         }
         else
         {
             return true;
         }
     }
     public void AddPlayerToRoom(NetworkMessage message)

     {
        
         string[] args = message.CommandArgs.Split(">".ToCharArray());
         int i = int.Parse(args[4]);

         Player player = new Player();
         player.Name = message.SendingPlayerName;
         player.SelectedDeckIndex = i;
         player.myRoom = this;

         if (WaitingForPlayers())
         {
             Log.Add("RegisterPlayer:" + player.Name);
             Players.Add(player);

             Team team = new Team();
             team.Owner = player.Name;
             Teams.Add(team);

             
             Console.WriteLine("Server:" + player.Name + " just joined room " + message.CommandArgs);

             if (Players.Count == MaxPlayerCount)
             {

                 foreach (Player p in Players)
                 {
                     p.LoadAgents();
                 }
                 Log.Add("LoadMap:0");
             }
                 
                 
         }
         else
         {
             //not waiting for player, handle that somehow
         }

     }

     public void CreateAgent(NetworkMessage message)
     {
         
     }

     public int FindAgentsTeamID(Agent agent)
     {
         int id = 0;
         bool looking = true;
         foreach (Team team in Teams)
         {

             foreach (Agent possAgent in team.Agents)
             {
                 if (possAgent == agent)
                 {
                     looking = false;
                 }
             
                     }

             if (looking)
             {
                 id++;
             }
                 }
             
         


         return id;
     }
    }
}
