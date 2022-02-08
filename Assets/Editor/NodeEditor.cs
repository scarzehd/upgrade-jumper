using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
class NodeEditor : Editor
{
    Node node;

    private void OnEnable()
    {
        node = (Node)target;
    }

    private void OnSceneGUI()
    {
        foreach (NodeConnection connection in node.connections)
        {
            if (connection.nodes.Length == 2)
            {
                Handles.DrawAAPolyLine(connection.nodes[0].transform.position, connection.nodes[1].transform.position);
                Vector3 between = Vector3.Lerp(connection.nodes[0].transform.position, connection.nodes[1].transform.position, 0.5f);
                between += Vector3.up * 0.5f;
                Handles.Label(between, connection.cost + "");
            }
        }
    }
}