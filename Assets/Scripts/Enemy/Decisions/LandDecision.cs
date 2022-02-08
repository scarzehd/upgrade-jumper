using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Land")]
public class LandDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return Land(controller);
    }

    private bool Land(StateController controller)
    {
        if (controller.grounded == true && controller.alreadyJumped == true)
        {
            return true;
        }
        return false;
    }
}
