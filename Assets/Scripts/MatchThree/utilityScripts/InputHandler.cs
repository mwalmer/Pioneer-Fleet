using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    public interface inputBase
    {
        bool isInputDown { get; }
        bool isInputUp{ get; }
        Vector2 inputPosition { get; } 
    }
}
