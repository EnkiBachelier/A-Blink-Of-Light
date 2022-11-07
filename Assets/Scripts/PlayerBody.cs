using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public sbyte speed = 4;
    public sbyte jump = 5;
    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    private Rigidbody rb;
    private bool onGround = true;

    public Transform cameraT;
    public PlayerController playerInputs;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }

    void Update()
    {
        /*
         
        Un simple système de saut :
            - On ajoute une force lorsque le joueur souhaite sauter
            - onGround permet d'éviter les doubles sauts en devenant faux si le joueur initie le saut et vrai si il rentre en collision (voir après)

        */

        if (playerInputs.wantsToJump && onGround)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            onGround = false;
        }

        /*
         
        Le système de mouvement du joueur :
            - 
          
        */
        
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 directionNorm = direction.normalized;

        if (directionNorm != Vector2.zero)
        {
            float playerRotation = Mathf.Atan2(directionNorm.x, directionNorm.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, playerRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        float currentSpeed = speed * directionNorm.magnitude;
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!onGround)
            onGround = true;
    }
}
