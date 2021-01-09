using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealTrueDamagePerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DealTrueDamagePerMasteryEffect() : base()
        {
        }

        public DealTrueDamagePerMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                int computedValue = 0;
                Entity source = fightHandler.Entities[context.SourceId];
                if (source.GetStat(Stat.mastery1) != 0)
                {
                    computedValue += value;
                }
                if (source.GetStat(Stat.mastery2) != 0)
                {
                    computedValue += value;
                }
                if (source.GetStat(Stat.mastery2) != 0)
                {
                    computedValue += value;
                }

                foreach (int targetId in targets.GetTargets(context))
                {
                    Console.WriteLine("Dealing "+ computedValue + " true damage to " + fightHandler.Entities[targetId].Definition.Name);
                    //check broken guard
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                        fightHandler.Entities[targetId].GetStat(Stat.hp) - computedValue));
                    
                    
                }
            }
        }
        #endregion
    }
}
