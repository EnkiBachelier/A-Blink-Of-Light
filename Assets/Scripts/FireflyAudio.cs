using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAudio : MonoBehaviour
{
    public float MinSoundDelay = 1;
    public float RandomDelay = 5;
    private float NextSoundDelay = 0;
    private AudioSource aSource;

    [SerializeField]
    private float maxPitch = 1.25f;
    [SerializeField]
    private float minPitch = 0.60f;

    private void computeNextSoundDelay()
    {
        NextSoundDelay = MinSoundDelay + Random.Range(0, RandomDelay);
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        computeNextSoundDelay();
    }

    void Update()
    {
        NextSoundDelay -= Time.deltaTime;
        if (NextSoundDelay <= 0)
        {
            aSource.pitch = Random.Range(minPitch, maxPitch);
            aSource.Play();
            computeNextSoundDelay();
        }
    }
}
