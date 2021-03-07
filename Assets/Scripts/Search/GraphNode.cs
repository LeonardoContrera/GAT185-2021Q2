using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : SearchNode
{
    public enum eType
    {
        Default,
        Source,
        Destination,
        Path,
        Visited
    }

    Color[] colors =
    {
        Color.white,
        Color.green,
        Color.red,
        Color.yellow,
        Color.blue
    };

    public struct Edge
    {
        public GraphNode nodeA; //this node
        public GraphNode nodeB; // conecting node
    }

    public List<Edge> edges { get; set; } = new List<Edge>();
    public eType Type { get; set; } = eType.Default;
    public GraphNode Parent { get; set; } = null;
    public bool Visited { get; set; } = false;
    public float Cost { get; set; } = float.MaxValue;
    public float Heuristic { get; set; } = 0;


    void Update()
    {
        GetComponent<Renderer>().material.color = colors[(int)Type];

        foreach( Edge edge in edges )
        {
            Debug.DrawLine(edge.nodeA.transform.position, edge.nodeB.transform.position);
        }
    }

    public static GraphNode[] GetGraphNodes()
    {
        return GameObject.FindObjectsOfType<GraphNode>();
    }

    public static GraphNode GetGraphNode(eType type)
    {
        GraphNode[] graphNodes = GetGraphNodes();
        foreach( GraphNode graphNode in graphNodes)
        {
            if(graphNode.Type == type)
            {
                return graphNode;
            }
        }

        return null;
    }

    public static void UnlinkNodes()
    {
        GraphNode[] graphNodes = GetGraphNodes();
        foreach (GraphNode graphNode in graphNodes)
        {
            graphNode.edges.Clear();
        }
    }

    public static void LinkNodes(float range)
    {
        GraphNode[] graphNodes = GetGraphNodes();
        foreach(GraphNode graphNode in graphNodes)
        {
            LinkNode(graphNode, range);
        }
    }

    public static void LinkNode(GraphNode node, float range)
    {
        Collider[] colliders = Physics.OverlapSphere(node.transform.position, range);
        foreach (Collider collider in colliders)
        {
            GraphNode otherNode = collider.GetComponent<GraphNode>();
            if (otherNode != null && otherNode != node)
            {
                GraphNode.Edge edge;
                edge.nodeA = node;
                edge.nodeB = otherNode;

                node.edges.Add(edge);
            }
        }
    }

    public static void ClearNodeType(eType type)
    {
        GraphNode[] graphNodes = GetGraphNodes();
        foreach (GraphNode graphNode in graphNodes)
        {
            if (graphNode.Type == type)
            {
                graphNode.Type = eType.Default;
            }
        }

    }

    public static void Reset()
    {
        ClearNodeType(eType.Path);
        ClearNodeType(eType.Visited);
        
        GraphNode[] graphNodes = GetGraphNodes();
        foreach (GraphNode graphNode in graphNodes)
        {
            graphNode.Visited = false;
            graphNode.Parent = null;
            graphNode.Cost = float.MaxValue;
        }
    }
}
