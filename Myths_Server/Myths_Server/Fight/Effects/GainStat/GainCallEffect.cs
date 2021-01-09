using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainCallEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainCallEffect() : base()
        {
        }

        public GainCallEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+value+" calls");

                    Entity target = fightHandler.Entities[targetId];
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.calls, target.GetStat(Stat.calls) + value));
                }
            }
        }
        #endregion
    }
}
