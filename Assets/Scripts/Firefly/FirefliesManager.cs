using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class FirefliesManager : MonoBehaviour
{
    private static FirefliesManager instance = null;
    public static FirefliesManager sharedInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<FirefliesManager>();
            }
            return instance;
        }
    }

    [SerializeField] private Blink thisBlink;
    [SerializeField] private float cooldownNewTarget = 0;

    private Vector3 target = new Vector3(0, 8, 0);
    public Firefly fireflyPrefab;
    public float nbBoids = 30;
    public float startSpeed = 1;
    public float startSpread = 10;

    public float maxDistBoids = 30;
    private List<Firefly> fireflies = new List<Firefly>();

    void Start()
    {
        for (int i = 0; i < nbBoids; i++)
        {
            Firefly b = GameObject.Instantiate<Firefly>(fireflyPrefab);
            Vector3 positionBoid = Random.insideUnitSphere * startSpread;
            positionBoid.y = Mathf.Abs(positionBoid.y);
            b.transform.position = positionBoid;
            b.velocity = (positionBoid - transform.position).normalized * startSpeed;
            b.transform.parent = this.transform;
            fireflies.Add(b);
        }
    }

    void Update()
    {
        cooldownNewTarget -= Time.deltaTime;

        if (cooldownNewTarget < 0 && thisBlink.isInHand)
        {
            target = new Vector3(Random.Range(4, 8), Random.Range(6, 11), Random.Range(4, 11));
            cooldownNewTarget = 7;
        }
        Vector3 targetBlink = thisBlink.transform.position;

        foreach (Firefly b in fireflies)
        {
            if (!thisBlink.isInHand)
                b.target = targetBlink;
            else
                b.target = target;

            b.goToTarget = true;
        }
    }

    public ReadOnlyCollection<Firefly> readFireflies
    {
        get { return new ReadOnlyCollection<Firefly>(fireflies); }
    }
}
