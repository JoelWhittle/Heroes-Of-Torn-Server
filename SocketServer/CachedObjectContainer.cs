using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
   public static class CachedObjectContainer
   {
       public static List<CachedRace> CachedRaces = new List<CachedRace>();
       public static  List<CachedCatalogueUnit> CachedCatalogueUnits = new List<CachedCatalogueUnit>();


       public static void CacheObjects()
       {
           CacheCatalogueUnits();
           CacheRaces();

       }

       public static void CacheCatalogueUnits()
       {
           Console.WriteLine("Caching Catalogue Units...");
           hotEntities he = new hotEntities();

           foreach (unitcatalogue uc in he.unitcatalogues)

           {
               CachedCatalogueUnit ccu = new CachedCatalogueUnit();

                 ccu.CatalogueID = uc.CatalogueID;
                 ccu.Name = uc.Name;
        ccu.UnitType = uc.UnitType;
       ccu.Faction = uc.Faction;
        ccu.Rarity = uc.Rarity;
        ccu.Collection  = uc.Collection;
       ccu.Race = uc.Race;

        ccu.DamageType = uc.DamageType;
        ccu.Attack = int.Parse(uc.Attack);
        ccu.Accuracy = uc.Accuracy;
        ccu.Dodge = uc.Dodge;
        ccu.HitPoints = uc.HitPoints;
               ccu.MovementSpeed = int.Parse(uc.MovementSpeed);
               ccu.Magic = uc.Magic;
               ccu.MagicResistance = uc.MagicResistance;
               ccu.FireResistance = uc.FireResistance;
                   ccu.SlashResistance = uc.SlashResistance;
                   ccu.PiercingResistance = uc.PiercingResistance;
                   ccu.BludgeoningResistance = uc.BludgeoningResistance;
                   ccu.MinAttackRange =int.Parse(uc.MinAttackRange);
               ccu.MaxAttackRange =  int.Parse(uc.MaxAttackRange);
               ccu.Abilities = uc.Abilities;
               ccu.Artist = uc.Artist;
               ccu.FlavourText = uc.FlavourText;

               CachedCatalogueUnits.Add(ccu);
               
           }

           Console.WriteLine("Catalogue Units cached: " + CachedCatalogueUnits.Count.ToString());
       }

       public static void CacheRaces()
       {
          Console.WriteLine("Caching Races..");
 
           hotEntities he = new hotEntities();

           foreach (race r in he.races)
           {
              CachedRace cr = new CachedRace();

               cr.Accuracy = r.Accuracy;
               cr.Attack = r.Attack;
               cr.BludgeoningResistance = r.BludgeoningResistance;
               cr.Dodge = r.Dodge;
               cr.FireResistance = r.FireResistance;
               cr.HitPoints = r.HitPoints;
               cr.Magic = r.Magic;
               cr.MagicResistance = r.MagicResistance;
               cr.MovementSpeed = r.MovementSpeed;
               cr.PiercingResistance = r.PiercingResistance;
               cr.SlashResistance = r.SlashResistance;
               cr.Description = r.Description;
               cr.Name = r.Name;

               CachedRaces.Add(cr);

           }

           Console.WriteLine("Races Cached: " + CachedRaces.Count.ToString());
       }

       public static CachedRace GetCachedRaceByName(string name)
       {
           CachedRace cr = null;

           foreach (CachedRace cachedRace in CachedRaces)
           {
               if (cachedRace.Name == name )
               {
                   cr = cachedRace;
               }
           }
           return cr;
       }

       public static CachedCatalogueUnit GetCachedCatUnitByID(int id)
       {
           CachedCatalogueUnit cu = null;

           foreach (CachedCatalogueUnit cachedUnit in CachedCatalogueUnits)
           {
               if (cachedUnit.CatalogueID == id)
               {
                   cu = cachedUnit;
               }
           }
           return cu;
       }


   }
}
