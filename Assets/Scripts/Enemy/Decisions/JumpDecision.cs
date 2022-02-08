using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Jump")]
public class JumpDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool jump = Jump(controller);
        return jump;
    }

    private bool Jump(StateController controller)
    {
        if (controller.grounded == false && controller.alreadyJumped == false)
        {
            return true;
        }
        return false;
    }
}
