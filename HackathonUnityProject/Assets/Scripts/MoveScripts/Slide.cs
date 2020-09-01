using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class Slide : MonoBehaviour
    {
        float startTime;
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
            float sin = Mathf.Sin(Time.time - startTime);
            position = originalPosition;
            if (trigger)
            {
                position.x += sin * 5;
            }
            else
            {
                position.y += sin * 5;
            }
            transform.position = position;
        }
    }
}