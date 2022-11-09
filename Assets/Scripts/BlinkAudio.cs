using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAudio : MonoBehaviour
{
    #region Variables Declarations
    [SerializeField] private Blink thisBlink;
    [SerializeField] private AudioClip pickUpClip;
    private AudioSource thisAudioSource;
    #endregion

    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!thisBlink.isMoving && !thisBlink.isBeingPickedUp)
            thisAudioSource.Stop();

        if (thisBlink.isBeingPickedUp)
            thisAudioSource.PlayOneShot(pickUpClip);

        if (thisBlink.isBeingPickedUp && thisBlink.isMoving)
        {
            thisAudioSource.Stop();
            thisAudioSource.PlayOneShot(pickUpClip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thisBlink.isMoving && !thisAudioSource.isPlaying)
        {
            thisAudioSource.loop = true;
            thisAudioSource.volume = 2f;
            thisAudioSource.Play();
        }
    }
}
