using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePattern2 : MonoBehaviour
{
    public float width = 5;
    public float cycle = 1;
    float startTime;
    Vector3 originalPosition;
    Vector3 position;
    public float ToCycle(float t)
    {
        float Cy = Mathf.PI * cycle;
        t %= Cy;
        if (t < Cy / 2)
        {
            return t;
        }
        else
        {
            return Cy - t;
        }
    }
    void Awake()
    {
        startTime = Time.time;
        originalPosition = transform.position;
    }
    void Update()
    {
        position = originalPosition;
        position.y += Mathf.Pow(ToCycle(Time.time),2) * width;
        position.x += ToCycle(Time.time) * width;
        transform.position = position;
    }
}
