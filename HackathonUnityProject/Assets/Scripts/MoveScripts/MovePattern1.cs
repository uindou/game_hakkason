using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePattern1: MonoBehaviour
{
    public float gap = 0;
    public float width = 5;
    public float cycle = 1;
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
        float sin3 = Mathf.Pow(Mathf.Sin((Time.time - startTime + gap)*cycle), 3);
        float sin = Mathf.Sin((Time.time - startTime + gap)*cycle);
        position = originalPosition;
        position.x += sin3 * width;
        position.y += sin * width;
        transform.position = position;
    }
}
