using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    #region Variables Declarations
    public bool isInHand { get; private set; } = false;
    public bool isMoving { get; private set; } = false;

    [SerializeField]
    private PlayerController playerInput;
    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private BlinkAnimation thisBlinkAnimation;
    [SerializeField]
    private float ProjectileStartSpeed = 10;
    [SerializeField]
    private Light greenLight;
    [SerializeField]
    private Transform positionInHand;

    private Rigidbody thisRigidBody;
    private SphereCollider thisSphereCollider;
    #endregion

    private void Start()
    {
        thisRigidBody = transform.GetComponent<Rigidbody>();
        thisSphereCollider = transform.GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        //Used for the sound
        if (GlobalFunctions.isItMoving(thisRigidBody.velocity) && !isInHand)
            isMoving = true;
        else
            isMoving = false;

        CheckBlinkStatus();
    }

    #region New Methods
    //Either puts the Blink next to the player in inactive mode (no light, no bloom, ready to be launched)
    //Or throw the Blink forward in active mode (light, bloom, collider, ready to be picked up)
    //If the Blink is picked up, launches an animation
    public void CheckBlinkStatus()
    {
        //If the player launches the Blink
        if (playerInput.wantsToLaunchBlink && isInHand)
        {
            isInHand = false;
            thisBlinkAnimation.LaunchedState(true);
            thisSphereCollider.enabled = true;
            thisRigidBody.useGravity = true;
            greenLight.enabled = true;
            transform.parent = null;
            thisRigidBody.AddForce(transform.forward * ProjectileStartSpeed, ForceMode.Impulse);
        }
        //If the player pickes up the Blink
        else if (playerInput.wantsToRecoverBlink && !isInHand)
        {
            thisBlinkAnimation.LaunchedState(false);
            thisBlinkAnimation.DestroyAnimation(true);
            StartCoroutine(PickUpAnimation());
        }
    }

    //Used to display the PickUpAnimation and put the Blink in inactive mode after the animation 
    private IEnumerator PickUpAnimation()
    {
        isInHand = true;
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
    }
    #endregion
}
