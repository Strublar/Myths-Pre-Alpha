using Microsoft.VisualBasic.FileIO;
using Myths_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Myths_Server
{
    class SpellDefinition
    {

        #region Attributes
        private int id;
        private string name;
        private TargetSelector targetSelector;
        private List<EffectDefinition> effects;
        private int energyCost, minRange, maxRange;
        private byte isUlt;
        private Mastery element;

        #endregion

        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int EnergyCost { get => energyCost; set => energyCost = value; }
        public int MinRange { get => minRange; set => minRange = value; }
        public int MaxRange { get => maxRange; set => maxRange = value; }
        internal TargetSelector TargetSelector { get => targetSelector; set => targetSelector = value; }
        internal List<EffectDefinition> Effects { get => effects; set => effects = value; }
        public Mastery Element { get => element; set => element = value; }
        public byte IsUlt { get => isUlt; set => isUlt = value; }

        #endregion

        #region Constructor
        public SpellDefinition()
        { }

        

        #endregion

        #region Methods

        public static SpellDefinition BuildFrom(int spellId)
        {
            string dir = Environment.CurrentDirectory;
            string fileName = "../../../Data/Spells.csv";
            string path = Path.GetFullPath(fileName, dir);

            TextFieldParser parser = new TextFieldParser(path);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                //Process row
                string[] fields = parser.ReadFields();
                if (fields[0].Equals(spellId.ToString()))
                {
                    SpellDefinition newSpellDefinition = new SpellDefinition();
                    //Name
                    newSpellDefinition.Name = fields[1];
                    //energyCost
                    newSpellDefinition.energyCost = Int32.Parse(fields[2]);
                    //Min & max range
                    newSpellDefinition.minRange = Int32.Parse(fields[3]);
                    newSpellDefinition.maxRange = Int32.Parse(fields[4]);
                    //element
                    newSpellDefinition.Element = (Mastery)Int32.Parse(fields[5]);

                    //isUlt
                    newSpellDefinition.isUlt = byte.Parse(fields[6]);
                    //TargetSelector
                    Type targetSelectorType = Type.GetType("Myths_Server." + fields[7]);
                    object targetSelectorObject = Activator.CreateInstance(targetSelectorType);
                    if (targetSelectorObject is TargetSelector targetSelector)
                    {
                        newSpellDefinition.targetSelector = targetSelector;
                        newSpellDefinition.targetSelector.Value = Int32.Parse(fields[8]);
                    }

                    //Effects
                    newSpellDefinition.effects = new List<EffectDefinition>();
                    if(fields[9] != "")
                    {
                        newSpellDefinition.effects.Add(EffectDefinition.BuildFrom(Int32.Parse(fields[9])));
                    }
                    if (fields[10] != "")
                    {
                        newSpellDefinition.effects.Add(EffectDefinition.BuildFrom(Int32.Parse(fields[10])));
                    }
                    if (fields[11] != "")
                    {
                        newSpellDefinition.effects.Add(EffectDefinition.BuildFrom(Int32.Parse(fields[11])));
                    }
                    if (fields[12] != "")
                    {
                        newSpellDefinition.effects.Add(EffectDefinition.BuildFrom(Int32.Parse(fields[12])));
                    }
                    if (fields[13] != "")
                    {
                        newSpellDefinition.effects.Add(EffectDefinition.BuildFrom(Int32.Parse(fields[13])));
                    }
                    if (fields[14] != "")
                    {
                        newSpellDefinition.effects.Add(EffectDefinition.BuildFrom(Int32.Parse(fields[14])));
                    }
                    return newSpellDefinition;
                }
            }

            return null;
        }

        #endregion


    }
}
