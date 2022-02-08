using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{
    public int gCost;
    public int hCost;
    public int fCost;

    public Node previousNode;

    public WalkabilityMask currentWalkability;

    public List<NodeConnection> connections;

    void Start()
    {
        previousNode = null;
        gCost = int.MaxValue;

        foreach (NodeConnection connection in connections)
        {
            connection.CalculateCost();
        }
    }

    private void Update()
    {
        foreach (NodeConnection connection in connections)
        {
            connection.CalculateCost();
        }
    }

    public void CalculateCost()
    {
        fCost = gCost + hCost;
    }

    private void OnDrawGizmos()
    {
        Node node = this;
    }

    public override string ToString()
    {
        return "g: " + gCost + ", h: " + hCost + ", f: " + fCost;
    }
}
