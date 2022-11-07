using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public PlayerController playerInput;
    public Transform playerBody;

    public float ProjectileStartSpeed = 50;
    public float OffsetForwardShoot = 2;

    private bool isBlinkInHand = false;
    private Rigidbody thisRigidBody;
    private SphereCollider thisSphereCollider;

    private MeshRenderer thisMeshRenderer;

    [SerializeField]
    private Material activeBlinkMaterial;
    [SerializeField]
    private Material inactiveBlinkMaterial;
    [SerializeField]
    private float offSetBlinkInHandX = 0;
    [SerializeField]
    private float offSetBlinkInHandY = 0;
    [SerializeField]
    private float offSetBlinkInHandZ = 0;

    private void Start()
    {
        thisRigidBody = transform.GetComponent<Rigidbody>();
        thisSphereCollider = transform.GetComponent<SphereCollider>();
        thisMeshRenderer = transform.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        CheckBlinkStatus();
    }

    private void CheckBlinkStatus()
    {
        if (playerInput.wantsToLaunchBlink && isBlinkInHand)
        {
            thisSphereCollider.enabled = true;
            transform.position = transform.position + transform.forward * OffsetForwardShoot;
            thisRigidBody.AddForce(transform.forward * ProjectileStartSpeed, ForceMode.Impulse);
            thisMeshRenderer.material = activeBlinkMaterial;
            isBlinkInHand = false;
        }

        if ((isBlinkInHand) || (playerInput.wantsToRecoverBlink && !isBlinkInHand))
        {
            thisSphereCollider.enabled = false;
            transform.position = new Vector3(playerBody.position.x + offSetBlinkInHandX, playerBody.position.y + offSetBlinkInHandY, playerBody.position.z + offSetBlinkInHandZ);
            thisMeshRenderer.material = inactiveBlinkMaterial;
            isBlinkInHand = true;
        }
    }
}
