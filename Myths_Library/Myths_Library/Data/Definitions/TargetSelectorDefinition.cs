using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class TargetSelectorDefinition
    {
        public SelectorType type;
        public int value;
    }
    public enum SelectorType : byte
    {

        source,
        target,
        effectHolder,
        simple,
        square,
        all,
        effectHolderOwner,
        sourceOwner,
        targetOwner,
        between

    }
}
