using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    #region Variables Declarations
    [SerializeField]
    private sbyte speed = 4;
    [SerializeField]
    private sbyte jump = 5;
    [SerializeField]
    private float turnSmoothTime = 0.2f;
    [SerializeField]
    private Transform cameraT;
    [SerializeField]
    private PlayerController playerInputs;
    [SerializeField]
    private AudioSource thisAudioSource;
    
    private Rigidbody thisRigidbody;
    private float turnSmoothVelocity;
    private bool isOnGround = true;
    #endregion

    void Start()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }

    void FixedUpdate()
    {
        /*
         
        Un simple système de saut :
            - On ajoute une force lorsque le joueur souhaite sauter
            - isOnGround permet d'éviter les doubles sauts en devenant faux si le joueur initie le saut et vrai si il rentre en collision (voir après)

        */


        if (playerInputs.wantsToJump && isOnGround)
        {
            thisRigidbody.AddForce(Vector3.up * jump, ForceMode.Impulse);
            isOnGround = false;
        }

        /*
         
        Le système de mouvement du joueur :
            - 
          
        */
        
        Vector2 direction = new Vector2(playerInputs.horizontalValue, playerInputs.verticalValue);
        Vector2 directionNorm = direction.normalized;

        if (directionNorm != Vector2.zero)
        {
            float playerRotation = Mathf.Atan2(directionNorm.x, directionNorm.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, playerRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        float currentSpeed = speed * directionNorm.magnitude;
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        if (currentSpeed != 0 && !thisAudioSource.isPlaying && isOnGround)
        {
            thisAudioSource.loop = true;
            thisAudioSource.Play();
        }
        else if (currentSpeed == 0 || !isOnGround)
            thisAudioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isOnGround)
            isOnGround = true;

    }

}
