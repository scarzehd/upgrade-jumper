using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Detect")]
public class DetectAction : Action
{
    public override void Act(StateController controller)
    {
        Detect(controller);
    }

    private void Detect(StateController controller)
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        Vector3 direction = controller.GetDirection(player.transform.position, controller.transform.position);
        float distance = Vector3.Distance(controller.transform.position, player.transform.position);
        bool los = !Physics2D.Raycast(controller.transform.position, direction, distance, controller.ground);
        if (distance > controller.detectionDistance) los = false;
        if (los == true)
        {
            controller.target = player;
        }
        else
        {
            controller.target = null;
        }
    }
}
