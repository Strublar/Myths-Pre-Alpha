using Myths_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Spell
{

    #region Attributes
    private int id;
    private Myth owner;

    private string name;
    private string description;
    private Mastery element;
    private int energyCost;
    private int minRange, maxRange;
    private string shape;


    #endregion

    #region Getters & Setters
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int EnergyCost { get => energyCost; set => energyCost = value; }
    public int MinRange { get => minRange; set => minRange = value; }
    public int MaxRange { get => maxRange; set => maxRange = value; }
    public Myth Owner { get => owner; set => owner = value; }
    public Mastery Element { get => element; set => element = value; }
    public string Shape { get => shape; set => shape = value; }
    #endregion

    #region Static methods
    public static Spell ParseSpell(int id)
    {
        Spell newSpell = new Spell();
        newSpell.Id = id;

        string dir = Environment.CurrentDirectory;
        string csv = "/Assets/Resources/Spells/Spells.csv";
        string path = Path.GetFullPath(dir + csv);
        StreamReader strReader = new StreamReader(path);

        string fileString = strReader.ReadToEnd();
        string[] lines = fileString.Split('\n');
        string[] fields = lines[id + 1].Split(';');
        if (fields[0].Equals(id.ToString()))
        {

            newSpell.Name = fields[1].TrimEnd(new char[] { '\r', '\n' });
            newSpell.energyCost = int.Parse(fields[2]);
            newSpell.minRange = int.Parse(fields[3]);
            newSpell.maxRange = int.Parse(fields[4]);
            newSpell.element = (Mastery)int.Parse(fields[5]);
            newSpell.Shape = fields[6].TrimEnd(new char[] { '\r', '\n' });
            newSpell.Description = fields[7].TrimEnd(new char[] { '\r', '\n' });
        }
        else
        {
            throw new Exception(" Invalid Unit Id given in builder, table might be wrong");
        }

        return newSpell;
    }

    #endregion
}
