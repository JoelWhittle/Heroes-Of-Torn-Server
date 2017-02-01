using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServer
{
     public class Buff
    {

        public int Duration;
        public string BuffedBy;
        public int AttackBuffModifier;
        public int AccuracyBuffModifier;
        public int DodgeBuffModifier;
        public int HitPointsBuffModifier;
        public int MagicBuffModifier;
        public int MagicResistanceBuffModifier;
        public int FireResistanceBuffModifier;
        public int SlashResistanceBuffModifier;
        public int PiercingResistanceBuffModifier;
        public int BludgeoningResistanceBuffModifier;

        public int MovementSpeedBuffModifier;

        public int MinAttackRangeBuffModifier;
        public int MaxAttackRangeBuffModifier;

    

        public void Tick()
        {
            Duration--;
            if (Duration == 0)
            {
              //remove

            }
        }
    }
}
