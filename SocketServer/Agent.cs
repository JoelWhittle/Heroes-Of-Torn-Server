using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace SocketServer
{
   
public class Agent 
{


    public int CATID;
    public string Owner;
    public string Name;
    public string Race;
    public string UnitType;
    public string Faction;
    public string Rarity;
    public string Collection;
    public int ID;
    public string DamageType;


    public int BaseAttack;
    public int BaseAccuracy;
    public int BaseDodge;
    public int BaseHitPoints;
    public int CurrentHitpoints;
    public int BaseMagic;
    public int BaseMagicResistance;
    public int BaseFireResistance;
    public int BaseSlashResistance;
    public int BasePiercingResistance;
    public int BaseBludgeoningResistance;

    public int BaseMovementSpeed;

    public int BaseMinAttackRange;
    public int BaseMaxAttackRange;

    public int Level;
    public List<Ability> Abilities = new List<Ability>();

    //Pathfinding

    public  Transform transform = new Transform();
    public Hex ParentHex;
    public bool IsMoving;

    
    //game
    public List<Buff> CurrentBuffs = new List<Buff>();

    //server

    public Room Room;

    public int GetAttackBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.AttackBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetAccuracyBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.AccuracyBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetDodgeBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.DodgeBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetHitPointsBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.HitPointsBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetMagicBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.MagicBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetMagicResistanceBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.MagicResistanceBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetSlashResistanceBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.SlashResistanceBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetPiercingResistanceBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.PiercingResistanceBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetBludgeoningResistanceBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.BludgeoningResistanceBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetFireResistanceBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.FireResistanceBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetMovementSpeedBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.MovementSpeedBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetMinAttackRangeBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.MinAttackRangeBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }
    public int GetMaxAttackRangeBuffModifier()
    {
        int n = 0;
        if (CurrentBuffs.Count > 0)
        {
            foreach (Buff buff in CurrentBuffs)
            {
                n = n + buff.MaxAttackRangeBuffModifier;
            }
            return n;
        }
        else
        {
            return 0;
        }
    }

    public bool IsConscious()
    {
        if (CurrentHitpoints > 0)
        {
          return true;  
        }
        else
        {
            return false;
        }
    }

    public void Init()
    {
  
        CurrentHitpoints = BaseHitPoints;


    }



    public int GetAttack()
    {
        return BaseAttack  + GetAttackBuffModifier();

    }
    public int GetAccuracy()
    {
        return BaseAccuracy + GetAccuracyBuffModifier();

    }
    public int GetDodge()
    {
        return BaseDodge + GetDodgeBuffModifier();

    }
    public int GetHitPoints()
    {
        return CurrentHitpoints;

    }
    public int GetMagic()
    {
        return BaseMagic + GetMagicBuffModifier();

    }
    public int GetMagicResistance()
    {
        return BaseMagicResistance + GetMagicResistanceBuffModifier();

    }

    public int GetFireResistance()
    {
        return BaseFireResistance + GetFireResistanceBuffModifier();
    }

    public int GetPiercingResistance()
    {
        return BasePiercingResistance + GetPiercingResistanceBuffModifier();
    }

    public int GetSlashResistance()
    {
        return BaseSlashResistance + GetSlashResistanceBuffModifier();
    }

    public int GetBludgeoningResistance()
    {
        return BaseBludgeoningResistance + GetBludgeoningResistanceBuffModifier();
    }


    public int GetMovementSpeed()
    {
        return BaseMovementSpeed + GetMovementSpeedBuffModifier();

    }

    public int GetMinAttackRange()
    {
        return BaseMinAttackRange + GetMinAttackRangeBuffModifier();
    }

    public int GetMaxAttackRange()
    {
        
        return BaseMaxAttackRange + GetMaxAttackRangeBuffModifier();
    }

    
   

    public void LerpLookAtTarget(Quaternion qStartPosition, Vector3 vTargetPosition)
    {
        Quaternion qDir = Quaternion.LookRotation(vTargetPosition - this.transform.position);
        Vector3 vDir = qDir.eulerAngles;

        this.transform.position = vDir;


    }

    public void LerpToPosition(Vector3 vStartPosition, Vector3 vTargetPosition)
    {
        Vector3 ourRot = this.transform.rotation;
        Quaternion ourQuat = Quaternion.Euler(ourRot);
        
      LerpLookAtTarget(ourQuat, vTargetPosition);

  this.transform.position = vTargetPosition;


    }

    public void LerpDownPath(List<Hex> Path)
    {
        List<Hex> tmp = new List<Hex>();

        foreach (Hex hex in Path)
        {
            if (hex != null)
            {
                tmp.Add(hex);
            }
        }
        Path = tmp;
        Debug.Log("reached lerp down path");
        IsMoving = true;

        Vector3[] vPath = new Vector3[Path.Count];

        for (int n = 0; n < Path.Count; n++)
        {
            if (Path[n]!=null)
            {


                vPath[n] = Path[n].transform.position;

            }
        }
     
        //Tell parent were moving 
        ParentHex.SetOccupier(null);
        //Tell ourselves were moving
        ParentHex = null;

        //Move
        int iCurIndex = 0;
        for (int x = 0; x < vPath.Length ; x++)
        {
            
         LerpToPosition(this.transform.position,vPath[iCurIndex]);
            iCurIndex++;

            //Set ParentHex
          ParentHex = Room.RoomMap.Hexs[Path[x].x, Path[x].y];
            //Tell ParentHex we occupy it
          ParentHex.SetOccupier(this);
            if (x != 0)
            {
                //Tell temp parent we are leaving
               Room.RoomMap.Hexs[Path[x - 1].x, Path[x - 1].y]
                    .SetOccupier(null);
            }
          
        }
        IsMoving = false;
     
    }

   

    //Bool to see if we are flanked
    public bool IsFlanked()
    {
        int n = 0;
        foreach (Agent enemy in GetEnemies())
        {
            if (enemy.GetValidAttackableTargets().Contains(this))
            {
                n++;
            }

        }
        if (n > 1)
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }

    //Cycles through all enemies and checks to make sure they are in range and also not obstructed
    public List<Agent> GetValidAttackableTargets()
    {
        List<Agent> returnList = new List<Agent>();

        foreach (Agent enemy in GetEnemies())
        {
            Debug.Log( enemy.ID.ToString() + ":" + GetManhattanDistance(enemy).ToString());
            if (GetManhattanDistance(enemy) >= GetMinAttackRange() && GetManhattanDistance(enemy) <= GetMaxAttackRange())
            {
              returnList.Add(enemy);  
            }
        }
        
        return returnList;
    }

    //Gets the manhattan distance between this agent and an enemy 
    public int GetManhattanDistance(Agent enemy)
    {
     //   return Pathfinding.Instance.GetDistanceAsCrowFlys(ParentHex, enemy.ParentHex);
        return 0;
    }

    //Returns a list of all enemies by cycling through all teams and adding all agents that arent in our team
    public List<Agent> GetEnemies()
    {
        List<Agent> returnList = new List<Agent>();

        foreach (Team team in Room.Teams)
        {
            if (team.Owner != Owner)
            {
                foreach (Agent agent in team.Agents)
                {
                    returnList.Add(agent);
                }  
            }
        }
        return returnList;

    }

    //check to see if this agent is behind another object, usually another agent, ie a backstab attack
    public bool IsBehindAgent(Agent target)
    {
   Vector3 v = new Vector3(target.transform.rotation.x, target.transform.rotation.y, target.transform.rotation.z);
        Quaternion q = Quaternion.Euler(v);

        GameObject go = new GameObject();
        go.transform.rotation = q;

    Vector3   vf = go.transform.forward;

      
       
        
        Vector3 directionToTarget = target.transform.position - this.transform.position;
        float angle = Vector3.Angle(vf, directionToTarget);
        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            return false;
        }
        else
        {


            if (Mathf.Abs(angle) < 90 || Mathf.Abs(angle) > 270)
            {
                return true;
            }
            else
            {
                return true;

            }
        }

    }
}

    }

