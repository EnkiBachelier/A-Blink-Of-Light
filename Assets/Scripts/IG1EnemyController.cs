using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IG1EnemyController : AgentController
{
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform blink = GameObject.FindGameObjectWithTag("Blink").transform;
        GetComponent<AISenseHearing>().AddSenseHandler(new AISense<HearingStimulus>.SenseEventHandler(HandleHearing));
        GetComponent<AISenseHearing>().AddObjectToTrack(player);
        GetComponent<AISenseHearing>().AddObjectToTrack(blink);
    }
    void HandleHearing(HearingStimulus sti, AISense<HearingStimulus>.Status evt)
    {
        if (evt == AISense<HearingStimulus>.Status.Enter)
            Debug.Log("Objet " + evt + " ouïe en " + sti.position);
        FindPathTo(sti.position);
    }
}
