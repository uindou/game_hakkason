using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
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

    void SceneChange()
    {
        SceneManager.LoadScene("Title");
    }
}
