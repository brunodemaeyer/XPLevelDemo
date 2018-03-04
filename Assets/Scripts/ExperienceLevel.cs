using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GbangaExperienceLevelAPI_basic
{
    public class ExperienceLevel
    {
        //initialise new player experience status - at the start the player is a complete nobody
        public ExperienceLevel()
        {
            totalExperience = 0;
            experience = 0;
            level = 0;

            //100 experience points equal 1 level
            levelGap = 100;
        }

        //overload to add a different levelGap
        public ExperienceLevel(long levelGap )
        {
            totalExperience = 0;
            experience = 0;
            level = 0;

            //100 experience points equal 1 level
            this.levelGap = levelGap;
        }

        private long experience;
        private long totalExperience;
        private long level;
        private long levelGap;

        //get current level
        public long GetLevel()
        {
            return level;
        }

        //Get level value for an arbitrary experience value
        public long ExperienceToLevel(long experience)
        {
            return experience / levelGap;
        }

        //get minimum experience value for an arbitrary level value
        public long LevelToExperience(long level)
        {
            return level * levelGap;
        }

        //get experience needed until next level-up
        public long ExperienceTillLevelUp()
        {
            return levelGap - experience;
        }

        //get percentual experience progress between last and next level-up
        public long ProgressToNextLevel()
        {
            return (long)((double)experience *100 / levelGap);
        }

        //get experience delta between current experience and arbitrary level value
        public long ExperienceDelta(long level)
        {
            return Math.Abs(level * levelGap - totalExperience);
        }

        //add experience points, update points between levels and total experience points at the same time
        //check and handle cases where multiple levels are crossed in one go
        public void AddExperience(long amount)
        {
            experience += amount;
            totalExperience += amount;
            //check if we have reached a new level
            while (experience >= levelGap)
            {
                nextLevel();
            }
        }

        //subtract experience points - Level stays the same even when experience between levels drops below zero
        public void SubtractExperience(long amount)
        {
                experience -= amount;
                totalExperience -= amount;
        }

        //updates level to the next one if requirements are met in the AddExperience method
        private void nextLevel()
        {
            level++;
            experience -= levelGap;
        }

        //override the current level and set it to an arbitrary value; this resets the totalExperience points to the minimum total experience points needed for that level; current experience points are set to 0
        public void SetLevel(long level)
        {
            this.level = level;
            totalExperience = LevelToExperience(level);
            experience = 0;
        }

        //reset experience to last level up
        public void ResetExperience()
        {
            totalExperience -= experience;
            experience = 0;
        }
        
        //Helper functions to get the values of private variables
        public long GetCurrentExperience()
        {
            return experience;
        }

        public long GetTotalExperience()
        {
            return totalExperience;
        }
    }
}
