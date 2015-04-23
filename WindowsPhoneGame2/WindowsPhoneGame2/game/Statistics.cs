using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RufusAndTheMagicMushrooms.game
{
    class Statistics
    {
        public static int maxJumps = 0;
        public static int maxMushrooms = 0;
        public static int maxPoints = 0;
        public static int maxCombo = 0;
        public static int maxSpecials = 0;
        public static int maxBloobs = 0;

        public static int totalJumps = 0;
        public static int totalMushrooms = 0;
        public static int totalSpecials = 0;

        public static string toString()
        {
            return maxMushrooms.ToString() + "," + maxBloobs.ToString() + "," + maxSpecials.ToString() + "," + maxCombo.ToString() + "," + maxJumps.ToString() + "," + maxPoints.ToString();
        }

    }
}
