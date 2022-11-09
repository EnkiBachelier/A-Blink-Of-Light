using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    #region Variables Declarations
    [SerializeField]
    private sbyte speed = 4;
    [SerializeField]
    private PlayerController playerInputs;
    [SerializeField]
    private AudioSource thisAudioSource;
    [SerializeField]
    private float mass = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 velocity;

    private bool isOnGround = true;
    private CharacterController thisCharController;
    #endregion

    void Start()
    {
        thisCharController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        isOnGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isOnGround && velocity.y < 0)
            velocity.y = -2f; //not 0f because this might happen before we are on the ground, so to make sure the player IS indeed on the ground we put a slightly lower negative number

        float horizontalValue = playerInputs.horizontalValue;
        float verticalValue = playerInputs.verticalValue;

        //Vector 3 move = new Vector3(x, 0f, z); --- FOR GLOBAL SPACE

        //direction based on our x & z movement on local coordinates
        Vector3 move = transform.right * horizontalValue + transform.forward * verticalValue;

        //function Move, framerate independent & with a given speed
        thisCharController.Move(move * speed * Time.fixedDeltaTime);

        //to allow the player to jump
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.fixedDeltaTime * mass;
        thisCharController.Move(velocity * Time.fixedDeltaTime);

        if ((horizontalValue != 0 || verticalValue != 0) && !thisAudioSource.isPlaying && isOnGround)
        {
            thisAudioSource.loop = true;
            thisAudioSource.Play();
        }
        else if ((horizontalValue == 0 && verticalValue == 0) || !isOnGround)
            thisAudioSource.Stop();
    }

}
