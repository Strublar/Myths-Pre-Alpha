using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Passive
{
    #region Attributes
    private string name;
    private string description;
    #endregion

    #region Getters & Setters
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    #endregion

    #region Constructor
    public Passive()
    {

    }

    
    #endregion

    #region Methods
    public static Passive ParsePassive(int id)
    {
        string dir = Environment.CurrentDirectory;
        string csv = "/Assets/Resources/Passives/Passives.csv";
        string path = Path.GetFullPath(dir + csv);
        StreamReader strReader = new StreamReader(path);

        string fileString = strReader.ReadToEnd();
        string[] lines = fileString.Split('\n');
        string[] fields = lines[id + 1].Split(';');
        if (fields[0].Equals(id.ToString()))
        {
            Passive newPassive = new Passive();


            newPassive.Name = fields[1].TrimEnd(new char[] { '\r', '\n' });
            newPassive.Description = fields[2].TrimEnd(new char[] { '\r', '\n' });
            return newPassive;
        }
        else
        {
            throw new Exception(" Invalid passive Id given in builder, table might be wrong");
        }

    }
    #endregion
}

