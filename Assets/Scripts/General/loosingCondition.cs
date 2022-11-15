using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loosingCondition : MonoBehaviour
{
    [SerializeField] private PlayerController thisPlayer;
    [SerializeField] private AgentController thisAgent;
    [SerializeField] private FPSCamera thisCamera;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private TextMeshProUGUI lossText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private float limitForCatch = 1;
    [SerializeField] private Animator thisPlayerAnimator;

    public static bool hasCaught = false;


    void Update()
    {
        if ((thisAgent.transform.position - thisPlayer.transform.position).sqrMagnitude < limitForCatch * limitForCatch || hasCaught)
        {
            hasCaught = true;
            thisPlayer.transform.position = Vector3.one;
            StartCoroutine(LooseAnimation());
        }
    }

    private IEnumerator LooseAnimation()
    {
        thisCamera.distanceToPlayer = 3;
        thisPlayerAnimator.SetBool("isCaught", true);
        yield return new WaitForSeconds(0.3f);
        thisPlayerAnimator.SetBool("isCaught", false);
        hasCaught = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        endCanvas.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
        lossText.gameObject.SetActive(true);
    }
}
