using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    public class MouseHandler : inputBase
    {
        bool inputBase.isInputDown => Input.GetButtonDown("Fire1");
        bool inputBase.isInputUp => Input.GetButtonUp("Fire1");

        Vector2 inputBase.inputPosition => Input.mousePosition;
    }
}
