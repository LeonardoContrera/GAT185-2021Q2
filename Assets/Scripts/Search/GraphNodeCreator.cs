using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNodeCreator : MonoBehaviour
{
    public GraphNode graphNode;
    public LayerMask layerMask;
    public float range = 1;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitinfo, 100, layerMask))
            {
                GraphNode node = Instantiate(graphNode, hitinfo.point, Quaternion.identity);
                GraphNode.UnlinkNodes();
                GraphNode.LinkNodes(range);
            }
        }
        
    }

}
