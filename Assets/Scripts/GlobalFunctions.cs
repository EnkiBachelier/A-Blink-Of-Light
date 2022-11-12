using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    //Determines if the velocity of a small object is significant (must be redefine to match a big object)
    public static bool isItMoving(Vector3 velocityToCheck)
    {
        if (velocityToCheck.x > 0.05)
            return true;
        else if (velocityToCheck.y > 0.05)
            return true;
        else if (velocityToCheck.z > 0.05)
            return true;
        else
            return false;
    }
}
