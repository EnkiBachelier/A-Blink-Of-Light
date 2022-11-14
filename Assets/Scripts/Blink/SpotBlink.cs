using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotBlink : MonoBehaviour
{

    [SerializeField] private Transform thisBlinkToFollow;
    [SerializeField] private float distanceUpToBlink;

    void Update()
    {
        transform.position = thisBlinkToFollow.position + new Vector3(0, distanceUpToBlink, 0);
    }
}
