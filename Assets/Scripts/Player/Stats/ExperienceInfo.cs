using System;

namespace Player
{
    public static class ExperienceInfo
    {
        public static int CalculateLevel(int exp, out int remaindExp)
        {
            int level = 1;
            int expForLevel = 100;

            while (exp >= expForLevel)
            {
                exp -= expForLevel;
                level++;

                expForLevel = (int)Math.Round(expForLevel * 1.1, 0, MidpointRounding.AwayFromZero);
            }

            remaindExp = exp;

            return level;
        }

        public static int CalculateLevel(int exp)
        {
            int level = 1;
            int expForLevel = 100;

            while (exp >= expForLevel)
            {
                exp -= expForLevel;
                level++;

                expForLevel = (int)Math.Round(expForLevel * 1.1, 0, MidpointRounding.AwayFromZero);
            }

            return level;
        }

        public static int GetExpForNext(int exp)
        {
            int expForLevel = 100;

            while (exp >= expForLevel)
            {
                exp -= expForLevel;

                expForLevel = (int)Math.Round(expForLevel * 1.1, 0, MidpointRounding.AwayFromZero);
            }

            return expForLevel;
        }
    }
}