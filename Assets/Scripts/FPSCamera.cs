using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    #region Variables Declarations
    [SerializeField]
    private float mouseSpeed = 4;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private float distanceToPlayer = 0;
    [SerializeField]
    private float upDownMax = 80;
    [SerializeField]
    private float upDownMin = -80;
    [SerializeField]
    private float rotationSmoothTime = 0.12f;


    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    private float orbit;
    private float upDown;
    #endregion
    void LateUpdate()
    {
        upDown -= Input.GetAxis("Mouse Y") * mouseSpeed;
        orbit += Input.GetAxis("Mouse X") * mouseSpeed;

        //Limitation on the vertical movements of the camera
        upDown = Mathf.Clamp(upDown, upDownMin, upDownMax);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(upDown, orbit), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        //We put the camera to a certain distance from the player
        transform.position = Player.position - transform.forward * distanceToPlayer;
    }
}
