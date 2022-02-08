using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private List<Node> openList;
    private List<Node> closedList;

    public GameObject nodeRoot;

    public Node[] nodes;

    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectsOfType<Node>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Node GetNearestNode(Vector3 position, Node[] nodeList = null)
    {
        return GetNearestNode(position, out float _, nodeList);
    }

    public  Node GetNearestNode(Vector3 position, out float distance, Node[] nodeList = null)
    {
        if (nodeList == null)
        {
            nodeList = nodes;
        }

        float lowestDistance = Vector3.Distance(position, nodeList[0].transform.position);
        Node closestNode = nodeList[0];
        foreach (Node node in nodeList)
        {
            float currentDistance = Vector3.Distance(position, node.transform.position);

            if (currentDistance < lowestDistance)
            {
                closestNode = node;
                lowestDistance = currentDistance;
            }
        }

        distance = lowestDistance;

        return closestNode;
    }

    public List<Node> FindPath(Node start, Node end, WalkabilityMask mask)
    {
        openList = new List<Node> { start };
        closedList = new List<Node>();

        Node[] nodes = nodeRoot.GetComponentsInChildren<Node>();

        foreach (Node node in nodes)
        {
            node.gCost = int.MaxValue;
            node.CalculateCost();
            node.previousNode = null;
        }

        start.gCost = 0;
        float startDistance = Vector3.Distance(start.transform.position, end.transform.position);
        start.hCost = Mathf.FloorToInt(startDistance * NodeConnection.MOVEMENT_COST);
        start.CalculateCost();

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestCost(openList);
            if (currentNode == end)
            {
                return CalculatePath(end);
            }   

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (NodeConnection connection in currentNode.connections)
            {
                Node connectedNode = connection.nodes[1];

                if (mask.jump == false && connection.mask.jump == true)
                {
                    closedList.Add(connectedNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + connection.cost;
                if (tentativeGCost < connectedNode.gCost)
                {
                    connectedNode.previousNode = currentNode;
                    connectedNode.currentWalkability = connection.mask;
                    connectedNode.gCost = tentativeGCost;
                    float distance = Vector3.Distance(connectedNode.transform.position, end.transform.position);
                    connectedNode.hCost = Mathf.FloorToInt(distance * NodeConnection.MOVEMENT_COST);
                    connectedNode.CalculateCost();
                    
                    if (!openList.Contains(connectedNode))
                    {
                        openList.Add(connectedNode);
                    }
                }
            }
        }

        //out of nodes

        return null;
    }

    private List<Node> CalculatePath(Node end)
    {
        List<Node> path = new List<Node>();
        path.Add(end);
        Node currentNode = end;
        while (currentNode.previousNode != null)
        {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }

        path.Reverse();
        return path;
    }

    private Node GetLowestCost(List<Node> nodes)
    {
        Node lowestCostNode = nodes[0];
        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i].fCost < lowestCostNode.fCost)
            {
                lowestCostNode = nodes[i];
            }
        }

        return lowestCostNode;
    }
}
