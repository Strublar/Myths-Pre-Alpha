using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainMasteryEffect() : base()
        {
        }

        public GainMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains mastery");

            Entity target = fightHandler.Entities[targetId];
            if(target.GetStat(Stat.mastery1)==0)
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery1, value));
                fightHandler.FireEvent(new GainMasteryEvent(targetId, targetId, value));
            }
            else if(target.GetStat(Stat.mastery2) == 0)
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2, value));
                fightHandler.FireEvent(new GainMasteryEvent(targetId, targetId, value));
            }
            else if (target.GetStat(Stat.mastery3) == 0)
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, value));
                fightHandler.FireEvent(new GainMasteryEvent(targetId, targetId, value));
            }
            else
            {
                Console.WriteLine("Mastery overdrawn");
            }

        }
        #endregion
    }
}
