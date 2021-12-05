using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DisengageEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DisengageEffect() : base()
        {
        }

        public DisengageEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine(fightHandler.Entities[targetId].Name+
                " becomes engaged ");

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.isEngaged, 0));



        }
        #endregion
    }
}
