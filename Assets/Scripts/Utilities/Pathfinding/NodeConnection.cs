using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeConnection
{
    public const int MOVEMENT_COST = 10;
    public const int JUMP_COST = 10;

    public WalkabilityMask mask;

    public Node[] nodes;
    public int cost;

    public void CalculateCost()
    {
        if (nodes.Length > 0)
        {
            if (nodes[0] != null && nodes[1] != null)
            {
                float distance = Vector3.Distance(nodes[0].transform.position, nodes[1].transform.position);
                cost = Mathf.FloorToInt(distance * MOVEMENT_COST + (mask.jump ? JUMP_COST : 0));
            }
        }
    }
}
