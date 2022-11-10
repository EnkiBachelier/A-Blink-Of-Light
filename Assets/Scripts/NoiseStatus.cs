using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseStatus : MonoBehaviour
{
    public float noiseLevel = 0;
    public bool isHeardEverywhere = false;

    public enum noiseLevelStatus
    {
        PlayerLevel = 10,
        BlinkLevel = 4,
        SFXLevel = 4,
        QuietLevel = 0
    };
}
