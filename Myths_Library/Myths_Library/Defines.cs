using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class FightDefines
    {
        public static int GetDistance(int x1, int y1, int x2, int y2)
        {
            return (Math.Abs(x2 - x1) + Math.Abs(y2 - y1));
        }

        public static Stat GetStatFromMastery(Mastery element)
        {
            Dictionary<Mastery, Stat> converter = new Dictionary<Mastery, Stat>
        {
            {Mastery.fire,Stat.masteryFire },
            {Mastery.water,Stat.masteryWater },
            {Mastery.air,Stat.masteryAir },
            {Mastery.earth,Stat.masteryEarth },
            {Mastery.light,Stat.masteryLight },
            {Mastery.dark,Stat.masteryDark }
        };

            return converter[element];
        }
    }
    public enum Stat : byte
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
        calls,
        masteryLight,
        masteryDark,
        masteryFire,
        masteryWater,
        masteryAir,
        masteryEarth
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
        endTurn,
        entityCall,
        entityCastSpell,
        entityMove,
        entityRecall,
        beginTurn,
        endGame,
        entityDies,
        entityCalled,
        entityMoved,
        entityStatChanged,
        firstSpellCast,
        listeningEffectPlaced,
        spellCast
    }
}
