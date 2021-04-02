using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointPosition : MonoBehaviour
{
    public Vector2 positionN;
    public Vector2 positionNW;
    public Vector2 positionW;
    public Vector2 positionSW;
    public Vector2 positionS;
    public Vector2 positionSE;
    public Vector2 positionE;
    public Vector2 positionNE;

    public void SetCurrentPosition(Vector2 position)
    {
        float x = CompareNumber(position.x);
        float y = CompareNumber(position.y);
        if (x == 0 && y == 1) position = positionN;
        else if (x == 1 && y == 1) position = positionNE;
        else if (x == 1 && y == 0) position = positionE;
        else if (x == 1 && y == -1) position = positionSE;
        else if (x == 0 && y == -1) position = positionS;
        else if (x == -1 && y == -1) position = positionSW;
        else if (x == -1 && y == 0) position = positionW;
        else if (x == -1 && y == 1) position = positionNW;
        transform.localPosition = new Vector3(position.x, position.y, transform.position.z);
    }

    private int CompareNumber(float number)
    {
        if (number > 0) return 1;
        else if (number < 0) return -1;
        else return 0;
    }
}
