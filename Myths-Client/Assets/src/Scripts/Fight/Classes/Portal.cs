using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Portal :Entity
{
    #region Attributes

    #endregion

    #region Getters & Setters

    #endregion

    #region Constructor
    public Portal()
    {
    }

    public Portal(int id) : base(id, "Portal")
    {
        Stats.Add(Stat.x, 0);
        Stats.Add(Stat.y, 0);
    }

    #endregion
}

