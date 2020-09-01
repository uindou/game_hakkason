using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hackathon
{
    public class TitleManagerScript : MonoBehaviour
    {
        public AudioClip[] SEs;

        AudioSource audioSource;

        GameObject TitlePlayer;

        bool playOnce = true;

        // Start is called before the first frame update
        void Start()
        {
            TitlePlayer = GameObject.Find("TitleBear");

            audioSource = GetComponent<AudioSource>();

            GameManagerScript.questionNum = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if(TitlePlayer.GetComponent<bearController>().titleSceneChange&&playOnce)
            {
                SEPlay_SceneChange();

                playOnce = false;
            }
        }

        void SEPlay_SceneChange()
        {
            audioSource.PlayOneShot(SEs[0]);

            Invoke("SceneChange", 0.5f);
        }

        void SceneChange()
        {
            SceneManager.LoadScene("Stage_0");
        }
    }
}
