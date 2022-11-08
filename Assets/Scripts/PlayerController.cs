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

    private AudioSource thisAudioSource;
    #endregion
    private void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        #region Blink system
        wantsToLaunchBlink = Input.GetMouseButtonDown(0);
        wantsToRecoverBlink = Input.GetMouseButtonDown(1);
        #endregion

        #region Movement system
        wantsToJump = Input.GetKeyDown(KeyCode.Space);
        horizontalValue = Input.GetAxisRaw("Horizontal");
        verticalValue = Input.GetAxisRaw("Vertical");

        if ((verticalValue != 0 || horizontalValue != 0) && !thisAudioSource.isPlaying)
        {
            thisAudioSource.Play();
            Debug.Log("Sound Play");
        }
        else if (verticalValue == 0 && horizontalValue == 0)
        {
            thisAudioSource.Stop();
            Debug.Log("Sound Pause");
        }
        #endregion

    }
}
