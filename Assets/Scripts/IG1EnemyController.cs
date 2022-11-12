using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IG1EnemyController : AgentController
{
    #region Variables declarations
    [SerializeField] private GameObject eyes;
    [SerializeField] private float radiusForPatrol;

    private AISenseHearing thisHearingSense;
    private float randomShutEyes;
    private static bool isPatrolling = true;
    private NavMeshAgent thisNavAgent;
    private static Vector3 debugRandomPosition;
    private static float debugRadius;
    #endregion

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform blink = GameObject.FindGameObjectWithTag("Blink").transform;

        thisNavAgent = GetComponent<NavMeshAgent>();
        thisHearingSense = GetComponent<AISenseHearing>();
        thisHearingSense.AddSenseHandler(new AISense<HearingStimulus>.SenseEventHandler(HandleHearing));
        thisHearingSense.AddObjectToTrack(player);
        thisHearingSense.AddObjectToTrack(blink);
        randomShutEyes = Random.Range(3, 6);
    }

    private void Update()
    {
        //Make the eyes appear randomly
        randomShutEyes -= Time.deltaTime;
        if (randomShutEyes <= 0)
        {
            eyes.SetActive(!eyes.activeInHierarchy);
            randomShutEyes = Random.Range(3, 6);
        }
        
        //if the AI is not hunting, we make it patrolling
        if (isPatrolling && !thisNavAgent.hasPath)
            FindPathTo(RandomNavmeshLocation(radiusForPatrol));
    }

    //Activates at every sound stimulus (player, blink...)
    private void HandleHearing(HearingStimulus sti, AISense<HearingStimulus>.Status evt)
    {
        if (evt == AISense<HearingStimulus>.Status.Enter)
        {
            Debug.Log("is Hunting");
            isPatrolling = false;
        }
        else if (evt == AISense<HearingStimulus>.Status.Stay)
        {
            Debug.Log("is Waiting");
            isPatrolling = false;

        }else if (evt == AISense<HearingStimulus>.Status.Leave)
        {
            Debug.Log("has stopped Hunting");
            isPatrolling = true;
        }
        FindPathTo(sti.position);
    }

    //Defines a random position on the navmesh in a given sphere
    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        debugRandomPosition = randomDirection;
        debugRadius = radius;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            finalPosition = hit.position;

        return finalPosition;
    }

    //Shows the patrolling sphere
    public new void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(debugRandomPosition, debugRadius);
    }
}
