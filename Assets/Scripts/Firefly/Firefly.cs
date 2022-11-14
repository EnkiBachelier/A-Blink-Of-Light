using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    [SerializeField] private float repulsionArea = 5;
    [SerializeField] private float alignmentArea = 9;
    [SerializeField] private float attractionArea = 50;

    [SerializeField] private float repulsionStrength = 15;
    [SerializeField] private float alignmentStrength = 3;
    [SerializeField] private float attractionStrength = 15;
    [SerializeField] private float minSpeed = 12;

    public float maxSpeed = 20;

    public Vector3 target = new Vector3();
    public float forceTarget = 20;
    public bool goToTarget = false;
    public Vector3 velocity = new Vector3();

    private void Update()
    {
        Vector3 sumForces = new Vector3();
        Color colorDebugForce = Color.black;
        float nbForcesApplied = 0;

        foreach (Firefly otherFirefly in FirefliesManager.sharedInstance.readFireflies)
        {
            Vector3 vecToOtherBoid = otherFirefly.transform.position - transform.position;

            Vector3 forceToApply = new Vector3();

            //Si on doit prendre en compte cet autre boid (plus grande zone de perception)
            if (vecToOtherBoid.sqrMagnitude < attractionArea * attractionArea)
            {
                //Si on est entre attraction et alignement
                if (vecToOtherBoid.sqrMagnitude > alignmentArea * alignmentArea)
                {
                    //On est dans la zone d'attraction uniquement
                    forceToApply = vecToOtherBoid.normalized * attractionStrength;
                    float distToOtherBoid = vecToOtherBoid.magnitude;
                    float normalizedDistanceToNextZone = ((distToOtherBoid - alignmentArea) / (attractionArea - alignmentArea));
                    float boostForce = (4 * normalizedDistanceToNextZone);
                    if (!goToTarget) //Encore plus de cohésion si pas de target
                        boostForce *= boostForce;
                    forceToApply = vecToOtherBoid.normalized * attractionStrength * boostForce;
                    colorDebugForce += Color.green;
                }
                else
                {
                    //On est dans alignement, mais est on hors de répulsion ?
                    if (vecToOtherBoid.sqrMagnitude > repulsionArea * repulsionArea)
                    {
                        //On est dans la zone d'alignement uniquement
                        forceToApply = otherFirefly.velocity.normalized * alignmentStrength;
                        colorDebugForce += Color.blue;
                    }
                    else
                    {
                        //On est dans la zone de repulsion
                        float distToOtherBoid = vecToOtherBoid.magnitude;
                        float normalizedDistanceToPreviousZone = 1 - (distToOtherBoid / repulsionArea);
                        float boostForce = (4 * normalizedDistanceToPreviousZone);
                        forceToApply = vecToOtherBoid.normalized * -1 * (repulsionStrength * boostForce);
                        colorDebugForce += Color.red;
                    }
                }

                sumForces += forceToApply;
                nbForcesApplied++;
            }
        }

        //On fait la moyenne des forces, ce qui nous rend indépendant du nombre de boids
        sumForces /= nbForcesApplied;

        //Si on a une target, on l'ajoute
        if (goToTarget)
        {
            Vector3 vecToTarget = target - transform.position;
            if (vecToTarget.sqrMagnitude < 1)
                goToTarget = false;
            else
            {
                Vector3 forceToTarget = vecToTarget.normalized * forceTarget;
                sumForces += forceToTarget;
                colorDebugForce += Color.magenta;
                nbForcesApplied++;
                Debug.DrawLine(transform.position, target, Color.magenta);
            }
        }

        Debug.DrawLine(transform.position, transform.position + sumForces, colorDebugForce / nbForcesApplied);
        //On freine
        velocity += -velocity * 10 * Vector3.Angle(sumForces, velocity) / 180.0f * Time.deltaTime;

        //on applique les forces
        velocity += sumForces * Time.deltaTime;

        //On limite la vitesse
        if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
            velocity = velocity.normalized * maxSpeed;
        if (velocity.sqrMagnitude < minSpeed * minSpeed)
            velocity = velocity.normalized * minSpeed;

        //On regarde dans la bonne direction        
        if (velocity.sqrMagnitude > 0)
            transform.LookAt(transform.position + velocity);

        Debug.DrawLine(transform.position, transform.position + velocity, Color.blue);
        //Deplacement du boid
        transform.position += velocity * Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
            // Répulsion
            Gizmos.color = new Color(1, 0, 0, 1.0f);
            Gizmos.DrawWireSphere(transform.position, repulsionArea);
            // Alignement
            Gizmos.color = new Color(0, 1, 0, 1.0f);
            Gizmos.DrawWireSphere(transform.position, alignmentArea);
            // Attraction
            Gizmos.color = new Color(0, 0, 1, 1.0f);
            Gizmos.DrawWireSphere(transform.position, attractionArea);
    }
}