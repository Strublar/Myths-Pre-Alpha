using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class SwapEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public SwapEffect() : base()
        {
        }

        public SwapEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Entity origin = fightHandler.Entities[context.SourceId];
            Entity target = fightHandler.Entities[targetId];
            int originX = origin.GetStat(Stat.x);
            int originY = origin.GetStat(Stat.y);
            int targetX = target.GetStat(Stat.x);
            int targetY = target.GetStat(Stat.y);

            Console.WriteLine(origin.Name + " swap position with " + target.Name);

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
            Stat.x, originX));
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
            Stat.y, originY));

            fightHandler.FireEvent(new EntityStatChangedEvent(origin.Id, origin.Id,
            Stat.x, targetX));
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
            Stat.y, targetY));

            fightHandler.FireEvent(new EntityMovedEvent(targetId, targetId, originX, originY));
            fightHandler.FireEvent(new EntityMovedEvent(origin.Id, origin.Id, targetX, targetY));


        }
        #endregion
    }
}
