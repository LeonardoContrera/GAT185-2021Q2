using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Priority_Queue;

public static class SearchDijkstra
{
    public static bool Search(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
		bool found = false;

		//dijkstra algorithm
		SimplePriorityQueue<GraphNode> nodes = new SimplePriorityQueue<GraphNode>();
		source.Cost = 0;
		nodes.Enqueue(source, source.Cost);

		// set the current number of steps
		int steps = 0;
		while (!found && nodes.Count > 0 && steps++ < maxSteps)
		{
			// <dequeue node>
			GraphNode node = nodes.Dequeue();
			if (node == destination)
	{
				// <set found to true>
				found = true;
				// continue, do not execute the rest of this loop
				continue;
			}

			// search node edges for unvisited node
			foreach (GraphNode.Edge edge in node.edges)
			{
				// calculate cost to nodeB = node cost + edge distance (nodeA to nodeB)
				float cost = node.Cost + Vector3.Distance(edge.nodeA.transform.position, edge.nodeB.transform.position);/*< Vector3.Distance nodeA <->nodeB >*/
				// if cost < nodeB cost, add to priority queue
				if (/*< cost is less than nodeB.cost >*/cost < edge.nodeB.Cost)
		{
					// <set nodeB cost to cost>
					edge.nodeB.Cost = cost;
					// <set nodeB parent to node>
					edge.nodeB.Parent = node;
					// <enqueue without duplicates nodeB with cost as priority>
					nodes.EnqueueWithoutDuplicates(edge.nodeB, edge.nodeB.Cost);
				}
			}
		}

		// create a list of graph nodes (path)
		path = new List<GraphNode>();
		// if found is true
		if (found)
		{
			GraphNode node = destination;//< set node to destination >
		    // while node not null
			while (node != null)
			{
				// <add node to path list>
				path.Add(node);
				// <set node to node.Parent>
				node = node.Parent;
			}
			// reverse path
			path.Reverse();
		}
		else
		{
			// add all nodes to path
			//path.Add(nodes);
			foreach(GraphNode node in nodes)
            {
				path.Add(node);
            }
			while (nodes.Count > 0)
			{
				// <add (dequeued node) to path>
				path.Add(nodes.Dequeue());
			}
		}

		return found;


	}
}
