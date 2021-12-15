using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class EffectBuilder
    {
        public static Dictionary<EffectType, Type> effectsMap = new Dictionary<EffectType, Type>()
            {
                {EffectType.dealDamage, typeof(DealDamageEffect)},
                {EffectType.dealTrueDamage, typeof(DealTrueDamageEffect)},
                {EffectType.heal, typeof(HealEffect)},
                {EffectType.gainArmor, typeof(GainArmorEffect)},
                {EffectType.gainMastery, typeof(GainMasteryEffect)},
                {EffectType.modifyStat, typeof(ModifyStatEffect)},
                {EffectType.placeListeningEffect, typeof(PlaceListeningEffectEffect)},
                {EffectType.push, typeof(PushEffect)},
                {EffectType.pull, typeof(PullEffect)},
                {EffectType.swap, typeof(SwapEffect)},
                {EffectType.teleport, typeof(TeleportEffect)},
                {EffectType.consumeMastery, typeof(ConsumeMasteryEffect)},
                {EffectType.transferDefenses, typeof(TransferDefenseEffect)},
            };



        public static Effect BuildFrom(EffectDefinition definition)
        {
            
            Effect newEffect = Activator.CreateInstance(effectsMap[definition.effectType],definition) as Effect;
            return newEffect;
        }

    }
}
