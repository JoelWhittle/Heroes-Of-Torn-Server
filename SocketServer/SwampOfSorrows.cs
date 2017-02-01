using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class SwampOfSorrows : Map
    {

        public int Width = 10;
        public int Height = 15;
        // Use this for initialization

      

      public override  void  StartMap()
        {
            Init(Width, Height);
        }
        public override void Init(int w, int h)
        {
  
            base.SetWidthAndHeight(Width, Height);
            base.Init(Width, Height);

            InitSpawnPoints();
        }

        public override void InitSpawnPoints()
        {
            NoOfPlayers = 2;
            for (int x = 0; x < NoOfPlayers; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    UnitSpawnPoint sp = new UnitSpawnPoint();
                    SpawnPoints.Add(sp);
                    sp.iPlayerTeamId = x;

                    if (x == 0)
                    {
                        sp.ParentHex = Hexs[y, 0];
                    }
                    if (x == 1)
                    {
                        sp.ParentHex = Hexs[y, 6];
                    }

                    SpawnPoints.Add(sp);

                }
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
