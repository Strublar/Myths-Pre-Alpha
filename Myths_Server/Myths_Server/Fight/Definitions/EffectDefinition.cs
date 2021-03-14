using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Myths_Server
{
    class EffectDefinition
    {

        #region Attributes
        private int id;
        private string name;
        private Type effectType;
        private List<int> values;
        private TargetSelector targetSelector;
        private TargetSelector sourceSelector;
        private List<Condition> conditions;
        private bool isAbsolute;
        #endregion

        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Type EffectType { get => effectType; set => effectType = value; }
        public List<int> Values { get => values; set => this.values = value; }
        internal TargetSelector TargetSelector { get => targetSelector; set => targetSelector = value; }
        internal TargetSelector SourceSelector { get => sourceSelector; set => sourceSelector = value; }
        internal List<Condition> Conditions { get => conditions; set => conditions = value; }
        public bool IsAbsolute { get => isAbsolute; set => isAbsolute = value; }


        #endregion

        #region Constructor
        public EffectDefinition()
        { }

        public EffectDefinition(int id, string name, Type effectType, List<int> values, TargetSelector targetSelector,
            TargetSelector sourceSelector, List<Condition> conditions)
        {
            this.id = id;
            this.name = name;
            this.effectType = effectType;
            this.values = values;
            this.targetSelector = targetSelector;
            this.sourceSelector = sourceSelector;
            this.conditions = conditions;
        }
        #endregion

        #region Methods

        public static EffectDefinition BuildFrom(int effectId)
        {
            string dir = Environment.CurrentDirectory;
            string fileName = "../../../Data/Effects.csv";
            string path = Path.GetFullPath(fileName, dir);

            TextFieldParser parser = new TextFieldParser(path);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                //Process row
                string[] fields = parser.ReadFields();
                if (fields[0].Equals(effectId.ToString()))
                {
                    EffectDefinition newEffectDefinition = new EffectDefinition();
                    newEffectDefinition.Name = fields[1];
                    newEffectDefinition.EffectType = Type.GetType("Myths_Server." + fields[2]);

                    //Effect Values
                    newEffectDefinition.values = new List<int>();
                    string[] subStr = fields[3].Split(",");
                    foreach(string sub in subStr)
                    {
                        newEffectDefinition.values.Add(int.Parse(sub));
                    }
                    


                    //TargetSelector
                    Type targetSelectorType = Type.GetType("Myths_Server." + fields[4]);
                    object targetSelectorObject = Activator.CreateInstance(targetSelectorType);
                    if (targetSelectorObject is TargetSelector targetSelector)
                    {
                        newEffectDefinition.targetSelector = targetSelector;
                        newEffectDefinition.targetSelector.Value = Int32.Parse(fields[5]);
                    }

                    //SourceSelector
                    Type sourceSelecorType = Type.GetType("Myths_Server." + fields[6]);
                    object sourceSelectorObject = Activator.CreateInstance(sourceSelecorType);
                    if (sourceSelectorObject is TargetSelector sourceSelector)
                    {
                        newEffectDefinition.sourceSelector = sourceSelector;
                        newEffectDefinition.sourceSelector.Value = Int32.Parse(fields[7]);
                    }
                    //Absolute
                    bool isAbsolute = int.Parse(fields[8]) == 1 ? true : false;
                    newEffectDefinition.IsAbsolute = isAbsolute;
                    //Conditions
                    int fieldLength = fields.Length;
                    newEffectDefinition.conditions = new List<Condition>();
                    if(fieldLength>9)
                    {
                        for(int i = 9;i<fieldLength;i+=2)
                        {
                            Type conditionType = Type.GetType("Myths_Server." + fields[i]);
                            if(conditionType != null)
                            {
                                object conditionObject = Activator.CreateInstance(conditionType);
                                
                                if (conditionObject is Condition newConditionObject)
                                {
                                    //TODO PARAMS
                                    newConditionObject.Value = Int32.Parse(fields[i + 1]);
                                    newEffectDefinition.conditions.Add(newConditionObject);
                                    
                                }
                            }
                        }
                    }
                    return newEffectDefinition;
                }
            }

            return null;
        }

        #endregion


    }
}
