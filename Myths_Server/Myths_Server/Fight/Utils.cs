using System;
using System.Collections.Generic;
using System.Text;
using Myths_Library;


namespace Myths_Server
{
    class Utils
    {
        public static int ParseInt(byte[] message, int startIndex)
        {

            byte[] intArray = new byte[4];
            Array.Copy(message, startIndex, intArray, 0, 4);


            return BitConverter.ToInt32(intArray, 0);
        }

        public static int GetDistance(int x1, int y1, int x2, int y2)
        {
            return (int)(MathF.Abs(x2 - x1) + MathF.Abs(y2 - y1));
        }

        public static int GetCostReduction(Entity player, Mastery element)
        {
            int costReduction = 0;
            switch (element)
            {
                case Mastery.arcane:
                    costReduction += player.GetStat(Stat.gaugeArcane);
                    break;
                case Mastery.light:
                    costReduction += player.GetStat(Stat.gaugeArcane);
                    costReduction += player.GetStat(Stat.gaugeLight);
                    break;
                case Mastery.dark:
                    costReduction += player.GetStat(Stat.gaugeArcane);
                    costReduction += player.GetStat(Stat.gaugeDark);
                    break;
                case Mastery.fire:
                    costReduction += player.GetStat(Stat.gaugeArcane);
                    costReduction += player.GetStat(Stat.gaugeFire);
                    break;
                case Mastery.earth:
                    costReduction += player.GetStat(Stat.gaugeArcane);
                    costReduction += player.GetStat(Stat.gaugeEarth);
                    break;
                case Mastery.air:
                    costReduction += player.GetStat(Stat.gaugeArcane);
                    costReduction += player.GetStat(Stat.gaugeAir);
                    break;
                case Mastery.water:
                    costReduction += player.GetStat(Stat.gaugeArcane);
                    costReduction += player.GetStat(Stat.gaugeWater);
                    break;

            }
            return costReduction;
        }

        public static Stat GetGaugeFromMastery(Mastery mastery)
        {
            switch (mastery)
            {
                case Mastery.arcane:
                    return Stat.gaugeArcane;
                case Mastery.light:
                    return Stat.gaugeLight;
                case Mastery.dark:
                    return Stat.gaugeDark;
                case Mastery.fire:
                    return Stat.gaugeFire;
                case Mastery.earth:
                    return Stat.gaugeEarth;
                case Mastery.air:
                    return Stat.gaugeAir;
                case Mastery.water:
                    return Stat.gaugeWater;
            }
            return Stat.gaugeArcane;
        }
    }
    
}


