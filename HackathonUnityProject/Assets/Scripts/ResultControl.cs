using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Hackathon
{
    public class ResultControl : MonoBehaviour
    {
        static float totalScore = 0;
        static TextMeshProUGUI textmesh;
        static TextMeshProUGUI clearTextmesh;
        void Start()
        {
            textmesh = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            clearTextmesh = GameObject.Find("clear").GetComponent<TextMeshProUGUI>();

            if (GameManagerScript.questionNum < 6)
            {
                float score = GameManagerScript.getScore();
                textmesh.text = "ClearTime: " + score.ToString("F2");
                clearTextmesh.text = "Stage " + GameManagerScript.questionNum.ToString() + " Clear!";
                totalScore += score;
            }
            else
            {
                textmesh.text = "TotalTime: " + totalScore.ToString("F2");
            }
            GameManagerScript.questionNum++;
        }
    }
}