using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAudio : MonoBehaviour
{
    #region Variables Declarations
    [SerializeField]
    private Blink thisBlink;

    private AudioSource thisAudioSource;
    #endregion

    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();    
    }

    void Update()
    {
        if (!thisBlink.isMoving)
            thisAudioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thisBlink.isMoving && !thisAudioSource.isPlaying)
        {
            thisAudioSource.loop = true;
            Debug.Log("Start roll");
            thisAudioSource.Play();
        }
    }
}
