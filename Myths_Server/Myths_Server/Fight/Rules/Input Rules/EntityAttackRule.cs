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
                    List<int> values = new List<int> { fightHandler.Entities[newEvent.SourceId].GetStat(Stat.attack) };
                    effects.Add(new DealPhysicalDamageEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                    values));
                }
                else if(source.GetStat(Stat.attackType) == 2)
                {
                    List<int> values = new List<int> { fightHandler.Entities[newEvent.SourceId].GetStat(Stat.attack) };
                    effects.Add(new DealMagicalDamageEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                    values));
                }
                //notify damage done
                ListeningEffect newListeningEffect = 
                    new ListeningEffect(newEvent.TargetId, InstantTrigger.GetInstantTrigger(), InstantTrigger.GetInstantTrigger(), effects);
                fightHandler.ListeningEffects.Add(newListeningEffect);
                
                fightHandler.FireEvent(
                    new ListeningEffectPlacedEvent(newListeningEffect.HolderId,newListeningEffect.HolderId,newListeningEffect.Id));


                //Release Mastery
                List<Effect> effectsGauge = new List<Effect>();
                #region Convert Mastery

                effectsGauge.Add(new GainGaugePerMasteryEffect(new EventSourceSelector(), new EventTargetSelector(),
                    new List<int> { -1, -1, 1 }));

                ListeningEffect newListeningEffectGauge =
                    new ListeningEffect(source.Owner.Id, InstantTrigger.GetInstantTrigger(), InstantTrigger.GetInstantTrigger(), effectsGauge);
                fightHandler.ListeningEffects.Add(newListeningEffectGauge);

                fightHandler.FireEvent(new ListeningEffectPlacedEvent(newListeningEffectGauge.HolderId, source.Id,
                    newListeningEffectGauge.Id));

                /*fightHandler.FireEvent(new ListeningEffectPlacedEvent(source.Id, newListeningEffectGauge.HolderId,
                    newListeningEffectGauge.Id));*/

                int tmp1 = source.GetStat(Stat.mastery1),
                    tmp2 = source.GetStat(Stat.mastery2),
                    tmp3 = source.GetStat(Stat.mastery3);
                fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.mastery1, 0));
                fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.mastery2, 0));
                fightHandler.FireEvent(new EntityStatChangedEvent(source.Id, source.Id, Stat.mastery3, 0));
                if (tmp1 != 0)
                    fightHandler.FireEvent(new ConsumeMasteryEvent(source.Id, source.Id, tmp1));
                if (tmp2 != 0)
                    fightHandler.FireEvent(new ConsumeMasteryEvent(source.Id, source.Id, tmp2));
                if (tmp3 != 0)
                    fightHandler.FireEvent(new ConsumeMasteryEvent(source.Id, source.Id, tmp3));
                #endregion




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
