using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanameSlide : MonoBehaviour
{
    float startTime;
    public float width = 3;
    public float gap = 0;
    Vector3 originalPosition;
    Vector3 position;
    public bool trigger;  // trueなら横移動、falseなら縦移動

    void Awake()
    {
        startTime = Time.time;
        originalPosition = transform.position;
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time - startTime - gap);
        position = originalPosition;
            position.x += sin * width;
            position.y -= sin * width;
       transform.position = position;
    }
}