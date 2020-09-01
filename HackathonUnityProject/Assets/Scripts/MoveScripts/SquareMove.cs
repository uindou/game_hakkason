using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMove : MonoBehaviour
{
    float startTime;
    float divTime;
    public bool Direction = true;
    public float width = 7;
    public float gap = 0;// 1で1/4周のずれ
    Vector3 originalPosition;
    Vector3 position;
    void Awake()
    {
        startTime = Time.time;
        originalPosition = transform.position;
    }

    void Update()
    {
        divTime = (Time.time - startTime + gap) % 4;
        position = originalPosition;
        if (Direction)
        {
            if (divTime < 1)
            {
                position.x += divTime * width;
            }
            else if (divTime < 2)
            {
                position.x += width;
                position.y += (divTime - 1) * width;
            }
            else if (divTime < 3)
            {
                position.y += width;
                position.x += width;
                position.x -= (divTime - 2) * width;
            }
            else
            {
                position.y += width;
                position.y -= (divTime - 3) * width;
            }
        }
        else
        {
            if (divTime < 1)
            {
                position.x -= divTime * width;
            }
            else if (divTime < 2)
            {
                position.x -= width;
                position.y += (divTime - 1) * width;
            }
            else if (divTime < 3)
            {
                position.y += width;
                position.x -= width;
                position.x += (divTime - 2) * width;
            }
            else
            {
                position.y += width;
                position.y -= (divTime - 3) * width;
            }
        }
        transform.position = position;
    }
}
