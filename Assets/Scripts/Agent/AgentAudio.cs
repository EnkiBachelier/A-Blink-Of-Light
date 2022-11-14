using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudio : MonoBehaviour
{
    #region Variables Declarations
    [SerializeField] private AgentController thisAgent;
    [SerializeField] private PlayerController thatPlayer;
    [SerializeField] private AudioClip huntingClip;
    [SerializeField] private AudioClip caughtClip;
    [SerializeField] private AudioClip[] idleNoises;

    private float cooldownNoise;
    private AudioSource thisAudioSource;
    #endregion

    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        cooldownNoise = Random.Range(6, 10);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownNoise -= Time.deltaTime;
        if (thisAgent.isHunting && cooldownNoise < 0)
        {
            thisAudioSource.PlayOneShot(huntingClip);
            cooldownNoise = Random.Range(6, 10);
        }

        if (!thisAgent.isHunting && cooldownNoise < 0 && !thisAudioSource.isPlaying)
        {
            thisAudioSource.PlayOneShot(idleNoises[Random.Range(0, idleNoises.Length)]);
            cooldownNoise = Random.Range(6, 10);
        }

        if (loosingCondition.hasCaught)
        {
            if (thisAudioSource.clip != caughtClip)
                thisAudioSource.Stop();

            if (!thisAudioSource.isPlaying)
            {
                thisAudioSource.clip = caughtClip;
                thisAudioSource.PlayOneShot(caughtClip);
            }

        }
    }

}
