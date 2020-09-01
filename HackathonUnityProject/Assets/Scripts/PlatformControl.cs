using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class PlatformControl : MonoBehaviour
    {
        public Vector3 target;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                var bc = collision.gameObject.GetComponent<bearController>();
                bc.RespornPoint = target;
                bc.IsStartPoint = false;
            }
        }
    }
}