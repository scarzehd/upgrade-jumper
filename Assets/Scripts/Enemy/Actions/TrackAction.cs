using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Track")]
public class TrackAction : Action
{
    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {
        if (controller.target != null)
        {
            Vector3 directionToPlayer = controller.GetDirection(controller.target.transform.position, controller.transform.position);
            if (!controller.Countdown(controller.trackTime)) {
                float rotateAmount = controller.trackSpeed * Time.deltaTime;
                //rotateAmount = rotateAmount * rotateAmount * (3f - 2f * rotateAmount);
                controller.currentTrackDirection = Vector3.Slerp(controller.currentTrackDirection, directionToPlayer, rotateAmount).normalized;
                controller.line.SetPosition(0, controller.transform.position);
                controller.line.SetPosition(1, controller.currentTrackDirection.normalized * controller.lineDistance + controller.transform.position);
            } else
            {
                controller.target = null;
                controller.TransitionToState(controller.currentState);
            }
        } else
        {
            controller.line.SetPosition(0, controller.transform.position);
            controller.line.SetPosition(1, controller.transform.position);
        }
    }
}
