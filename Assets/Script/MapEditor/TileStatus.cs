using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStatus : MonoBehaviour
{
    public GameObject Flag;

    public bool isTileFree;
    public bool isTileGoal;

    public bool isRed;
    public bool isBlue;
    public bool isGreen;
    // Start is called before the first frame update
    void Start()
    {
        isTileFree = false;
        isTileGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        Color color = GetComponent<MeshRenderer>().materials[0].color;
        if(color == TileColor.tileColorRed)
        {
            isRed = true;
            isBlue = false;
            isGreen = false;
        }

        if(color == TileColor.tileColorBlue)
        {
            isRed = false;
            isBlue = true;
            isGreen = false;
        }

        if(color == TileColor.tileColorGreen)
        {
            isRed = false;
            isBlue = false;
            isGreen = true;
        }

    }

    public void SettingFlag()
    {
        GameObject Dummy = GameObject.FindWithTag("Flag");

        if (Dummy != null) Destroy(Dummy);

        Debug.Log("ÇÃ·¡±× ¸¸µë");
        GameObject flag = Instantiate(Flag);
        flag.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
    }
}
