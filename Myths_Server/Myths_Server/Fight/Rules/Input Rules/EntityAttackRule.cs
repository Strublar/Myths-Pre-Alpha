using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityAttackRule : Rule
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public override void OnEvent(Event newEvent, FightHandler fightHandler)
        {
            
            if(newEvent is EntityAttackEvent)
            {
                
                Console.WriteLine("Attack rule activated");
                List<Effect> effects = new List<Effect>();

                Unit source = (Unit)fightHandler.Entities[newEvent.SourceId];
                //Deal damages
                if (source.GetStat(Stat.attackType) == 1)
                {
                    effects.Add(new DealPhysicalDamageEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                    fightHandler.Entities[newEvent.SourceId].GetStat(Stat.attack)));
                }
                else if(source.GetStat(Stat.attackType) == 2)
                {
                    effects.Add(new DealMagicalDamageEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                    fightHandler.Entities[newEvent.SourceId].GetStat(Stat.attack)));
                }
                //notify damage done
                ListeningEffect newListeningEffect = 
                    new ListeningEffect(newEvent.TargetId, InstantTrigger.GetInstantTrigger(), InstantTrigger.GetInstantTrigger(), effects);
                fightHandler.ListeningEffects.Add(newListeningEffect);
                
                fightHandler.FireEvent(
                    new ListeningEffectPlacedEvent(newListeningEffect.HolderId,newListeningEffect.HolderId,newListeningEffect.Id));


                //Release Mastery
                List<Effect> effectsGauge = new List<Effect>();
                switch (source.GetStat(Stat.mastery1))
                {
                    case (int)Mastery.arcane:
                        effectsGauge.Add(new GainArcaneGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.light:
                        effectsGauge.Add(new GainLightGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.dark:
                        effectsGauge.Add(new GainDarkGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.fire:
                        effectsGauge.Add(new GainFireGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.earth:
                        effectsGauge.Add(new GainEarthGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;

                    case (int)Mastery.air:
                        effectsGauge.Add(new GainAirGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.water:
                        effectsGauge.Add(new GainWaterGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;

                }

                switch (source.GetStat(Stat.mastery2))
                {
                    case (int)Mastery.arcane:
                        effectsGauge.Add(new GainArcaneGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.light:
                        effectsGauge.Add(new GainLightGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.dark:
                        effectsGauge.Add(new GainDarkGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.fire:
                        effectsGauge.Add(new GainFireGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.earth:
                        effectsGauge.Add(new GainEarthGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.air:
                        effectsGauge.Add(new GainAirGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.water:
                        effectsGauge.Add(new GainWaterGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;

                }

                switch (source.GetStat(Stat.mastery3))
                {
                    case (int)Mastery.arcane:
                        effectsGauge.Add(new GainArcaneGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.light:
                        effectsGauge.Add(new GainLightGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.dark:
                        effectsGauge.Add(new GainDarkGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.fire:
                        effectsGauge.Add(new GainFireGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.earth:
                        effectsGauge.Add(new GainEarthGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.air:
                        effectsGauge.Add(new GainAirGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;
                    case (int)Mastery.water:
                        effectsGauge.Add(new GainWaterGaugeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                            1));
                        break;

                }

                ListeningEffect newListeningEffectGauge =
                    new ListeningEffect(source.Owner.Id, InstantTrigger.GetInstantTrigger(), InstantTrigger.GetInstantTrigger(), effectsGauge);
                fightHandler.ListeningEffects.Add(newListeningEffectGauge);

                fightHandler.FireEvent(new ListeningEffectPlacedEvent(source.Id, newListeningEffectGauge.HolderId,
                    newListeningEffectGauge.Id));

                int tmp1 = source.GetStat(Stat.mastery1),
                    tmp2 = source.GetStat(Stat.mastery2),
                    tmp3 = source.GetStat(Stat.mastery3);
                fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.mastery1, 0));
                fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.mastery2, 0));
                fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.mastery3, 0));
                if(tmp1 != 0)
                    fightHandler.FireEvent(new ConsumeMasteryEvent(source.Id, source.Id, tmp1));
                if(tmp2 != 0)
                    fightHandler.FireEvent(new ConsumeMasteryEvent(source.Id, source.Id, tmp2));
                if(tmp3 != 0)
                    fightHandler.FireEvent(new ConsumeMasteryEvent(source.Id, source.Id, tmp3));
                //Update that entity can't attack anymore this turn
                fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.SourceId, newEvent.SourceId, Stat.canAttack, 0));


            }
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {

        }
        #endregion
    }
}
