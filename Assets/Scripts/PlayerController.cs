using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables Declarations
    public bool wantsToJump { get; private set; } = false;
    public bool wantsToLaunchBlink { get; private set; } = false;
    public bool wantsToRecoverBlink { get; private set; } = false;
    public float horizontalValue { get; private set; } = 0;
    public float verticalValue { get; private set; } = 0;
    public Rigidbody thisRigidbody { get; private set; }
    #endregion
    private void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        #region Blink system
        wantsToLaunchBlink = Input.GetMouseButtonDown(0);
        wantsToRecoverBlink = Input.GetMouseButtonDown(1);
        #endregion

        #region Movement system
        wantsToJump = Input.GetKeyDown(KeyCode.Space);
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        #endregion

    }
}
