using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class TargetSelector
{

    public SelectorType selector;
    public int value;

    public TargetSelectorDefinition BuildDefinition()
    {
        TargetSelectorDefinition definition = new TargetSelectorDefinition()
        {
            type = selector,
            value = value
        };

        return definition;
    }
}
