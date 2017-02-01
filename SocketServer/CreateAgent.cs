using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
  public static  class CreateAgent
    {

      public static Agent CreateAgentFromVaultUnit(int id)
      {
         Agent agent = new Agent();

          hotEntities he = new hotEntities();


          unitvault uv = he.unitvaults.First(a => a.ID == id);

          CachedCatalogueUnit cachedUnit = CachedObjectContainer.GetCachedCatUnitByID(int.Parse(uv.CatalogueID));

          CachedRace cachedRace = CachedObjectContainer.GetCachedRaceByName(cachedUnit.Race);


          agent.ID = uv.ID;
          agent.CATID = cachedUnit.CatalogueID;
          agent.Name = cachedUnit.Name;
          agent.UnitType = cachedUnit.UnitType;
          agent.Faction = cachedUnit.Faction;
          agent.Rarity = cachedUnit.Rarity;
          agent.Collection = cachedUnit.Collection;
          agent.Race = cachedUnit.Race;
          agent.DamageType = cachedUnit.DamageType;
          agent.BaseAttack = cachedUnit.Attack  + int.Parse(uv.Attack) + cachedRace.Attack;
          agent.BaseAccuracy = cachedUnit.Accuracy + int.Parse(uv.Accuracy) + cachedRace.Accuracy;
          agent.BaseDodge = cachedUnit.Dodge + int.Parse(uv.Dodge) + cachedRace.Dodge;
          agent.BaseHitPoints = cachedUnit.HitPoints + int.Parse(uv.HitPoints) + cachedRace.HitPoints;
          agent.BaseMagic = cachedUnit.Magic + int.Parse(uv.Magic) + cachedRace.Magic;
          agent.BaseMagicResistance = cachedUnit.MagicResistance + int.Parse(uv.MagicResistance) + cachedRace.MagicResistance;
          agent.BaseFireResistance = cachedUnit.FireResistance + int.Parse(uv.FireResistance) + cachedRace.FireResistance;
          agent.BaseSlashResistance = cachedUnit.SlashResistance + int.Parse(uv.SlashResistance) + cachedRace.SlashResistance;
          agent.BasePiercingResistance = cachedUnit.PiercingResistance + int.Parse(uv.PiercingResistance) + cachedRace.PiercingResistance;
          agent.BaseBludgeoningResistance = cachedUnit.BludgeoningResistance + int.Parse(uv.BludgeoningResistance) + cachedRace.BludgeoningResistance;
          agent.BaseMovementSpeed = cachedUnit.MovementSpeed + int.Parse(uv.MovementSpeed) + cachedRace.MovementSpeed;
          agent.BaseMinAttackRange = cachedUnit.MinAttackRange + int.Parse(uv.MinAttRange) ;
          agent.BaseMaxAttackRange = cachedUnit.MaxAttackRange + int.Parse(uv.MaxAttRange);
          agent.BaseMinAttackRange = cachedUnit.MinAttackRange + int.Parse(uv.MinAttRange);
          agent.BaseMinAttackRange = cachedUnit.MinAttackRange + int.Parse(uv.MinAttRange);
          agent.Level = uv.Level;
          agent.Owner = uv.Owner;
          //players team id
          //hex to spawn on
         // agent.Abilities = uv.Abilities;
          string returnString = "";

     
          //               unit.iLevel.ToString().ToString() + ">" +
          //               NetworkManager.Instance.cPlayer.sName + ">" +
          //               GameManager.Instance.GetPlayersTeamID().ToString() + ">" +
          //               hex.gameObject.name + ">" +
          //               unit.Abilities;


                           


          return agent;
      }
    }
}
