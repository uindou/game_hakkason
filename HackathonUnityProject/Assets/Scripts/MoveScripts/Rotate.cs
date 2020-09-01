using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float width = 5;
    public float gap = 0;
    public bool Direction = true;//trueなら反時計回り、falseなら時計回り
    float startTime;
    Vector3 originalPosition;
    Vector3 position;

    void Awake()
    {
        startTime = Time.time;
        originalPosition = transform.position;
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time - startTime + gap);
        float cos = Mathf.Cos(Time.time - startTime + gap);
        position = originalPosition;
        if (Direction)
        {
            position.x += cos * width;
            position.y += sin * width;
        }
        else
        {
            position.x += sin * width;
            position.y += cos * width;
        }
        transform.position = position;
    }

}