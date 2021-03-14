using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainGaugePerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainGaugePerMasteryEffect() : base()
        {
        }

        public GainGaugePerMasteryEffect(TargetSelector sources, TargetSelector targets, List<int> values)
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {
            //Values : GaugeCode, MasteryCode, Count
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
                case (int)Mastery.any:
                    GainGaugePerAnyMastery(targetId, context, fightHandler);
                    return;
            }
            #endregion
            int count = values.Count > 2 ? values[2] : 1;
            int element = values.Count > 1 ? values[1] : 0;
            int computedValue = 0;
            Entity source = fightHandler.Entities[context.SourceId];
            if (source.GetStat(Stat.mastery1) == element)
            {
                computedValue += count;
            }
            if (source.GetStat(Stat.mastery2) == element)
            {
                computedValue += count;
            }
            if (source.GetStat(Stat.mastery3) == element)
            {
                computedValue += count;
            }

            Entity target = fightHandler.Entities[targetId];
            Console.WriteLine(target.Definition.Name+" gains  " + computedValue + " " +gauge);

            
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                gauge, target.GetStat(gauge) + computedValue));

        }

        public void GainGaugePerAnyMastery(int targetId, Context context, FightHandler fightHandler)
        {
            Console.WriteLine("Gaining Gauge per any mastery");
            int count = values.Count > 2 ? values[2] : 1;
            Entity source = fightHandler.Entities[context.SourceId];
            if (source.GetStat(Stat.mastery1) != 0)
            {
                Stat gauge = Utils.GetGaugeFromMastery((Mastery)source.GetStat(Stat.mastery1));
                Entity target = fightHandler.Entities[targetId];
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                    gauge, target.GetStat(gauge) + count));
            }
            if (source.GetStat(Stat.mastery2) != 0)
            {
                Stat gauge = Utils.GetGaugeFromMastery((Mastery)source.GetStat(Stat.mastery2));
                Entity target = fightHandler.Entities[targetId];
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                    gauge, target.GetStat(gauge) + count));
            }
            if (source.GetStat(Stat.mastery3) != 0)
            {
                Stat gauge = Utils.GetGaugeFromMastery((Mastery)source.GetStat(Stat.mastery3));
                Entity target = fightHandler.Entities[targetId];
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                    gauge, target.GetStat(gauge) + count));
            }
        }
        #endregion
    }
}
