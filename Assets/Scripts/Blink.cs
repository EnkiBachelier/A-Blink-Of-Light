using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public PlayerController playerInput;
    public Transform playerBody;

    public float ProjectileStartSpeed = 50;
    public float OffsetForwardShoot = 2;

    public bool isInHand { get; private set; } = false;
    public bool isMoving { get; private set; } = false;

    private Rigidbody thisRigidBody;
    private SphereCollider thisSphereCollider;
    private MeshRenderer thisMeshRenderer;

    
    [SerializeField]
    private Light greenLight;
    [SerializeField]
    private Transform positionInHand;
    [SerializeField]
    private Material activeBlinkMaterial;
    [SerializeField]
    private Material inactiveBlinkMaterial;

    private void Start()
    {
        thisRigidBody = transform.GetComponent<Rigidbody>();
        thisSphereCollider = transform.GetComponent<SphereCollider>();
        thisMeshRenderer = transform.GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if (thisRigidBody.velocity != Vector3.zero && !isInHand)
            isMoving = true;
        else
            isMoving = false;

        CheckBlinkStatus();
    }


    private void CheckBlinkStatus()
    {
        if (playerInput.wantsToLaunchBlink && isInHand)
        {
            thisSphereCollider.enabled = true;
            greenLight.enabled = true;
            transform.parent = null;
            thisMeshRenderer.material = activeBlinkMaterial;
            thisRigidBody.AddForce(transform.forward * ProjectileStartSpeed, ForceMode.Impulse);
            isInHand = false;
        }

        if ((isInHand) || (playerInput.wantsToRecoverBlink && !isInHand))
        {

            thisSphereCollider.enabled = false;
            greenLight.enabled = false;
            thisMeshRenderer.material = inactiveBlinkMaterial;
            thisRigidBody.velocity = Vector3.zero;
            thisRigidBody.angularVelocity = Vector3.zero;
            transform.parent = positionInHand;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            isInHand = true;
        }
    }
}
