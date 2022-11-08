using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    public Transform prefab;
    public Vector3 offset;
    public static int stop = 0;

    // Update is called once per frame
    public void CreatePetitCube()
    {
        if(stop == 0)
            GameObject.Instantiate(prefab, transform.position + offset, Quaternion.identity);
        stop = 1;
    }
}
