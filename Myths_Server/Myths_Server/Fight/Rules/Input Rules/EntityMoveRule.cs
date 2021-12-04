using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityMoveRule : Rule
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
            
            if(newEvent is EntityMoveEvent moveEvent)
            {
                Console.WriteLine("Move rule activated ");
                if (fightHandler.UnitOnTile(moveEvent.X, moveEvent.Y) == null && 
                    fightHandler.Entities[moveEvent.TargetId].GetStat(Stat.canMove) == 1)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(moveEvent.TargetId, moveEvent.TargetId,
                        Stat.x, moveEvent.X));
                    fightHandler.FireEvent(new EntityStatChangedEvent(moveEvent.TargetId, moveEvent.TargetId,
                        Stat.y, moveEvent.Y));
                    fightHandler.FireEvent(new EntityStatChangedEvent(moveEvent.TargetId, moveEvent.TargetId,
                        Stat.canMove, 0));
                    fightHandler.FireEvent(new EntityMovedEvent(moveEvent.TargetId, moveEvent.TargetId, moveEvent.X, moveEvent.Y));

                }
            }
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {

        }
        #endregion
    }
}
