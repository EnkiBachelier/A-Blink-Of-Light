using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables Declarations
    public bool wantsToJump { get; private set; } = false;
    public bool wantsToLaunchBlink { get; private set; } = false;
    public bool wantsToRecoverBlink { get; private set; } = false;
    #endregion

    void Update()
    {
        #region Movement system
        wantsToJump = Input.GetKeyDown(KeyCode.Space);
        #endregion

        #region Blink system
        wantsToLaunchBlink = Input.GetMouseButtonDown(0);
        wantsToRecoverBlink = Input.GetMouseButtonDown(1);
        #endregion
    }
}
