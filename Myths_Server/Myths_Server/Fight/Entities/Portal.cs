using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class Portal : SpecialTile
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor


        public Portal(FightHandler fightHandler, int portalId ) : 
            base(fightHandler, GetPortalDefinition(portalId), -1 )
        {
            /*fightHandler.Game.SendMessageToAllUsers(
                new InitPortalMessage(this.Id, this.GetStat(Stat.x), this.GetStat(Stat.y)));*/
        }
        #endregion

        #region Methods
        public static EntityDefinition GetPortalDefinition(int portalId)
        {
            EntityDefinition portalDefinition = new EntityDefinition();
            portalDefinition.Name = "Portail";
            Dictionary<Stat, int> stats = new Dictionary<Stat, int>();
            //position
            stats.Add(Stat.x, 3);
            stats.Add(Stat.y, 4*portalId);
            portalDefinition.BaseStats = stats;
            List<ListeningEffectDefinition> listeningEffects = new List<ListeningEffectDefinition>();
            portalDefinition.BaseListeningEffects = listeningEffects;
            return portalDefinition;
        }
        #endregion
    }
}
