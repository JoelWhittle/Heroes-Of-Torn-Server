using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Data.Sql;
using System.Linq;
using System.Data.SqlClient;

// State object for reading client data asynchronously
using System.Windows.Forms.VisualStyles;
using SocketServer;
using UnityEngine;

public class StateObject
{
    // Client  socket.

    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1024;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();
}




public static  class AsynchronousSocketListener
{
    // Thread signal.
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public static List<Room> Rooms = new List<Room>();





    public static void StartListening()
    {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8888);

        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {

        // Signal the main thread to continue.
        allDone.Set();

        // Get the socket that handles the client request.
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        // Retrieve the state object and the handler socket
        // from the asynchronous state object.
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client socket. 
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {
            // There  might be more data, so store the data received so far.
            state.sb.Append(Encoding.ASCII.GetString(
                state.buffer, 0, bytesRead));

            // Check for end-of-file tag. If it is not there, read 
            // more data.
            content = state.sb.ToString();
            if (content.IndexOf("<EOF>") > -1)
            {
                // All the data has been read from the 
                // client. Display it on the console.
                Console.WriteLine("Read {0} bytes from socket. \n Input : {1}",
                    content.Length, content);
          

       //Send the  incoming message of to be parsed
                //First isolate the incoming message

                string[] arg = content.Split("<".ToCharArray());

                
                NetworkMessage incomingMessage = new NetworkMessage();
                
                incomingMessage.AssembleDetailsBasedFromInput(arg[0]);

                //check to see if concerned room exists, if not create it
                if ( FindRoomByName(incomingMessage.ConcernedRoomName) == null)
                {
                    MakeARoom(incomingMessage);
                }

               string result =  IncomingMessageParser.ParseIncomingMessage(incomingMessage);


        
                Send(handler, result);

               
            }
            else
            {
                // Not all data received. Get more.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }
    }


    private static bool DoesRoomExist(string roomName)
    {
        bool returnBool = false;
        foreach (Room room in Rooms)
        {
            if (room.RoomName == roomName)
            {
                returnBool = true;
            }

        }
        return returnBool;
    }

    public static Room MakeARoom(NetworkMessage message)
    {

        string[] args = message.CommandArgs.Split(">".ToCharArray());
        Room room = new Room();
        room.RoomName = args[1];
     
        Rooms.Add(room);

     

        room.AddPlayerToRoom(message);

        Console.WriteLine("Server:" + message.SendingPlayerName +  "Made a new Room:" + args[1]);
        return room;

    }

    private static void DeleteARoom(string roomName)
    {
      
        Rooms.Remove(FindRoomByName(roomName));
    }

    public static Room FindRoomByName(string roomName)
    {
        Room returnRoom = null;

        foreach (Room room in Rooms)
        {
            if (room.RoomName == roomName)
            {
                returnRoom = room;
            }
        }

       
        return returnRoom;
    }


    private static void Send(Socket handler, String data)
    {
        data = data + "<EOF>";
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);

        Console.WriteLine("Output:" + data);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public static int Main(String[] args)
    {
        InitLobbyRoom();
        CachedObjectContainer.CacheObjects();



//        Test();
        StartListening();

        return 0;
    }


    private static void Test()
    {
        hotEntities he = new hotEntities();

    

    }

    public  static void InitLobbyRoom()
    {
        Room LobbyRoom = new Room();

        LobbyRoom.RoomName = "Test";
        LobbyRoom.Rank = 99;
        LobbyRoom.MaxPlayerCount = 99;
        Rooms.Add(LobbyRoom);
        Console.WriteLine("Initiated Lobby Room");


    }

    public static  Room GetLobbyRoom()
    {
        return FindRoomByName("Test");
    }
}