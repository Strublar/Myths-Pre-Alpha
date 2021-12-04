using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainGaugeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainGaugeEffect() : base()
        {
        }

        public GainGaugeEffect(TargetSelector sources, TargetSelector targets, List<int> values)
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {
            //Values = Gauge code, Count
            int count = values.Count > 1 ? values[1] : 1;
            Stat gauge = Stat.gaugeArcane;
            #region selecting gauge
            switch (values[0])
            {
                case (int)Mastery.arcane:
                    gauge = Stat.gaugeArcane;
                    break;
                case (int)Mastery.light:
                    gauge = Stat.gaugeLight;
                    break;
                case (int)Mastery.dark:
                    gauge = Stat.gaugeDark;
                    break;
                case (int)Mastery.fire:
                    gauge = Stat.gaugeFire;
                    break;
                case (int)Mastery.earth:
                    gauge = Stat.gaugeEarth;
                    break;

                case (int)Mastery.air:
                    gauge = Stat.gaugeAir;
                    break;
                case (int)Mastery.water:
                    gauge = Stat.gaugeWater;
                    break;
            }
            #endregion
            Entity target = fightHandler.Entities[targetId];
            Console.WriteLine(target.Definition.Name+" gains  " + count + " " +gauge);

            
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                gauge, target.GetStat(gauge) + count));

        }
        #endregion
    }
}
