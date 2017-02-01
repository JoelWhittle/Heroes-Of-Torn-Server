using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class IncomingMessageParser
    {

        public static  string ParseIncomingMessage(NetworkMessage message)
        {
            string returnString = null;

            switch (message.Command)
            {
                case "RequestRoomLog":
               returnString =  NetworkCommandHandler.RequestRoomLog(message.ConcernedRoomName ,Int32.Parse(message.CommandArgs));

                    break;

                case "HandleCreateRankedGame":

                    returnString = NetworkCommandHandler.CreateRankedGame(message);

                    break;
                case "RequestRoomOfRank":

                    returnString = NetworkCommandHandler.ServeOpenRoomOfRank(message);
                    break;
                    
                case "PlayerJoinedRoom":
                    returnString = NetworkCommandHandler.PlayerJoinedRoom(message);


                    break;

                case "RequestLogin":

                    returnString = NetworkCommandHandler.RequestLogin(message);

                    break;

                case "RequestSignup":

                    returnString = NetworkCommandHandler.RequestSignup(message);

                    break;

                case "RequestPlayerData":

                    returnString = NetworkCommandHandler.RequestPlayerData(message);
                    break;

                case "RequestUnitCatalogue":
                    returnString = NetworkCommandHandler.RequestUnitCatalogue();

                    break;

                case "RequestUsersUnits":

                    returnString = NetworkCommandHandler.RequestUsersUnits(message);

                    break;

                case "RequestBeginnersCollection":

                  returnString =  GameConfig.GetBeginnersCollection();
                    break;

                case "SaveDeck":

                    returnString = NetworkCommandHandler.RequestSaveDeck(message);

                    break;

                case "RequestNewUnitVaultUnit":

                    returnString = NetworkCommandHandler.RequestNewUnitVaultUnit(message);
                    break;

                case "RequestMapMetaData":

                    returnString = NetworkCommandHandler.RequestMapMetaData();

                    break;

                case "RequestRaceData":
                    returnString = NetworkCommandHandler.RequestRaceData();
                    break;
                   
                    
                //case "SpawnUnit":

                //    returnString = NetworkCommandHandler.ServeSpawnUnitRequest(message);
                //    break;

                 default:
//Release code \/
                 //   returnString = "Function not implemented yet: " + message.Command;

                    //TEMP test code just to push through the messages

                    returnString = NetworkCommandHandler.ForceMessageThrough(message.ConcernedRoomName, message.Command,
                        message.CommandArgs);


                    break;

            }
            return returnString;
        }

     
    }


}
