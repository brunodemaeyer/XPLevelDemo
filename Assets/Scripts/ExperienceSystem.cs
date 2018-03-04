using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GbangaExperienceLevelAPI
{
    public class ExperienceSystem
    {
        //initialise new player experience status - at the start the player is a complete nobody
        public ExperienceSystem(LevelModel levelExp)
        {
            totalExperience = 0;
            experience = 0;
            level = 0;
            updateLevelGap(levelExp);
        }

        private long experience; //the current experience, resets after level-up
        private long totalExperience; //the total experience since start of the game
        private long level; //the experience level
        private long levelGap; //the gap between previous and next level, counted in experience
        
        
        //Following fields are for future implementation
        //private long highestLevel;


        //get current level
        public long GetLevel()
        {
            return level;
        }

        //Get level value for an arbitrary experience value
        public long ExperienceToLevel(long experience,LevelModel levelExp)
        {
            return levelExp.GetLevel(experience);
        }

        //get minimum experience value for an arbitrary level value
        public long LevelToExperience(long level, LevelModel levelExp)
        {
            return levelExp.XPNeededForLevel(level);
        }

        //get experience needed until next level-up
        public long ExperienceTillLevelUp(LevelModel levelExp)
        {
            //return levelExp.XPNeededForLevel(level + 1) - totalExperience;
            return levelGap - experience;
        }

        //get percentual experience progress between last and next level-up
        public long ProgressToNextLevel(LevelModel levelExp)
        {
            return (long)((double)experience *100 / levelGap);
        }

        //get experience delta between current experience and arbitrary level value
        public long ExperienceDelta(long level, LevelModel levelExp)
        {
            return Math.Abs(LevelToExperience(level, levelExp) - totalExperience);
        }

        //Calculate total Experience needed in the current level step
        private void updateLevelGap(LevelModel levelExp)
        {
            levelGap = LevelToExperience(level+1,levelExp) - LevelToExperience(level,levelExp);
        }

        //add experience points, update points between levels and total experience points at the same time
        //check and handle cases where multiple levels are crossed in one go
        public void AddExperience(long amount,LevelModel levelExp)
        {
            experience += amount;
            totalExperience += amount;

            //check if we have reached a new level till our experience points do not reach a next level
            while (experience >= levelGap)
            {
                nextLevel(levelExp);
            }            
        }

        //subtract experience points - Level stays the same even when experience between levels drops below zero
        public void SubtractExperience(long amount)
        {
                experience -= amount;
                totalExperience -= amount;
        }

        //updates level to the next one if requirements are met in the AddExperience method
        private void nextLevel(LevelModel levelExp)
        {
            level++;
            experience -= levelGap;
            if (levelExp.Type != UpgradeType.linear)
            {
                updateLevelGap(levelExp); //Gap might change in the newly set level
            }
        }

        //override the current level and set it to an arbitrary value; this resets the totalExperience points to the minimum total experience points needed for that level; current experience points are set to 0

        public void SetLevel(long level, LevelModel levelExp)
        {
            this.level = level;
            totalExperience = LevelToExperience(level, levelExp);
            experience = 0;
            if (levelExp.Type != UpgradeType.linear)
            {
                updateLevelGap(levelExp); //Gap might change in the newly set level
            }
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

        public long GetLevelGap()
        {
            return levelGap;
        }
    }
}
