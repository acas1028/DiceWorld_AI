using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode : MonoBehaviour
{
    public bool isWalkable;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public ANode parentNode;

    public int DiceState;

    public GameObject hint;

    public ANode(bool nWalkable,int nGridX,int nGridY)
    {
        isWalkable = nWalkable;
        gridX = nGridX;
        gridY = nGridY;
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingNode(bool nWalkable, int ngridX, int ngridY)
    {
        isWalkable = nWalkable;
        gridX = ngridX;
        gridY = ngridY;
    }

    public int Debuging()
    {
        return GetComponent<TileNumberSetting>().GetTileNumber();
    }

    public void ChangeDiceStateLR(int currentState)
    {
        switch(currentState)
        {
            case 1:
                DiceState = 3;
                break;
            case 2:
                DiceState = 6;
                break;
            case 3:
                DiceState = 1;
                break;
            case 4:
                DiceState = 5;
                break;
            case 5:
                DiceState = 4;
                break;
            case 6:
                DiceState = 2;
                break;
        }
    }

    public void ChangeDiceStateUD(int currentState)
    {
        switch (currentState)
        {
            case 1:
                DiceState = 5;
                break;
            case 2:
                DiceState = 4;
                break;
            case 3:
                DiceState = 6;
                break;
            case 4:
                DiceState = 2;
                break;
            case 5:
                DiceState = 1;
                break;
            case 6:
                DiceState = 3;
                break;
        }
    }

    public bool CheckCanMove()
    {
        if (GetComponent<TileStatus>().isTileFree) return true;

        switch(DiceState)
        {
            case 1:
                if (!GetComponent<TileStatus>().isGreen) return false;
                break;
            case 2:
                if (!GetComponent<TileStatus>().isGreen) return false;
                break;
            case 3:
                if (!GetComponent<TileStatus>().isBlue) return false;
                break;
            case 4:
                if (!GetComponent<TileStatus>().isBlue) return false;
                break;
            case 5:
                if (!GetComponent<TileStatus>().isRed) return false;
                break;
            case 6:
                if (!GetComponent<TileStatus>().isRed) return false;
                break;
        }
        return true;
    }
}
