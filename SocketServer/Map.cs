using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SocketServer
{
 public   class Map
 {

     public Hex[,] Hexs;



     // Size of the map in terms of number of hex tiles
     // This is NOT representative of the amount of 
     // world space that we're going to take up.
     // (i.e. our tiles might be more or less than 1 Unity World Unit)
     public  int width = 10;
     public  int height = 10;
     float xOffset = 0.85f;
     float zOffset = 0.75f;
    // public TileType[] TileType;
     public List<UnitSpawnPoint> SpawnPoints = new List<UnitSpawnPoint>();
     public int NoOfPlayers = 2;
     public Texture2D MapData;
     public static Map Instance;


     public Room MyRoom;


     public virtual void StartMap()
     {
         
     }
  
     public void SetWidthAndHeight(int w, int h)
     {
         width = w;
         height = h;

         Hexs = new Hex[w,h];
     }

     public virtual void Init(int w, int h)
     {


         for (int x = 0; x < w; x++)
         {
             for (int y = 0; y < h; y++)
             {

                 float xPos = x * xOffset;

                 // Are we on an odd row?
                 if (y % 2 == 1)
                 {
                     xPos += xOffset / 2f;
                 }


                 Hex hex = new Hex();
                 hex.transform.position = new Vector3(xPos * 2, 0, y * zOffset * 2);
                 Hexs[x, y] = hex;
                 // Name the gameobject something sensible.
                 hex.name = "Hex_" + x + "_" + y;

                 // Make sure the hex is aware of its place on the map
                 hex.x = x;
                 hex.y = y;

                

                 //   hex_go.isStatic = true;


             }
         }
        // SnapTileTypesToMapData(w, h);
     //    gameObject.GetComponent<Pathfinding>().Init(w, h);

     }

     public void SnapTileTypesToMapData(int w, int h)
     {
         Color ColourType0 = new Color(1, 0, 0);
         Color ColourType1 = new Color(0, 1, 0);
         Color ColourType2 = new Color(0, 0, 1);


         for (int x = 0; x < w; x++)
         {
             for (int y = 0; y < h; y++)
             {

                 if (MapData.GetPixel(x, y) == ColourType0)
                 {
                  //   Hexs[x, y].ITileType = 0;
                 }
                 else

                     if (MapData.GetPixel(x, y) == ColourType1)
                     {
                   //      Hexs[x, y].ITileType = 1;
                     }
                     else

                         if (MapData.GetPixel(x, y) == ColourType2)
                         {
                         //    Hexs[x, y].ITileType = 2;
                         }
                         else
                         {
                         }
             }
         }
     }
   



     public virtual void InitSpawnPoints()
     {

     }

 }
}
