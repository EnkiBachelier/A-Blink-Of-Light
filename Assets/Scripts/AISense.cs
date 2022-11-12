using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AISense<Stimulus> : MonoBehaviour
{
    public enum Status
    {
        Enter,
        Stay,
        Leave
    };

    public float updateInterval = 3.0f;
    private float updateTime = 0.0f;

    public bool ShowDebug = true;
    protected List<Transform> trackedObjects = new List<Transform>();
    protected List<Transform> sensedObjects = new List<Transform>();

    public delegate void SenseEventHandler(Stimulus sti, Status sta);
    private event SenseEventHandler CallSenseEvent;

    public Status statusAI;

    //We called for sense on a certain basis and if something is detected we launch an event
    void Update()
    {
        Stimulus stimulus;
        statusAI = Status.Stay;

        updateTime += Time.deltaTime;
        if (updateTime > updateInterval)
        {
            resetSense();

            foreach (Transform t in trackedObjects)
            {
                stimulus = default(Stimulus);
                if (doSense(t, ref stimulus))
                {
                    statusAI = Status.Stay;
                    if (!sensedObjects.Contains(t))
                    {
                        sensedObjects.Add(t);
                        statusAI = Status.Enter;
                    }
                    CallSenseEvent(stimulus, statusAI);
                }
                else
                {
                    if (sensedObjects.Contains(t))
                    {
                        statusAI = Status.Leave;
                        CallSenseEvent(stimulus, statusAI);
                        sensedObjects.Remove(t);
                    }
                }
            }
            updateTime = 0;
        }
    }

    //Determine if something is sensed
    protected abstract bool doSense(Transform obj, ref Stimulus sti);

    protected virtual void resetSense()
    {

    }

    public void AddSenseHandler(SenseEventHandler handler)
    {
        CallSenseEvent += handler;
    }

    public void AddObjectToTrack(Transform t)
    {
        trackedObjects.Add(t);
    }
}
