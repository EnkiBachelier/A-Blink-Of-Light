using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loosingCondition : MonoBehaviour
{
    [SerializeField] private PlayerController thisPlayer;
    [SerializeField] private AgentController thisAgent;
    [SerializeField] private Camera thisCamera;
    [SerializeField] private float limitForCatch = 1;

    public static bool hasCaught = false;
    private Animator thisPlayerAnimator;
    private Animator thisCameraAnimator;

    void Start()
    {
        thisPlayerAnimator = thisPlayer.gameObject.GetComponent<Animator>();
        thisCameraAnimator = thisCamera.GetComponent<Animator>();
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
        thisCameraAnimator.SetBool("isCaught", true);
        yield return new WaitForSeconds(3.8f);
        
        //UI
    }
}
