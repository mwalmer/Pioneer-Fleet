using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    public class InputManager
    {
        Transform thisContainer;

#if UNITY_ANDROID && !UNITY_EDITOR
        inputBase thisInputHandler = new TouchHandler();
#else
        inputBase thisInputHandler = new MouseHandler();
#endif
        public InputManager(Transform container)
        {
            thisContainer = container;
        }

        public bool isTouchDown => thisInputHandler.isInputDown;
        public bool isTouchUp => thisInputHandler.isInputUp;
        public Vector2 touchPosition => thisInputHandler.inputPosition;
        public Vector2 touch2BoardPosition => touchPos(thisInputHandler.inputPosition);

        Vector2 touchPos(Vector3 vtInput)
        {
            //Convert coordinates
            Vector3 vtMousePosW = Camera.main.ScreenToWorldPoint(vtInput);
            Vector3 vtContainerLocal = thisContainer.transform.InverseTransformPoint(vtMousePosW);
            return vtContainerLocal;
        }

        public Swipe evalSwapDirection(Vector2 vtStart, Vector2 vtEnd)
        {
            return TouchEvaluator.getSwipeDirection(vtStart, vtEnd);
        }
    }
}