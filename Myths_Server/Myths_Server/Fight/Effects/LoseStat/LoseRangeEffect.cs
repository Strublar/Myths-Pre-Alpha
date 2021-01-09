using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseRangeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseRangeEffect() : base()
        {
        }

        public LoseRangeEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                foreach(int targetId in targets.GetTargets(context))
                {
                    Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+value+" mobility");

                    Entity target = fightHandler.Entities[targetId];
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.range, target.GetStat(Stat.range) - value));
                }
            }
        }
        #endregion
    }
}
