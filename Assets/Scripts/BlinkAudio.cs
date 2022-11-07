using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAudio : MonoBehaviour
{
    private AudioSource thisAudioSource;
    private float soundDelay = 0;
    
    [SerializeField]
    private Blink thisBlink;

    // Start is called before the first frame update
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        soundDelay -= Time.deltaTime;
        if(!thisBlink.isMoving)
            thisAudioSource.Pause();
        else if (thisBlink.isMoving && soundDelay <= 0)
        {
            thisAudioSource.Play();
            soundDelay = 5;
        }
    }
}
