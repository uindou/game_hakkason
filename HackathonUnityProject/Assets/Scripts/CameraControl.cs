using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class CameraControl : MonoBehaviour
    {
        public Vector2 camMoveLim;//カメラの移動制限(x;左側、y:右側)
        Vector3 position;

        void Start()
        {
            position = Cache.player.transform.position;
            position.z = -10;
            transform.position = position;
        }

        void Update()
        {
            //playerのpositionを取得してカメラの座標をそれに合わせる
            position = Cache.player.transform.position;
            position.z = -10;  // これをしないとクマが映らない

            //指定された範囲内でカメラが移動する
            if (camMoveLim.x >= position.x)
            {
                position.x = camMoveLim.x;
            }
            else if(camMoveLim.y <= position.x)
            {
                position.x = camMoveLim.y;
            }
            transform.position = position;
        }
    }
}