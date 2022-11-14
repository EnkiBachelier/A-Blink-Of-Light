using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAudio : MonoBehaviour
{
    #region Variables Declarations
    [SerializeField] private Blink thisBlink;
    [SerializeField] private AudioClip pickUpClip;
    private AudioSource thisAudioSource;
    private NoiseStatus thisNoiseStatus;
    #endregion

    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        thisNoiseStatus = GetComponent<NoiseStatus>();
    }

    void Update()
    {
        if (!thisBlink.isMoving && !thisBlink.isBeingPickedUp)
        {
            thisAudioSource.Stop();
            thisNoiseStatus.noiseLevel = 0;
            thisNoiseStatus.isHeardEverywhere = false;

        }

        if (thisBlink.isBeingPickedUp)
        {
            thisAudioSource.PlayOneShot(pickUpClip);
            thisNoiseStatus.isHeardEverywhere = true;
        }

        if (thisBlink.isBeingPickedUp && thisBlink.isMoving)
        {
            thisAudioSource.Stop();
            thisAudioSource.PlayOneShot(pickUpClip);
            thisNoiseStatus.isHeardEverywhere = true;
        }
        if (!thisAudioSource.isPlaying)
        {
            thisNoiseStatus.noiseLevel = (float)NoiseStatus.noiseLevelStatus.QuietLevel;
            thisNoiseStatus.isHeardEverywhere = false;
        }
        else
            thisNoiseStatus.noiseLevel = (float)NoiseStatus.noiseLevelStatus.BlinkLevel;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thisBlink.isMoving && !thisAudioSource.isPlaying)
        {
            thisAudioSource.loop = true;
            thisAudioSource.volume = 2f;
            thisAudioSource.Play();
            thisNoiseStatus.isHeardEverywhere = false;
        }
    }
}
