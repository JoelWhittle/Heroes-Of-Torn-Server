using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SocketServer.Abilities
{
    class AttackAbility : Ability
    {


        public override void Cast(Agent offensiveAgent, Agent targetAgent, bool DidHit, bool DidDie, int Damage)
        {
        Attack(offensiveAgent, targetAgent, DidHit, DidDie, Damage);
        }

        public override void CastRequest(Agent offensiveAgent, Agent targetAgent)
        {
            CalculateAttackResult(offensiveAgent, targetAgent);
        }


        //Calculate the results of an attack and asks ExchangeGameDataToSendIt
        public string CalculateAttackResult(Agent curAgent, Agent enemyAgent)
        {

            string result = "";
            int dammage = 0;

            //First get the ID of the attacker and the victim
            result = result + curAgent.ID.ToString() + ">" + enemyAgent.ID.ToString();

            //Then calculate wether the attack hit or not
            if (UnityEngine.Random.Range(0, 100 + enemyAgent.GetDodge()) < curAgent.GetAccuracy() || curAgent.IsBehindAgent(enemyAgent))
            {
                //we hit
                result = result + ">hit";

                int percent = 0;
                switch (curAgent.DamageType)
                {
                    case "Slash":
                        percent = Convert.ToInt32((curAgent.GetAttack() / 100) * enemyAgent.GetSlashResistance());
                        break;
                    case "Piercing":
                        percent = Convert.ToInt32((curAgent.GetAttack() / 100) * enemyAgent.GetPiercingResistance());
                        break;
                    case "Bludgeoning":
                        percent = Convert.ToInt32((curAgent.GetAttack() / 100) * enemyAgent.GetBludgeoningResistance());
                        break;

                }
                int baseDamage = curAgent.GetAttack() - percent;
                dammage = baseDamage;

                //check if enemy is flanked
                if (enemyAgent.IsFlanked())
                {
                    dammage = dammage + (baseDamage / 2);
                }

                //check for back stab
                if (curAgent.IsBehindAgent(enemyAgent))
                {
                    dammage = dammage + (baseDamage / 2);

                }

            }
            else
            {
                // we missed
                result = result + ">miss";
            }
            //Add dammage to result and check for death, add that to result to
            result = result + ">" + dammage.ToString();
            if (enemyAgent.GetHitPoints() <= dammage)
            {
                //death
                result = result + ">true";
            }
            else
            {

                result = result + ">false";

            }

            return result;
            //    StartCoroutine(ExchangeGameData.Instance.SendAttackRequest(result));
        }

        //Attack an enemy and deal damage based on results from the RoomLog
        public void Attack(Agent curAgent, Agent enemyAgent, bool didWeHit, bool didTheyDie, int damage)
        {
            //TODO:Update the room log

            curAgent.LerpLookAtTarget(Quaternion.Euler(curAgent.transform.rotation), enemyAgent.transform.position);


            if (didWeHit == true)
            {
                enemyAgent.CurrentHitpoints = enemyAgent.CurrentHitpoints - damage;
                if (didTheyDie == true)
                {
                    //They died to play their death animation and remove them from their team list

                    curAgent.Room.Teams[curAgent.Room.FindAgentsTeamID(enemyAgent)].Agents.Remove(enemyAgent);
                }
            }
            else
            {
                // they blocked.. dont think i need to remember this for now
            }



        }


    }
}
