using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public void FindPathTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    public void OnDrawGizmos()
    {
        
    }
}
