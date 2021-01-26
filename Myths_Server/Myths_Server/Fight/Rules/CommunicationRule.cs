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
                Stat stat = (Stat)statEvent.StatId;
                if (stat == Stat.hp ||
                    stat == Stat.armor ||
                    stat == Stat.barrier ||
                    stat == Stat.attack ||
                    stat == Stat.mobility ||
                    stat == Stat.range ||
                    stat == Stat.attackType ||
                    stat == Stat.energy ||
                    stat == Stat.mastery1 ||
                    stat == Stat.mastery2 ||
                    stat == Stat.mastery3 ||
                    stat == Stat.calls ||
                    stat == Stat.gaugeAir ||
                    stat == Stat.gaugeArcane ||
                    stat == Stat.gaugeFire ||
                    stat == Stat.gaugeEarth ||
                    stat == Stat.gaugeWater ||
                    stat == Stat.gaugeLight ||
                    stat == Stat.gaugeDark ||
                    stat == Stat.isDead ||
                    stat == Stat.canUlt ||
                    stat == Stat.isEngaged ||
                    stat == Stat.canMove)
                {
                    fightHandler.Game.SendMessageToAllUsers(new EntityStatChangedMessage(statEvent.TargetId, statEvent.StatId,
                    fightHandler.Entities[statEvent.TargetId].GetStat(statEvent.StatId)));
                }

                if (statEvent.StatId == Stat.isCalled && statEvent.NewValue == 1)
                {
                    fightHandler.Game.SendMessageToAllUsers(new CallMessage(statEvent.TargetId,
                        fightHandler.Entities[statEvent.TargetId].GetStat(Stat.x),
                        fightHandler.Entities[statEvent.TargetId].GetStat(Stat.y)));
                }

                if (statEvent.StatId == Stat.isCalled && statEvent.NewValue == 0)
                {
                    fightHandler.Game.SendMessageToAllUsers(new UnCallMessage(statEvent.TargetId));
                }

                
            }

            if(newEvent is EntityMovedEvent moveEvent)
            {
                fightHandler.Game.SendMessageToAllUsers(new MoveMessage(moveEvent.TargetId, moveEvent.X,moveEvent.Y));
            }

            if(newEvent is BeginTurnEvent beginEvent)
            {
                if(fightHandler.Entities[beginEvent.SourceId] is Player)
                {
                    fightHandler.Game.SendMessageToAllUsers(new BeginTurnMessage(fightHandler.Entities[beginEvent.TargetId].Team));

                }
            }

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

        }
        #endregion
    }
}
