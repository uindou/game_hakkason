using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtMoving : MonoBehaviour
{
    public float width = 1;
    public float cycle = 28;
    float startTime;
    Vector3 originalPosition;
    Vector3 position;
    void Awake()
    {
        startTime = Time.time;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float time = (Time.time - startTime) % cycle;
        position = originalPosition;
        position.x += time * width;
        transform.position = position;
    }
}
