using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class TransferDefenseEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public TransferDefenseEffect() : base()
        {
        }

        public TransferDefenseEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[context.SourceId].Definition.Name+
                " transfers defense to "+fightHandler.Entities[targetId].Definition.Name);

            Entity target = fightHandler.Entities[targetId];
            Entity source = fightHandler.Entities[context.SourceId];
            //give armor
            if (target.GetStat(Stat.armor) < target.Stats[Stat.armor])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                    (int)MathF.Min(target.Stats[Stat.armor], target.GetStat(Stat.armor) + source.GetStat(Stat.armor))));
            }
            fightHandler.FireEvent(new GainArmorEvent(targetId, targetId, source.GetStat(Stat.armor)));

            //Give barrier
            if (target.GetStat(Stat.barrier) < target.Stats[Stat.barrier])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier,
                    (int)MathF.Min(target.Stats[Stat.barrier], target.GetStat(Stat.barrier) + source.GetStat(Stat.barrier))));
            }
            // Warning : Gain barrier doesn't exist yet
            //fightHandler.FireEvent(new GainBarrierEvent(targetId, targetId, source.GetStat(Stat.barrier)));

            //Reset armor
            fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.armor, 0));
            //Reset barrier
            fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.barrier, 0));

        }
        #endregion
    }
}
