using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    /**/
    public enum Swipe
    {
        NA      = -1,
        RIGHT   = 0,
        UP      = 1,
        LEFT    = 2,
        DOWN    = 3
    }

    public static class SwipeDirMethod
    {
        public static int getRow(this Swipe swipeDir)
        {
            switch (swipeDir)
            {
                case Swipe.DOWN: return -1; ;
                case Swipe.UP: return 1;
                default:
                    return 0;
            }
        }

        public static int getColumn(this Swipe swipeDir)
        {
            switch (swipeDir)
            {
                case Swipe.LEFT: return -1; ;
                case Swipe.RIGHT: return 1;
                default:
                    return 0;
            }
        }
    }


    public static class TouchEvaluator
    {
//get swipe direction
        public static Swipe getSwipeDirection(Vector2 vtStart, Vector2 vtEnd)
        {
            float angle = getAngle(vtStart, vtEnd);
            if (angle < 0)
                return Swipe.NA;

            int swipe = (((int)angle + 45) % 360) / 90;

            switch (swipe)
            {
                case 0: return Swipe.RIGHT;
                case 1: return Swipe.UP;
                case 2: return Swipe.LEFT;
                case 3: return Swipe.DOWN;
            }

            return Swipe.NA;
        }

        static float getAngle(Vector2 vtStart, Vector2 vtEnd)
        {
            Vector2 dragDirection = vtEnd - vtStart;
            if (dragDirection.magnitude <= 0.2f)
                return -1f;
            float aimAngle = Mathf.Atan2(dragDirection.y, dragDirection.x);
            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }
            return aimAngle * Mathf.Rad2Deg;
        }
    }
}
