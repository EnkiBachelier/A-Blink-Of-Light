using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public PlayerController playerInput;
    public Transform playerBody;
    public BlinkAnimation thisBlinkAnimation;

    public float ProjectileStartSpeed = 10;

    public bool isInHand { get; private set; } = false;
    public bool isMoving { get; private set; } = false;

    private Rigidbody thisRigidBody;
    private SphereCollider thisSphereCollider;

    [SerializeField]
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
    }

    void FixedUpdate()
    {
        if (thisRigidBody.velocity != Vector3.zero && !isInHand)
            isMoving = true;
        else
            isMoving = false;

        CheckBlinkStatus();
    }


    public void CheckBlinkStatus()
    {
        if (playerInput.wantsToLaunchBlink && isInHand)
        {
            thisBlinkAnimation.LaunchedState(true);
            thisSphereCollider.enabled = true;
            thisRigidBody.useGravity = true;
            greenLight.enabled = true;
            transform.parent = null;
            thisRigidBody.AddForce(transform.forward * ProjectileStartSpeed, ForceMode.Impulse);
            isInHand = false;
        }

        if (playerInput.wantsToRecoverBlink && !isInHand)
        {
            thisBlinkAnimation.LaunchedState(false);
            thisBlinkAnimation.DestroyAnimation(true);
            StartCoroutine(PickUp());
        }
    }

    private IEnumerator PickUp()
    {
        yield return new WaitForSeconds(3.8f);
        thisBlinkAnimation.DestroyAnimation(false);
        thisSphereCollider.enabled = false;
        greenLight.enabled = false;
        thisRigidBody.velocity = Vector3.zero;
        thisRigidBody.angularVelocity = Vector3.zero;
        thisRigidBody.useGravity = false;
        transform.parent = positionInHand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        isInHand = true;
    }

}
