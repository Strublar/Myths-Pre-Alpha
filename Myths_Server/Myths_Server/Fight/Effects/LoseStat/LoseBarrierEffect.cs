using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseBarrierEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseBarrierEffect() : base()
        {
        }

        public LoseBarrierEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+value+" barrier");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.barrier, target.GetStat(Stat.barrier) - value));

        }
        #endregion
    }
}
