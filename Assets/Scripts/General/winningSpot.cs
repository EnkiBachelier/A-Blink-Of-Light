using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class winningSpot : MonoBehaviour
{
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private TextMeshProUGUI lossText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private AgentController thisAgent;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blink")
        {
            Debug.Log("You won !!");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            thisAgent.gameObject.SetActive(false);
            endCanvas.gameObject.SetActive(true);
            winText.gameObject.SetActive(true);
            lossText.gameObject.SetActive(false);
        }
    }
}
