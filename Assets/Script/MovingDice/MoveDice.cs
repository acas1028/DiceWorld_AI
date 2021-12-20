using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDice : MonoBehaviour
{
    public GameObject SuccessEffect;
    public GameObject FailEffect;
    public int speed = 300;
    bool isMoving = false;

    public int diceState;

    public bool isActivated = false;

    GameObject downQuad;
    TileManager tileManager;
    DiceGenerator diceGenerator;

    public int currentDicePosition;

    private void Start()
    {
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        diceGenerator = GameObject.Find("DiceGenerator").GetComponent<DiceGenerator>();
        diceState = 1;
    }
    void Update()
    {

        downQuad = GetComponent<DiceQuad>().DownQuad;

        if (isActivated) return;

        if (isMoving)
        {
            return;
        }

        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            if (currentDicePosition > tileManager.GetMapScale_X() * (tileManager.GetMapScale_Y() - 1) - 1) return;
            currentDicePosition += tileManager.GetMapScale_X();

            StartCoroutine(Roll(Vector3.right));
            diceState = StateChangeLR(diceState);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (currentDicePosition < tileManager.GetMapScale_X()) return;
            currentDicePosition -= tileManager.GetMapScale_X();

            StartCoroutine(Roll(Vector3.left));
            diceState = StateChangeLR(diceState);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (currentDicePosition % tileManager.GetMapScale_X() == 0) return;
            currentDicePosition -= 1;

            StartCoroutine(Roll(Vector3.back));
            diceState = StateChangeUD(diceState);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (currentDicePosition % tileManager.GetMapScale_X() == tileManager.GetMapScale_X() - 1) return;
            currentDicePosition += 1;

            StartCoroutine(Roll(Vector3.forward));
            diceState = StateChangeUD(diceState);
        }
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up,direction);



        while(remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        if (CheckMove())
        {
            GameObject effect = GameObject.Instantiate(SuccessEffect);
            effect.transform.position = new Vector3(transform.position.x, transform.position.y - 0.45f, transform.position.z);
            Destroy(effect, 1.0f);
        }
        else
        {
            GameObject effect = GameObject.Instantiate(FailEffect);
            effect.transform.position = transform.position;
            Destroy(effect, 1.0f);
            diceGenerator.CResetDice();
          
        }
        
        isMoving = false;
    }

    bool CheckMove()
    {
        Debug.Log(downQuad.GetComponent<MeshRenderer>().materials[0].color);
        Debug.Log(tileManager.TileList[currentDicePosition].GetComponent<MeshRenderer>().materials[0].color);

        if(currentDicePosition == tileManager.End_Point)
        {
            StageControl.Instance.ActiveUI();
        }

        if(tileManager.CheckFreeTile(currentDicePosition)) //프리 타일이면 그냥 통과 가능
        {
            return true;
        }

        if(downQuad.GetComponent<MeshRenderer>().materials[0].color 
            != tileManager.TileList[currentDicePosition].GetComponent<MeshRenderer>().materials[0].color)
        {
            return false;
        } // 색깔이 다르면 움직일 수 없다.

        return true;
    }

    int StateChangeLR(int currentState)
    {
        int value = 0;

        switch(currentState)
        {
            case 1:
                value = 3;
                break;
            case 2:
                value = 6;
                break;
            case 3:
                value = 1;
                break;
            case 4:
                value = 5;
                break;
            case 5:
                value = 4;
                break;
            case 6:
                value = 2;
                break;
        }

        return value;
    }

    int StateChangeUD(int currentState)
    {
        int value = 0;

        switch (currentState)
        {
            case 1:
                value = 5;
                break;
            case 2:
                value = 4;
                break;
            case 3:
                value = 6;
                break;
            case 4:
                value = 2;
                break;
            case 5:
                value = 1;
                break;
            case 6:
                value = 3;
                break;
        }

        return value;
    }

}
