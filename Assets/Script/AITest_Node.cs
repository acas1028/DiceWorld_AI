using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest_Node : MonoBehaviour
{
    public TileManager tileManager;

    public GameObject pathFinding;

    public GameObject[,] tileList;
    public ANode[,] nodeList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SettingAITile()
    {
        tileList = new GameObject[tileManager.GetMapScale_X(), tileManager.GetMapScale_Y()];
        nodeList = new ANode[tileManager.GetMapScale_X(), tileManager.GetMapScale_Y()];

        for (int i = 0; i < tileManager.TileList.Count; i++)
        {
            int x = i % tileManager.GetMapScale_X();
            int y = i / tileManager.GetMapScale_X();

            tileList[x, y] = tileManager.TileList[i];
            Debug.Log(tileList[x, y].GetComponent<MeshRenderer>().materials[0].color);
            nodeList[x, y] = tileManager.TileList[i].GetComponent<ANode>();
            nodeList[x, y].SettingNode(true, x, y);
        }

        pathFinding.SetActive(true);
    }

    public List<ANode> GetNeighbours(ANode node)
    {
        List<ANode> neighbours = new List<ANode>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                if (x == -1 && y == 1) continue;
                if (x == -1 && y == -1) continue;
                if (x == 1 && y == 1) continue;
                if (x == 1 && y == -1) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < tileManager.GetMapScale_X() && checkY >= 0 && checkY < tileManager.GetMapScale_Y())
                {
                    if(x == 1 || x == -1)
                    {
                        nodeList[checkX, checkY].ChangeDiceStateUD(node.DiceState);
                    }

                    if(y == 1 || y == -1)
                    {
                        nodeList[checkX, checkY].ChangeDiceStateLR(node.DiceState);
                    }

                    if (!nodeList[checkX, checkY].CheckCanMove()) nodeList[checkX, checkY].isWalkable = false;
                    if (nodeList[checkX, checkY].CheckCanMove()) nodeList[checkX, checkY].isWalkable = true;
                    neighbours.Add(nodeList[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public ANode FindStartNode()
    {
        int x = tileManager.Start_Point % tileManager.GetMapScale_X();
        int y = tileManager.Start_Point / tileManager.GetMapScale_X();

        return nodeList[x,y];
    }

    public ANode FindEndNode()
    {
        int x = tileManager.End_Point % tileManager.GetMapScale_X();
        int y = tileManager.End_Point / tileManager.GetMapScale_X();

        return nodeList[x, y];
    }
}
