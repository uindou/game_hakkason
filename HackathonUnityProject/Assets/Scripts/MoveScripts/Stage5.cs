using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hackathon
{
    public class Stage5 : MonoBehaviour
    {
        public float gap = 0;
        public float width = 5;
        public float ySpeed = 1;
        float Change1 = 5;
        float Change2 = 14;
        float Change3 = 23;
        float Change4 = 33;
        bool isStart = false;
        bool firstContact = true;
        public float speed = 1;
        float startTime = 0;
        Vector3 originalPosition;
        Vector3 position;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (firstContact)
            {
                if (collision.gameObject.tag == "Player")
                {
                    startTime = Time.time;
                    isStart = true;
                    firstContact = false;
                }
            }


        }
        void Awake()
        {
            originalPosition = transform.position;
        }
        // Update is called once per frame
        void Update()
        {
            float time = isStart ? Time.time - startTime - gap : 0;
            float sin = Mathf.Sin(time);
            float place1 = Mathf.Sin(Change2 - Change1) * width + Change1 * speed;
            position = originalPosition;
            if (bearController.Death)
            {
                isStart = false;
                firstContact = true;
            }
            else if (time < Change1 && time >= 0)
            {
                position.x += time * speed;
            }
            else if (time < Change2)
            {
                position.x += Change1 * speed + Mathf.Sin(time - Change1) * width;
                position.y += (time - Change1) * ySpeed;
            }
            else if (time < Change3)
            {
                position.x += place1 + (time - Change2) * speed;
                position.y += (Change2 - Change1) * ySpeed + Mathf.Sin(time - Change2) * width;

            }
            else if (time < Change4)
            {
                position.x += place1 + (time - Change2) * speed;
                position.y += (-time + Change3 + Change2 - Change1) * ySpeed + Mathf.Sin(Change3 - Change2) * width;
            }
            else
            {
                position.x += place1 + (Change4 - Change2) * speed;
                position.y += (-Change4 + Change3 + Change2 - Change1) * ySpeed + Mathf.Sin(Change3 - Change2) * width;
            }
            transform.position = position;
        }
    }
}