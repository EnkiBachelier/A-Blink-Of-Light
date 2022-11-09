using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickAgentController : AgentController
{
    private Ray rayPickPos; //Déclaré ici pour pouvoir le visualiser, il doit rester accessible entre deux clics
    public Camera thatDebugCam;
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rayPickPos = thatDebugCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(rayPickPos, out rh))
            {
                FindPathTo(rh.point);
            }
        }
    }

    public new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(rayPickPos.origin, rayPickPos.direction * 100);
    }
}
