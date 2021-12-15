using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class TeleportEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public TeleportEffect() : base()
        {
        }

        public TeleportEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

                    
           /* Entity target = fightHandler.Entities[targetId];
            int targetX = effectContext.X;
            int targetY = effectContext.Y;
                    
            if (fightHandler.UnitOnTile(targetX,targetY) == null)
            {
                Console.WriteLine(fightHandler.Entities[targetId].Name + " is teleported to " + targetX + " " + targetY);
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.x, targetX));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                    Stat.y, targetY));

                fightHandler.FireEvent(new EntityMovedEvent(targetId, targetId, targetX, targetY));
            }*/

        }
        #endregion
    }
}
