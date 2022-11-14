using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAnimation : MonoBehaviour
{
    #region Variables Declarations
    private Animator thisAnimator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }

    #region New Methods
    //Blink vanishes in the air
    public void DestroyAnimation(bool isDestroyed)
    {
        thisAnimator.SetBool(Animator.StringToHash("isBlinkPickedUp"), isDestroyed);
    }

    //Blink emits no light
    public void LaunchedState(bool isLaunched)
    {
        thisAnimator.SetBool(Animator.StringToHash("isBlinkLaunched"), isLaunched);
    }
    #endregion
}
