using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loosingCondition : MonoBehaviour
{
    [SerializeField] private PlayerController thisPlayer;
    [SerializeField] private AgentController thisAgent;
    [SerializeField] private FPSCamera thisCamera;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private float limitForCatch = 1;

    public static bool hasCaught = false;
    [SerializeField] private Animator thisPlayerAnimator;

    void Start()
    {
    }

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
        yield return new WaitForSeconds(1.3f);
        endCanvas.enabled = true;
        
        //UI
    }
}
