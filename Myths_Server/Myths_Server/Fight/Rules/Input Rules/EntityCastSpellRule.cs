using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Myths_Library;

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

                //Range verification
                Unit caster = (Unit)fightHandler.Entities[castEvent.SourceId];
                int distanceFromCaster = Utils.GetDistance(caster.GetStat(Stat.x),
                    caster.GetStat(Stat.y), castEvent.X, castEvent.Y);


                Console.WriteLine("Spell cast from position "+ caster.GetStat(Stat.x)+
                    " "+ caster.GetStat(Stat.y)+" to "+ castEvent.X+" "+ castEvent.Y+
                    " Distance computed : "+ distanceFromCaster + "With spell range "+ spellCast.MinRange+"-"+ spellCast.MaxRange);

                int actualCost = Math.Max(0, spellCast.EnergyCost - Utils.GetCostReduction(caster.Owner, spellCast.Element));

                bool ultCondition = (spellCast.IsUlt == 0 ||
                        (caster.Owner.GetStat(Stat.calls) >= 1 &&
                            (
                                (spellCast.IsUlt == 1 && caster.GetStat(Stat.canUlt1) == 1) ||
                                (spellCast.IsUlt == 2 && caster.GetStat(Stat.canUlt2) == 1) ||
                                (spellCast.IsUlt == 3 && caster.GetStat(Stat.canUlt3) == 1) 
                            )
                        )
                    );

                if (distanceFromCaster >= spellCast.MinRange && distanceFromCaster <= spellCast.MaxRange &&
                    fightHandler.Entities[castEvent.SourceId].GetStat(Stat.energy) >= actualCost &&
                    ultCondition)
                {

                    fightHandler.FireEvent(new EntityStatChangedEvent(castEvent.SourceId, castEvent.SourceId, Stat.energy,
                            fightHandler.Entities[castEvent.SourceId].GetStat(Stat.energy) - actualCost));
                    fightHandler.FireEvent(new SpellCastEvent(castEvent.TargetId, castEvent.SourceId,castEvent.SpellId,
                        castEvent.X, castEvent.Y));

                    #region Can Ult Update
                    if (spellCast.IsUlt > 0)
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.calls,
                            caster.Owner.GetStat(Stat.calls) - 1));
                        switch(spellCast.IsUlt)
                        {
                            case 1:
                                fightHandler.FireEvent(new EntityStatChangedEvent(caster.Id, caster.Id, Stat.canUlt1,
                                0));
                                break;
                            case 2:
                                fightHandler.FireEvent(new EntityStatChangedEvent(caster.Id, caster.Id, Stat.canUlt2,
                                0));
                                break;
                            case 3:
                                fightHandler.FireEvent(new EntityStatChangedEvent(caster.Id, caster.Id, Stat.canUlt3,
                                0));
                                break;
                        }
                        
                    }
                    #endregion

                    #region Gauge Consumption save
                    int[] tmpMast = new int[]
                    {
                        caster.Owner.GetStat(Stat.gaugeArcane),
                        caster.Owner.GetStat(Stat.gaugeLight),
                        caster.Owner.GetStat(Stat.gaugeDark),
                        caster.Owner.GetStat(Stat.gaugeFire),
                        caster.Owner.GetStat(Stat.gaugeEarth),
                        caster.Owner.GetStat(Stat.gaugeAir),
                        caster.Owner.GetStat(Stat.gaugeWater)
                    };
                    #endregion

                    Context context = new Context(fightHandler, newEvent.SourceId, newEvent.SourceId);
                    context.X = castEvent.X;
                    context.Y = castEvent.Y;
                    context.OriginX = caster.GetStat(Stat.x);
                    context.OriginY = caster.GetStat(Stat.y);


                    #region Effects distribution
                    List<Effect> effects = new List<Effect>();
                    List<Effect> absoluteEffects = new List<Effect>();
                    foreach(EffectDefinition effectDef in spellCast.Effects)
                    {
                        Effect newEffect = Effect.BuildFrom(effectDef);
                        newEffect.EffectContext = context;
                        if (effectDef.IsAbsolute)
                        {
                            absoluteEffects.Add(newEffect);
                        }
                        else
                        {
                            effects.Add(newEffect);
                        }
                        
                    }
                    #endregion

                    #region Absolute effects
                    ListeningEffect newListeningEffectAbsolute = new ListeningEffect(caster.Id, 
                            InstantTrigger.GetInstantTrigger(),
                            InstantTrigger.GetInstantTrigger(), absoluteEffects);
                    fightHandler.ListeningEffects.Add(newListeningEffectAbsolute);

                    fightHandler.FireEvent(new ListeningEffectPlacedEvent(castEvent.SourceId, castEvent.SourceId, 
                        newListeningEffectAbsolute.Id));
                    #endregion

                    int[] targetIds = spellCast.TargetSelector.GetTargets(context);
                    foreach (int target in targetIds)
                    {
                        ListeningEffect newListeningEffect = new ListeningEffect(target, InstantTrigger.GetInstantTrigger(),
                            InstantTrigger.GetInstantTrigger(), effects);
                        fightHandler.ListeningEffects.Add(newListeningEffect);
                        Event targetEvent = new ListeningEffectPlacedEvent(target, castEvent.SourceId, newListeningEffect.Id);
                        targetEvent.Context = context;
                        fightHandler.FireEvent(targetEvent);
                    }

                    #region Consume Gauges
                    switch (spellCast.Element)
                    {
                        case Mastery.arcane:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane,
                                caster.Owner.GetStat(Stat.gaugeArcane) - tmpMast[0]));
                            break;
                        case Mastery.light:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane,
                                caster.Owner.GetStat(Stat.gaugeArcane) - tmpMast[0]));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeLight,
                                caster.Owner.GetStat(Stat.gaugeLight) - tmpMast[1]));
                            break;
                        case Mastery.dark:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane,
                                caster.Owner.GetStat(Stat.gaugeArcane) - tmpMast[0]));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeDark,
                                caster.Owner.GetStat(Stat.gaugeDark) - tmpMast[2]));
                            break;
                        case Mastery.fire:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane,
                                caster.Owner.GetStat(Stat.gaugeArcane) - tmpMast[0]));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeFire,
                                caster.Owner.GetStat(Stat.gaugeFire) - tmpMast[3]));
                            break;
                        case Mastery.earth:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane,
                                caster.Owner.GetStat(Stat.gaugeArcane) - tmpMast[0]));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeEarth,
                                caster.Owner.GetStat(Stat.gaugeEarth) - tmpMast[4]));
                            break;
                        case Mastery.air:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane,
                                caster.Owner.GetStat(Stat.gaugeArcane) - tmpMast[0]));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeAir,
                                caster.Owner.GetStat(Stat.gaugeAir) - tmpMast[5]));
                            break;
                        case Mastery.water:
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeArcane,
                                caster.Owner.GetStat(Stat.gaugeArcane) - tmpMast[0]));
                            fightHandler.FireEvent(
                                new EntityStatChangedEvent(caster.Owner.Id, caster.Owner.Id, Stat.gaugeWater,
                                caster.Owner.GetStat(Stat.gaugeWater) - tmpMast[6]));
                            break;
                    }
                    #endregion
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
