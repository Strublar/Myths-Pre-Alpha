using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Myths_Server
{
    class CommunicationRule : Rule
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public override void OnEvent(Event newEvent, FightHandler fightHandler)
        {
            
            
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {
            if(newEvent is EntityStatChangedEvent statEvent)
            {
                Stat stat = statEvent.StatId;
                if (stat == Stat.hp ||
                    stat == Stat.armor ||
                    stat == Stat.mobility ||
                    stat == Stat.energy ||
                    stat == Stat.calls ||
                    stat == Stat.mana ||
                    stat == Stat.isDead ||
                    stat == Stat.canUlt1 ||
                    stat == Stat.canUlt2 ||
                    stat == Stat.canUlt3 ||
                    stat == Stat.isEngaged ||
                    stat == Stat.canMove ||
                    stat == Stat.masteryAir ||
                    stat == Stat.masteryDark ||
                    stat == Stat.masteryEarth ||
                    stat == Stat.masteryFire ||
                    stat == Stat.masteryLight ||
                    stat == Stat.masteryWater )
                {
                    fightHandler.Game.SendMessageToAllUsers(new EntityStatChangedMessage(statEvent.TargetId, statEvent.StatId,
                    fightHandler.Entities[statEvent.TargetId].GetStat(statEvent.StatId)));
                }

                if (statEvent.StatId == Stat.isCalled && statEvent.NewValue == 1)
                {
                    fightHandler.Game.SendMessageToAllUsers(new EntityCalledMessage(statEvent.TargetId,
                        fightHandler.Entities[statEvent.TargetId].GetStat(Stat.x),
                        fightHandler.Entities[statEvent.TargetId].GetStat(Stat.y)));
                }
                
                if (statEvent.StatId == Stat.isCalled && statEvent.NewValue == 0)
                {
                    fightHandler.Game.SendMessageToAllUsers(new EntityUnCalledMessage(statEvent.TargetId));
                }

                
            }
            
            if(newEvent is EntityMovedEvent moveEvent)
            {
                fightHandler.Game.SendMessageToAllUsers(new EntityMovedMessage(moveEvent.TargetId, moveEvent.X,moveEvent.Y));
            }

            if(newEvent is BeginTurnEvent beginEvent)
            {
                if(fightHandler.Entities[beginEvent.SourceId] is Player)
                {
                    fightHandler.Game.SendMessageToAllUsers(new BeginTurnMessage(fightHandler.Entities[beginEvent.TargetId].Team));

                }
            }
            /*
            if (newEvent is EntityAttackEvent attackEvent)
            {
                fightHandler.Game.SendMessageToAllUsers(new AttackMessage(attackEvent.SourceId));
            }

            if(newEvent is EndGameEvent endEvent)
            {
                int winnerId = 0;
                foreach (Entity entity in fightHandler.Entities.Values)
                {
                    if(entity is Player && entity.Id != newEvent.SourceId)
                    {
                        winnerId = entity.Id;
                    }
                }
                fightHandler.Game.SendMessageToAllUsers(new EndGameMessage(winnerId));
            }
            

            if (newEvent is SpellCastEvent castEvent)
            {
                fightHandler.Game.SendMessageToAllUsers(new EntityCastSpellMessage(castEvent.SourceId,castEvent.SpellId,
                    castEvent.X, castEvent.Y));
            }
            if(newEvent is CapturePortalEvent captureEvent)
            {
                fightHandler.Game.SendMessageToAllUsers(new CapturePortalMessage(captureEvent.TargetId,
                    fightHandler.Entities[captureEvent.TargetId].Team));
            }
            */
        }
        #endregion
    }
}
