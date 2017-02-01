using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Hosting;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SocketServer
{
  static   class NetworkCommandHandler
    {

      public static string RequestRoomLog(string roomName, int index)
      {
          string returnString = "";

          for (int i = index; i < AsynchronousSocketListener.FindRoomByName(roomName).Log.Count; i++)
          {
              returnString = returnString + AsynchronousSocketListener.FindRoomByName(roomName).Log[i] + "*";
          }

          return returnString;
      }

      public  static string ForceMessageThrough(string concernedRoom, string command, string commandArgs)
      {

          AsynchronousSocketListener.FindRoomByName(concernedRoom).Log.Add(command + ":" +commandArgs);
          string returnString = "success";

          Console.WriteLine("Function not yet implented, had to force through: " + command + ":" + commandArgs);
          return returnString;
      }


      public static string ServeOpenRoomOfRank(NetworkMessage message)
      {

          int rank = Int32.Parse(message.CommandArgs);
          List<Room> returnList = new List<Room>();

          foreach (Room room in AsynchronousSocketListener.Rooms)
          {
              if (room.WaitingForPlayers() && room.Rank == rank)
              {
                  returnList.Add(room);
              }
          }

          string returnString = "";

          if (returnList.Count() != 0)
          {
              returnString = returnList[0].RoomName;
          }

          return returnString;
      }

      public static string CreateRankedGame(NetworkMessage message)
      {
          string[] args = message.CommandArgs.Split(">".ToCharArray());


    Room room= AsynchronousSocketListener.MakeARoom(message);

          room.Rank = Int32.Parse(args[0]);
room.RoomMap = new SwampOfSorrows();
          room.RoomMap.StartMap();

          string returnString = "success";
     
          return returnString;
      }


      public static string PlayerJoinedRoom(NetworkMessage message)
      {
          string returnString = "success";

          string[] args = message.CommandArgs.Split(">".ToCharArray());
          string roomName =args[0];

          if (AsynchronousSocketListener.FindRoomByName(roomName).WaitingForPlayers())
          {
      
              AsynchronousSocketListener.FindRoomByName(roomName).AddPlayerToRoom(message);


          }
          else
          {
              returnString = "nope";
          }
          return returnString;

      }

      public  static string ServeSpawnUnitRequest(NetworkMessage message)
      {
           string returnString = "success";


          message.GetConcernedRoom().CreateAgent(message);

          return returnString;

      }

      //Recieves a Login request from the client. Queries the database
      //to 1: check the user exists , 2: check the password.
      //Then we tell the client if we were successfull or not
      public static string RequestLogin(NetworkMessage message)
      {
          //First split relevant details out of the message

          string[] args = message.CommandArgs.Split(">".ToCharArray());
          string requestedUsername = args[0];
          string requestedPassword = args[1];

          //initialise database entity

          hotEntities he = new hotEntities();

          try
          {
              user u = he.users.First(a => a.Username == requestedUsername);

              if (u != null)
              {
                  if (u.Password == requestedPassword)
                  {
                      return "Success";
                  }
                  else
                  {
                      return "Invalid Password";
                  }

              }
              else
              {
                  return "Invalid Username";
              }

          }
          catch (Exception e)
          {
              return "Invalid Username";

          }
       

      }


      //Recieves a Signup request from the client.
      //1st queries the database to make sure the username isnt taken
      //if the username isnt taken we insert a new user
      public static string RequestSignup(NetworkMessage message)
      {
          string[] args = message.CommandArgs.Split(">".ToCharArray());
          string requestedUsername = args[0];
          string requestedPassword = args[1];

          //initialise database entity

          hotEntities he = new hotEntities();

          try
          {
              user u = he.users.First(a => a.Username == requestedUsername);
              return "Sadly this Username has been taken";

          }
          catch (Exception e)
          {
         user u = new user();
              u.Username = requestedUsername;
              u.Password = requestedPassword;
              u.Rank = 0;
              u.Gold = 300;
              u.Dust = 0;
              u.Deck0 = "";
              u.Deck1 = "";
              u.Deck2 = "";
              u.Deck3 = "";
              u.Deck4 = "";
              u.Deck5 = "";
              u.Deck6 = "";
              u.Deck7 = "";
              u.Deck8 = "";


              he.users.Add(u);
            
                  he.SaveChanges();

                  return "Success>" + u.Gold.ToString()+ ">" + u.Dust.ToString();
            
          
       
          }
      }

      public static string RequestPlayerData(NetworkMessage message)
      {
          string username = message.CommandArgs;

          hotEntities he = new hotEntities();

          user u = he.users.First(a => a.Username == username);

          string returnString = "";

          returnString = returnString + u.Gold.ToString() + ",";
          returnString = returnString + u.Dust.ToString() + ",";
          returnString = returnString + u.Rank.ToString() + ",";
          returnString = returnString + u.Deck0.ToString() + ",";
          returnString = returnString + u.Deck1.ToString() + ",";
          returnString = returnString + u.Deck2.ToString() + ",";
          returnString = returnString + u.Deck3.ToString() + ",";
          returnString = returnString + u.Deck4.ToString() + ",";
          returnString = returnString + u.Deck5.ToString() + ",";
          returnString = returnString + u.Deck6.ToString() + ",";
          returnString = returnString + u.Deck7.ToString() + ",";
          returnString = returnString + u.Deck8.ToString();


          return returnString;


      }

      public static string RequestUnitCatalogue()
      {
          string returnString = "";

          hotEntities he = new hotEntities();
          
          List<unitcatalogue> catalogueUnits = new List<unitcatalogue>();

          catalogueUnits = he.unitcatalogues.ToList();

          foreach (unitcatalogue curUnit in catalogueUnits)
          {
              returnString = returnString + curUnit.CatalogueID + ">";
              returnString = returnString + curUnit.Name + ">";
              returnString = returnString + curUnit.UnitType + ">";
              returnString = returnString + curUnit.Faction + ">";
              returnString = returnString + curUnit.Rarity + ">";
              returnString = returnString + curUnit.Collection + ">";
              returnString = returnString + curUnit.Race + ">";
              returnString = returnString + curUnit.DamageType + ">";
              returnString = returnString + curUnit.Attack + ">";
              returnString = returnString + curUnit.Accuracy + ">";
              returnString = returnString + curUnit.Dodge.ToString() + ">";
              returnString = returnString + curUnit.HitPoints.ToString() + ">";
              returnString = returnString + curUnit.MovementSpeed + ">";
              returnString = returnString + curUnit.Magic + ">";
              returnString = returnString + curUnit.MagicResistance + ">";
              returnString = returnString + curUnit.FireResistance + ">";
              returnString = returnString + curUnit.SlashResistance + ">";
              returnString = returnString + curUnit.PiercingResistance + ">";
              returnString = returnString + curUnit.BludgeoningResistance + ">";
              returnString = returnString + curUnit.MinAttackRange + ">";
              returnString = returnString + curUnit.MaxAttackRange + ">";
              returnString = returnString + curUnit.Abilities + ">";
              returnString = returnString + curUnit.Artist + ">";
              returnString = returnString + curUnit.FlavourText + "@";


          }

          return returnString;
      }

      public static string RequestUsersUnits(NetworkMessage message)
      {
          string returnString = "";

          hotEntities he = new hotEntities();

         List<unitvault> vaultUnits = new List<unitvault>();

          vaultUnits = he.unitvaults.ToList();

          List<unitvault> myUnits = new List<unitvault>();

          foreach (unitvault vaultUnit in vaultUnits)
          {
              if (vaultUnit.Owner == message.CommandArgs)
              {
                  myUnits.Add(vaultUnit);
              }
      }

          foreach (unitvault myUnit in myUnits)
          {
              returnString = returnString + myUnit.ID + ",";
              returnString = returnString + myUnit.CatalogueID + ",0,";
              returnString = returnString + myUnit.Attack + ",";
              returnString = returnString + myUnit.Accuracy + ",";
              returnString = returnString + myUnit.Dodge + ",";

              returnString = returnString + myUnit.HitPoints + ",";
              returnString = returnString + myUnit.MovementSpeed + ",";
              returnString = returnString + myUnit.Magic + ",";
              returnString = returnString + myUnit.MagicResistance + ",";
              returnString = returnString + myUnit.FireResistance + ",";
              returnString = returnString + myUnit.PiercingResistance + ",";
              returnString = returnString + myUnit.SlashResistance + ",";
              returnString = returnString + myUnit.BludgeoningResistance + ",0 ,0 ,";
              returnString = returnString + myUnit.Experience + ",";
              returnString = returnString + myUnit.Level + ",";
              returnString = returnString + myUnit.Abilities + "#";



          }


          return returnString;
      }


      public static string RequestSaveDeck(NetworkMessage message)
      {
          string[] args = message.CommandArgs.Split(">".ToCharArray());

          int deckID = int.Parse(args[0]);
          try
          {
              hotEntities he = new hotEntities();

              user u = he.users.First(o => o.Username == message.SendingPlayerName);

              switch (deckID)
              {
                  case 0:
                      u.Deck0 = args[1];
                  break;
                  case 1:
                  u.Deck1 = args[1];
                  break;
                  case 2:
                  u.Deck2 = args[1];
                  break;
                  case 3:
                  u.Deck3 = args[1];
                  break;
                  case 4:
                  u.Deck4 = args[1];
                  break;
                  case 5:
                  u.Deck5 = args[1];
                  break;
                  case 6:
                  u.Deck6 = args[1];
                  break;
                  case 7:
                  u.Deck7 = args[1];
                  break;
                  case 8:
                  u.Deck8 = args[1];
                  break;

              }
              he.SaveChanges();


              return "Success";

          }
          catch (Exception)
          {

              return "Failure";
          }
    
      }

      public static string RequestNewUnitVaultUnit(NetworkMessage message)
      {
          string returnString = "";

          hotEntities he = new hotEntities();

          int i = int.Parse(message.CommandArgs);
          unitcatalogue catalogueUnit = he.unitcatalogues.First(o => o.CatalogueID == i);

          unitvault vaultUnit = new unitvault();

          vaultUnit.Abilities = catalogueUnit.Abilities;
          vaultUnit.CatalogueID = catalogueUnit.CatalogueID.ToString();
          vaultUnit.Accuracy = 0.ToString();
          vaultUnit.Attack = 0.ToString();
          vaultUnit.BludgeoningResistance = 0.ToString();
          vaultUnit.Dodge = 0.ToString();
          vaultUnit.Experience = 0;
          vaultUnit.FireResistance = 0.ToString();
          vaultUnit.HitPoints = 0.ToString();
          vaultUnit.Level = 0;
          vaultUnit.Magic = 0.ToString();
          vaultUnit.MagicResistance = 0.ToString();
          vaultUnit.MaxAttRange = 0.ToString();
          vaultUnit.MinAttRange = 0.ToString();
          vaultUnit.MovementSpeed = 0.ToString();
          vaultUnit.Owner = message.SendingPlayerName;
          vaultUnit.PiercingResistance = 0.ToString();
          vaultUnit.SlashResistance = 0.ToString();

          try
          {
              he.unitvaults.Add(vaultUnit);
              he.SaveChanges();
          }
          catch (Exception e)
          {
              
              Console.WriteLine(e);
          }

          returnString = vaultUnit.ID.ToString();




          return returnString;
      }

      public static string RequestMapMetaData()
      {
          List<mapmetadata> mapMetas = new List<mapmetadata>();

          hotEntities he = new hotEntities();

          mapMetas = he.mapmetadatas.ToList();

          string returnString = "";
          foreach (mapmetadata mapMeta in mapMetas)
          {
              returnString = returnString + mapMeta.Name + ",";
              returnString = returnString + mapMeta.Expansion + ",";
              returnString = returnString + mapMeta.PlayerCount.ToString() + ",";
              returnString = returnString + mapMeta.MapType + "#";



          }

          return returnString;

      }

      public static string RequestRaceData()
      {
          string returnString = "";

          hotEntities he = new hotEntities();
          List<race> races = new List<race>();
          races = he.races.ToList();

          foreach (race r in races)
          {
              returnString = returnString + r.Name + ">";
              returnString = returnString + r.Description + ">";
              returnString = returnString + r.Attack + ">";
              returnString = returnString + r.Dodge + ">";
              returnString = returnString + r.Accuracy + ">";
              returnString = returnString + r.HitPoints + ">";

              returnString = returnString + r.Magic + ">";
              returnString = returnString + r.MovementSpeed + ">";
              returnString = returnString + r.FireResistance + ">";
              returnString = returnString + r.MagicResistance + ">";
              returnString = returnString + r.SlashResistance + ">";
              returnString = returnString + r.PiercingResistance + ">";
              returnString = returnString + r.BludgeoningResistance + "#";


          }

     

          return returnString;
      }
    }



}
