using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        if (Vector2.Distance(controller.transform.position, controller.path[controller.path.Count - 1].transform.position) <= controller.nextNodeDistance)
        {
            //cycle through poi's
            Node node1 = controller.poi[0];
            controller.poi.RemoveAt(0);
            controller.poi.Add(node1);
            controller.path = controller.pathfinding.FindPath(controller.pathfinding.GetNearestNode(controller.transform.position), controller.poi[0], controller.mask);
        }

        if (Vector2.Distance(controller.transform.position, controller.path[0].transform.position) <= controller.nextNodeDistance)
        {
            //remove waypoints we've already reached
            controller.path.RemoveAt(0);
        }

        //check if we're on the ground
        controller.grounded = Physics2D.Raycast(controller.groundCheck.position, Vector2.down, controller.groundCheckDistance, controller.ground);

        //move and turn
        if (controller.path[0].transform.position.x > controller.transform.position.x)
        {
            if (controller.grounded == true)
            {
                controller.transform.localScale = new Vector3(1, controller.transform.localScale.y, controller.transform.localScale.z);

                controller.rb.velocity = new Vector2(controller.speed, controller.rb.velocity.y);
            }
        }
        else
        {
            if (controller.grounded == true)
            {
                controller.transform.localScale = new Vector3(-1, controller.transform.localScale.y, controller.transform.localScale.z);

                controller.rb.velocity = new Vector2(-controller.speed, controller.rb.velocity.y);
            }
        }

        if (controller.grounded == true)
        {
            //if we're on the ground, reset this so we can jump
            controller.alreadyJumped = false;
        }
    }
}
