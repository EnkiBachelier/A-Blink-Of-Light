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
    public static float lastLevelHeard = 0;

    protected override bool doSense(Transform obj, ref HearingStimulus sti)
    {
        NoiseStatus n = obj.GetComponent<NoiseStatus>();
        sti.position = obj.position;
        if (n != null)
        {
            sti.alertLevel = n.noiseLevel;
            if ((n.isHeardEverywhere && lastLevelHeard != (float)NoiseStatus.noiseLevelStatus.PlayerLevel) || (n.noiseLevel > hearingThreshold && (obj.position - transform.position).sqrMagnitude < maxDistance * maxDistance))
            {
                lastLevelHeard = n.noiseLevel;
                return true;
            }
        }
        return false;
    }

    protected override void resetSense()
    {
        lastLevelHeard = 0;
    }

    public void OnDrawGizmos()
    {
        if (!ShowDebug)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}