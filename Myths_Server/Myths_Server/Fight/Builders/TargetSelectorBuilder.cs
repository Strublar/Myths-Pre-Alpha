using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class TargetSelectorBuilder
    {
        public static Dictionary<SelectorType, Type> targetSelectorMap = new Dictionary<SelectorType, Type>()
            {
                {SelectorType.source, typeof(EffectSourceSelector)},
                {SelectorType.target, typeof(EventTargetSelector)},
                {SelectorType.effectHolder, typeof(EffectHolderSelector)},
                {SelectorType.simple, typeof(SimpleSelector)},
                {SelectorType.square, typeof(SquareSelector)},
                {SelectorType.all, typeof(AllSelector)},
                {SelectorType.effectHolderOwner, typeof(EffectHolderOwnerSelector)},
                {SelectorType.sourceOwner, typeof(EffectSourceOwnerSelector)},
                {SelectorType.between, typeof(BetweenSelector)},

            };



        public static TargetSelector BuildFrom(TargetSelectorDefinition definition)
        {

            TargetSelector newTargetSelector = Activator.CreateInstance(targetSelectorMap[definition.type]) as TargetSelector;
            newTargetSelector.Value = definition.value;
            return newTargetSelector;
        }

    }
}
