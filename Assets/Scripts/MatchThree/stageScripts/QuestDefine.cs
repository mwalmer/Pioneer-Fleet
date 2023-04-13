using System;

namespace MatchThree.Quest
{
    public enum MatchType
    {
        NONE        = 0,
        THREE       = 3,    // 3 Match
        FOUR        = 4,    // 4 Match     
        FIVE        = 5,    // 5 Match     
        THREE_THREE = 6,    // 3 + 3 Match 
        THREE_FOUR  = 7,    // 3 + 4 Match 
        THREE_FIVE  = 8,    // 3 + 5 Match 
        FOUR_FIVE   = 9,    // 4 + 5 Match 
        FOUR_FOUR   = 10,   // 4 + 4 Match
    }

    static class MatchTypeMethod
    {
        public static short ToValue(this MatchType matchType)
        {
            return (short)matchType;
        }

        public static MatchType Add(this MatchType matchTypeSrc, MatchType matchTypeTarget)
        {
            if (matchTypeSrc == MatchType.FOUR && matchTypeTarget == MatchType.FOUR)
                return MatchType.FOUR_FOUR;

            return (MatchType)((int)matchTypeSrc + (int)matchTypeTarget);
        }
    }
}