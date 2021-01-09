using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Myths_Server
{
    class EntityCastSpellRule : Rule
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
            
            if(newEvent is EntityCastSpellEvent castEvent)
            {
                Console.WriteLine("Cast Spell rule activated");
                SpellDefinition spellCast = SpellDefinition.BuildFrom(castEvent.SpellId);

                //Range verif
                Unit caster = (Unit)fightHandler.Entities[castEvent.SourceId];
                int distanceFromCaster = Utils.GetDistance(caster.GetStat(Stat.x),
                    caster.GetStat(Stat.y), castEvent.X, castEvent.Y);



                Console.WriteLine("Spell cast from position "+ caster.GetStat(Stat.x)+
                    " "+ caster.GetStat(Stat.y)+" to "+ castEvent.X+" "+ castEvent.Y+
                    " Distance computed : "+ distanceFromCaster + "With spell range "+ spellCast.MinRange+"-"+ spellCast.MaxRange);

                int actualCost = Math.Max(0, spellCast.EnergyCost - Utils.GetCostReduction(caster.Owner, spellCast.Element));

                if (distanceFromCaster >= spellCast.MinRange && distanceFromCaster <= spellCast.MaxRange &&
                    fightHandler.Entities[castEvent.SourceId].GetStat(Stat.energy) >= actualCost &&
                    (!spellCast.IsUlt || (caster.Owner.GetStat(Stat.calls) >=1 && caster.GetStat(Stat.canUlt) == 1)))
                {
                    
                    

                    Context context = new Context(fightHandler, newEvent.SourceId, newEvent.SourceId);
                    context.X = castEvent.X;
                    context.Y = castEvent.Y;
                    int[] targetIds = spellCast.TargetSelector.GetTargets(context);

                    fightHandler.FireEvent(new EntityStatChangedEvent(castEvent.SourceId, castEvent.SourceId, Stat.energy,
                            fightHandler.Entities[castEvent.SourceId].GetStat(Stat.energy) - actualCost));
                    fightHandler.FireEvent(new SpellCastEvent(castEvent.TargetId, castEvent.SourceId,castEvent.SpellId,
                        castEvent.X, castEvent.Y));
                    if(spellCast.IsUlt)
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.calls,
                            caster.Owner.GetStat(Stat.calls) - 1));
                        fightHandler.FireEvent(new EntityStatChangedEvent(caster.Id, caster.Id, Stat.canUlt,
                            0));
                    }
                    switch (spellCast.Element)
                    {
                        case Mastery.arcane:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane, 0));
                            break;
                        case Mastery.light:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane, 0));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeLight, 0));
                            break;
                        case Mastery.dark:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane, 0));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeDark, 0));
                            break;
                        case Mastery.fire:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane, 0));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeFire, 0));
                            break;
                        case Mastery.earth:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane, 0));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeEarth, 0));
                            break;
                        case Mastery.air:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane, 0));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeAir, 0));
                            break;
                        case Mastery.water:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane, 0));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeWater, 0));
                            break;
                    }

                    List<Effect> effects = new List<Effect>();
                    foreach(EffectDefinition effectDef in spellCast.Effects)
                    {
                        effects.Add(Effect.BuildFrom(effectDef));
                    }

                    foreach(int target in targetIds)
                    {
                        ListeningEffect newListeningEffect = new ListeningEffect(target, InstantTrigger.GetInstantTrigger(),
                            InstantTrigger.GetInstantTrigger(), effects);
                        fightHandler.ListeningEffects.Add(newListeningEffect);

                        fightHandler.FireEvent(new ListeningEffectPlacedEvent(target, castEvent.SourceId, newListeningEffect.Id));

                        

                        
                    }
                    

                }
                else
                {
                    Console.WriteLine("Spell cast out of range or not enough energy");
                }
            }
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {

        }
        #endregion
    }
}
