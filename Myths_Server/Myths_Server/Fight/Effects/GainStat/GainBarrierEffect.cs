using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainBarrierEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainBarrierEffect() : base()
        {
        }

        public GainBarrierEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+value+" barrier");

            Entity target = fightHandler.Entities[targetId];
            if (target.GetStat(Stat.barrier) < target.Stats[Stat.barrier])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier,
                    (int)MathF.Min(target.Stats[Stat.barrier], target.GetStat(Stat.barrier) + value)));
            }

        }
        #endregion
    }
}
