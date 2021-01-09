using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseMobilityEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseMobilityEffect() : base()
        {
        }

        public LoseMobilityEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                        Stat.mobility, target.GetStat(Stat.mobility) - value));
                }
            }
        }
        #endregion
    }
}
