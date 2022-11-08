using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAnimation : MonoBehaviour
{
    private Animator thisAnimator;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }

    public void DestroyAnimation(bool isDestroyed)
    {
        thisAnimator.SetBool(Animator.StringToHash("isBlinkPickedUp"), isDestroyed);
    }

    public void LaunchedState(bool isLaunched)
    {
        thisAnimator.SetBool(Animator.StringToHash("isBlinkLaunched"), isLaunched);
    }
}
