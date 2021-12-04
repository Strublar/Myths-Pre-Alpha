using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public class Effect
{
    

    public EffectType effectType;

    //Effects data
    public Mastery element;
    public int amount;
    public bool temporary;
    public Stat stat;

    public EffectDefinition BuildDefinition()
    {
        EffectDefinition definition = new EffectDefinition()
        {
            effectType = effectType,
            element = element,
            amount = amount,
            temporary = temporary,
            stat = stat
        };

        return definition;
    }

}

