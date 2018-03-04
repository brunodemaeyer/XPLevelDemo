using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GbangaExperienceLevelAPI
{
    public enum UpgradeType
    {
        linear, polynomial, exponential, manual
    };

    public class LevelModel
    {
        /// <summary>
        /// Define the way the amount of XP to reach the next level is calculated - Type Linear growth
        /// </summary>
        /// <param name="a">Constant amount of XP in each level</param>
        public LevelModel(long multiplier)
        {
            this.multiplier = multiplier;
            Type = UpgradeType.linear; 
        }

        /// <summary>
        /// Define the way the amount of XP to reach the next level is calculated - Types quadratic(a*n^b) and exponential(a*b^n)
        /// </summary>
        /// <param name="a">multiplier</param>
        /// <param name="b">the power (n^b) or the base (b^n)</param>
        /// <param name="type">quadratic or exponential</param>
        public LevelModel(long a, long b, UpgradeType type)
        {
            if (type == UpgradeType.exponential)
            {
                baseNum = b;
            }
            else if (type == UpgradeType.polynomial)
            {
                power = b;
            }
            multiplier = a;
            Type = type;
        }


        /// <summary>
        /// Define the way the amount of XP to reach the next level is calculated - Type manual - array of values
        /// </summary>
        /// <param name="boundaries">Array of total XP needed for each level, please make sure the first element is 0, to correspond to level 0</param>
        public LevelModel(long[] boundaries)
        {
            gaps = boundaries;
            Type = UpgradeType.manual;
        }

        long multiplier;
        long baseNum;
        long power;
        long[] gaps;

        public UpgradeType Type;

        //In this method the number of XP is calculated to REACH a certain level. To calculate the number of points needed between levels, simply perform a subtraction of XPNeededForLevel(level+1) -XPNeededForLevel(level)

        public long XPNeededForLevel(long level)
        {
            switch (Type)
            {
                case UpgradeType.linear:
                    return multiplier * level;
                case UpgradeType.exponential:
                    //1 must be substracted from the power (e.g. 2^0 = 1 and not 0), apart from when level=0, then we return 0
                    if (level == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return multiplier * (long)Math.Pow(baseNum, level - 1);
                    }
                case UpgradeType.polynomial:
                    return multiplier * (long)Math.Pow(level, power);
                case UpgradeType.manual:
                    if (level<= gaps.Count()-1)
                    {
                        return gaps[level];
                    }
                    //In case the array is not defined properly and does not contain enough elements, the xp needed will be set to long type maxvalue
                    else
                    {
                        return long.MaxValue;
                    }
                default:
                    return long.MaxValue;
            }
        }

        public long GetLevel(long points)
        {
            switch (Type)
            {
                case UpgradeType.linear:
                    return points / multiplier;
                case UpgradeType.exponential:
                    return 1+ (long)Math.Floor(Math.Log(points / multiplier, baseNum));
                case UpgradeType.polynomial:
                    return (long)Math.Floor(Math.Pow((double)points / multiplier, 1 / (double)power));
                case UpgradeType.manual:
                    int level = 0;
                    while (gaps[level] <= points)
                    {
                        level++;
                    }
                    return level - 1;
                default:
                    return long.MaxValue;
            }
        }
    }
}
