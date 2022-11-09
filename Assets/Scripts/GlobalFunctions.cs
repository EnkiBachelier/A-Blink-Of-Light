using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    public static bool isItMoving(Vector3 velocityToCheck)
    {
        if (velocityToCheck.x > 0.2)
            return true;
        else if (velocityToCheck.y > 0.2)
            return true;
        else if (velocityToCheck.z > 0.2)
            return true;
        else
            return false;
    }
}
