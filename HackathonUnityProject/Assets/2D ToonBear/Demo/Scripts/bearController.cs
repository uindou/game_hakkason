using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

//This Script is intended for demoing and testing animations only.

namespace Hackathon
{
    public class bearController : MonoBehaviour
    {
        private float HSpeed = 10f;
        public Vector3 RespornPoint;
        public bool IsStartPoint = true;
        //private float maxVertHSpeed = 20f;
        private bool facingRight = true;
        private float moveXInput;

        //Used for flipping Character Direction
        public static Vector3 theScale;

        //Jumping Stuff
        public Transform groundCheck;
        public LayerMask whatIsGround;
        private bool grounded = false;
        private float groundRadius = 0.5f;
        private float jumpForce = 24f;

        private Animator anim;

        //効果音用
        public AudioClip[] SEs;
        AudioSource audioSource;

        public Vector3 FirstPos;//初期位置

        public float mushJumpForce = 1.0f;
        public bool titleSceneChange = false;

        int dir;
        bool antigravity;
        bool trigger;       

        public static bool Death = false;

        // Use this for initialization
        void Awake()
        {
            var gravity = Physics2D.gravity;
            if (gravity.y > 0)
            {
                gravity.y *= -1;
                Physics2D.gravity = gravity;
            }

            //		startTime = Time.time;
            anim = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        void FixedUpdate()
        {

            dir = (antigravity) ? -1 : 1;
            trigger = (GetComponent<Rigidbody2D>().velocity.y * dir < 0) ? true : false;

            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            anim.SetBool("ground", grounded);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Sprint", true);
                HSpeed = 14f;
            }
            else
            {
                anim.SetBool("Sprint", false);
                HSpeed = 10f;
            }
        }

        void Update()
        {
            moveXInput = Input.GetAxis("Horizontal");

            if ((grounded) && Input.GetButtonDown("Jump"))
            {
                anim.SetBool("ground", false);

                audioSource.PlayOneShot(SEs[0]);

                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, jumpForce * dir);
            }

            anim.SetFloat("HSpeed", Mathf.Abs(moveXInput));
            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

            GetComponent<Rigidbody2D>().velocity = new Vector2((moveXInput * HSpeed), GetComponent<Rigidbody2D>().velocity.y);

            if (Input.GetButtonDown("Fire1") && (grounded))
            {
                audioSource.PlayOneShot(SEs[6]);

                anim.SetTrigger("Punch");
            }

            //Flipping direction character is facing based on players Input
            if (moveXInput > 0 && !facingRight)
                Flip();
            else if (moveXInput < 0 && facingRight)
                Flip();
        }
        ////Flipping direction of character
        void Flip()
        {
            facingRight = !facingRight;
            theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (trigger && collision.gameObject.tag == "Mushroom")//ジャンプ台
            {
                audioSource.PlayOneShot(SEs[1]);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, jumpForce * mushJumpForce * dir);
            }
            else if (trigger && collision.gameObject.tag == "Antigravity")//反重力
            {
                audioSource.PlayOneShot(SEs[2]);

                antigravity = !antigravity;

                var gravity = Physics2D.gravity;
                gravity.y *= -1;
                Physics2D.gravity = gravity;

                theScale = transform.localScale;
                theScale.y *= -1;
                transform.localScale = theScale;

                var force = new Vector2(0.0f, gravity.y);
                GetComponent<Rigidbody2D>().velocity = force;
            }
            else if (collision.gameObject.tag == "MoveWall")//移動する床
            {
                transform.SetParent(collision.transform);
                Death = false;
            }
            else if (trigger && collision.gameObject.tag == "Warp")//移動する床
            {
                audioSource.PlayOneShot(SEs[5]);
                transform.position = collision.gameObject.GetComponent<Warp>().target;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "MoveWall")//移動する床解除
            {
                transform.SetParent(null);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!grounded && ((Input.GetKey("left") || Input.GetKey("right")) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))))
            {
                HSpeed = 0f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "DestroyLine")//ある程度落下したらスタートへ
            {
                Death = true;
                if (antigravity)
                {
                    antigravity = !antigravity;

                    var gravity = Physics2D.gravity;
                    gravity.y *= -1;
                    Physics2D.gravity = gravity;

                    theScale = transform.localScale;
                    theScale.y *= -1;
                    transform.localScale = theScale;

                    var force = new Vector2(0.0f, 0.0f);
                    GetComponent<Rigidbody2D>().velocity = force;
                }

                transform.position = (IsStartPoint) ? FirstPos : RespornPoint;
                audioSource.PlayOneShot(SEs[3]);
            }
            else if (collision.gameObject.tag == "StartButton" && anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
            {
                titleSceneChange = true;
            }
            else if (collision.gameObject.tag == "Correct")
            {
                if (GameManagerScript.questionNum < 6)
                {
                    SceneManager.LoadScene("Result");
                }
                else
                {
                    SceneManager.LoadScene("Clear");
                }
            }
            else if (collision.gameObject.tag == "Wrong")
            {
                transform.position = FirstPos;
                Death = true;
                audioSource.PlayOneShot(SEs[4]);
            }
        }
    }
}