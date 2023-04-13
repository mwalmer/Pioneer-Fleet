using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    public class TouchHandler : inputBase
    {
        bool inputBase.isInputDown => Input.GetTouch(0).phase == TouchPhase.Began;
        bool inputBase.isInputUp => Input.GetTouch(0).phase == TouchPhase.Ended;

        Vector2 inputBase.inputPosition => Input.GetTouch(0).position;
    }
}
