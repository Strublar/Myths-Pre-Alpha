using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class FightDefines
    {

    }
    public enum Stat
    {
        //Pure stats

        hp,
        armor,
        energy,
        mobility,

        //Position stats
        x,
        y,

        //Control stats
        canMove,
        canRecall,
        canUlt1,
        canUlt2,
        canUlt3,

        //State stats
        isDead,
        isCalled,
        isEngaged,

        //Mana
        mana,

        //Player Stats
        calls
    }

    public enum Mastery : int
    {
        none = 0,
        arcane = 1,
        fire = 2,
        water = 3,
        earth = 4,
        air = 5,
        light = 6,
        dark = 7,
        any = -1
    }

    public enum GameEventType : int
    {
        entityStatChanged,
        endTurn
    }
}
