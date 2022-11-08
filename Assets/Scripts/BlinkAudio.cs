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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thisBlink.isMoving && !thisAudioSource.isPlaying)
        {
            thisAudioSource.loop = true;
            thisAudioSource.Play();
        }
        else if (!thisBlink.isMoving)
            thisAudioSource.Stop();
        
    }
}
