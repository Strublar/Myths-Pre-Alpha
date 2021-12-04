using Myths_Library;
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

        public GainMasteryEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains mastery");
            Entity target = fightHandler.Entities[targetId];
            int count = values.Count > 1 ? values[1] : 1;
            for(int i =0; i< count;i++)
            {
                if (target.GetStat(Stat.mastery1) == 0)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery1, values[0]));
                    fightHandler.FireEvent(new GainMasteryEvent(targetId, targetId, values[0]));
                }
                else if (target.GetStat(Stat.mastery2) == 0)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2, values[0]));
                    fightHandler.FireEvent(new GainMasteryEvent(targetId, targetId, values[0]));
                }
                else if (target.GetStat(Stat.mastery3) == 0)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, values[0]));
                    fightHandler.FireEvent(new GainMasteryEvent(targetId, targetId, values[0]));
                }
                else
                {
                    Console.WriteLine("Mastery overdrawn");
                }
            }
            
            

        }
        #endregion
    }
}
