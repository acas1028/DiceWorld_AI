using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public GameObject AI;
    public AITest_Node grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = AI.GetComponent<AITest_Node>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPath();
    }

    void FindPath()
    {
        ANode startNode = grid.FindStartNode();
        ANode endNode = grid.FindEndNode();

        List<ANode> openList = new List<ANode>();
        HashSet<ANode> closedList = new HashSet<ANode>();

        startNode.DiceState = 1;
        openList.Add(startNode);

        while(openList.Count > 0)
        {
            ANode currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost <currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == endNode)
            {
                // ³¡
                RetracePath(startNode,endNode);
                return;
            }

            foreach(ANode n in grid.GetNeighbours(currentNode))
            {
                if (!n.isWalkable || closedList.Contains(n))
                    continue;

                int newCurrentToNeighbourCost = currentNode.gCost + GetDistanceCost(currentNode,n);
                if(newCurrentToNeighbourCost < n.gCost || !openList.Contains(n))
                {
                    n.gCost = newCurrentToNeighbourCost;
                    n.hCost = GetDistanceCost(n, endNode);
                    n.parentNode = currentNode;

                    if (!openList.Contains(n))
                        openList.Add(n);
                }
            }
        }
    }

    void RetracePath(ANode startNode, ANode endNode)
    {
        List<ANode> path = new List<ANode>();
        ANode currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();
        for(int i = 0; i < path.Count; i++)
        {
            Debug.Log(path[i].Debuging());
        }
        for(int i = 0; i < path.Count; i++)
        {
            if(!path[i].hint.activeSelf)
                path[i].hint.SetActive(true);
        }
    }

    int GetDistanceCost(ANode nodeA, ANode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return distX + distY;
    }
}
