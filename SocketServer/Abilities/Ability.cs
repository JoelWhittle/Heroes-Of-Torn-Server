using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServer
{
   public class Ability
    {

        public string Name;
        public int MinRange;
        public int MaxRange;
        public _TargetType TargetType;
        public string Description;


        public string GetName()
        {
            return Name;

        }

        public virtual void Cast(Agent offensiveAgent, Agent targetAgent, int Damage, bool DidDie)
        {
            Console.WriteLine("Ability not implemented yet:" + Name);
        }

        public virtual void Cast(Agent offensiveAgent, Agent targetAgent, bool DidHit, bool DidDie, int Damage)
        {
            Console.WriteLine("Ability not implemented yet:" + Name);
        }

        public virtual void CastRequest(Agent offensiveAgent, Agent targetAgent)
        {
            Console.WriteLine("Ability not implemented yet:" + Name);

        }




        public enum _TargetType
        {
            Hex,
            Agent,
            Self
        }
    

    }
}
