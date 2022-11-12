using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winningSpot : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blink")
            Debug.Log("You won !!");
    }
}
