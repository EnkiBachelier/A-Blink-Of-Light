using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HearingStimulus
{
    public Vector3 position;
    public float alertLevel;
}

public class AISenseHearing : AISense<HearingStimulus>
{
    public float maxDistance = 10;
    public float hearingThreshold = 0.5f;

    protected override bool doSense(Transform obj, ref HearingStimulus sti)
    {
        NoiseStatus n = obj.GetComponent<NoiseStatus>();
        sti.position = obj.position;
        if (n != null)
        {
            sti.alertLevel = n.noiseLevel;
            if (n.isHeardEverywhere || (n.noiseLevel > hearingThreshold && (obj.position - transform.position).sqrMagnitude < maxDistance * maxDistance)
            {
                return true;
            }
        }
        return false;
    }

    public new void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!ShowDebug)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}