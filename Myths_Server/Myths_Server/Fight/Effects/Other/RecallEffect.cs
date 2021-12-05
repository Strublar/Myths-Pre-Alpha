using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class RecallEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public RecallEffect() : base()
        {
        }

        public RecallEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine(fightHandler.Entities[targetId].Name+
                " becomes Recalls ");

            fightHandler.FireEvent(new EntityRecallEvent(targetId,targetId));



        }
        #endregion
    }
}
