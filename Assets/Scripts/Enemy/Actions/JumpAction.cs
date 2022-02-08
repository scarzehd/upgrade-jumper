using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Jump")]
public class JumpAction : Action
{
    public override void Act(StateController controller)
    {
        //start jumping if we haven't already
        if (controller.alreadyJumped == false)
        {
            controller.alreadyJumped = true;
            controller.StartCoroutine(Jump(controller.path[0].transform.position, controller));
        }

        //check if we're on the ground
        controller.grounded = Physics2D.Raycast(controller.groundCheck.position, Vector2.down, controller.groundCheckDistance, controller.ground);
    }

    public System.Collections.IEnumerator Jump(Vector3 target, StateController controller, float jumpAngle = 65)
    {

        controller.rb.velocity = new Vector3(0, controller.rb.velocity.y, 0);

        yield return new WaitForSeconds(controller.jumpTime);

        float gravity = controller.rb.gravityScale * Physics2D.gravity.magnitude;

        //calculate angle in radians
        float angle = jumpAngle * Mathf.Deg2Rad;

        //remove z components of positions
        Vector3 planarTarget = new Vector3(target.x, target.y, 0);
        Vector3 planarPosition = new Vector3(controller.transform.position.x, controller.transform.position.y, 0);

        //distance between us and the target
        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = controller.transform.position.y - target.y;

        //get initial velocity
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(initialVelocity * Mathf.Cos(angle), initialVelocity * Mathf.Sin(angle), 0);

        //rotate velocity to match the direction between us and the target
        float angleBetweenObjects = Vector3.Angle(Vector2.right, planarTarget - planarPosition);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector2.up) * velocity;

        //jump!
        controller.rb.velocity = finalVelocity;
    }
}
