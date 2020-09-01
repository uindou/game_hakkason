using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Hackathon
{
    public class NextStage : MonoBehaviour
    {
        public string[] Stage;

        public AudioClip[] SEs;

        AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void SEPlay_SceneChange()
        {
            audioSource.PlayOneShot(SEs[0]);

            Invoke("SceneChange", 0.5f);
        }

        public void SceneChange()
        {
            if (GameManagerScript.questionNum - 1 > Stage.Length )
            {
                SceneManager.LoadScene("Clear");
            }

            SceneManager.LoadScene(Stage[GameManagerScript.questionNum - 2]);
        }
    }
}