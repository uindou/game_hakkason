using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Hackathon
{
    public class GameManagerScript : MonoBehaviour
    {
        public Animation QuestionNumAnim;
        public Animation QuestionAnim;

        TextMeshProUGUI TimeText,QuestionNumText,QuestionText;
        SpriteRenderer BackScreen;

        Color currentColor;

        string colorName;

        public static float time;
        public static int questionNum;
        public static int threshold = 15;

        GameObject trigger;

        // Start is called before the first frame update
        void Start()
        {
            if (SceneManager.GetActiveScene().name == "Stage_0")
            {
                questionNum = 1;
            }
            else if (SceneManager.GetActiveScene().name == "Stage_1")
            {
                questionNum = 2;
            }
            else if (SceneManager.GetActiveScene().name == "Stage_2")
            {
                questionNum = 3;
            }
            else if (SceneManager.GetActiveScene().name == "Stage_3")
            {
                questionNum = 4;
            }
            else if (SceneManager.GetActiveScene().name == "Stage_4")
            {
                questionNum = 5;
            }
            else if (SceneManager.GetActiveScene().name == "Stage_5")
            {
                questionNum = 6;
            }

                TimeText = Cache.Time.GetComponent<TextMeshProUGUI>();
            QuestionText= Cache.Question.GetComponent<TextMeshProUGUI>();
            QuestionNumText = Cache.QuestionNum.GetComponent<TextMeshProUGUI>();
            BackScreen = Cache.BackScreen.GetComponent<SpriteRenderer>();

            time = 0;
            var question = Cache.questions[Random.Range(0, Cache.questions.Length)];

            QuestionNumText.text = "第" + questionNum + "問";
            QuestionText.text = question[1];

            QuestionNumAnim.Play();
            Invoke("QuestionAnimPlay", 1.2f);

            ChangeColor_GetString(question);
            trigger = Resources.Load("Trigger") as GameObject;
            SetTrigger(question);
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;

            TimeText.text = "Time " + time.ToString("F2");

            if(Input.GetMouseButtonDown(0))
            {
                //ChangeColor_GetString();

                Debug.Log("色の名前:" + colorName);
            }
        }

        void ChangeColor_GetString(string[] question)
        {
            //色をランダムに決定

            //背景を変更
            if (int.Parse(question[0]) > threshold)
            {
                var keys = new string[Cache.colors.Keys.Count];
                Cache.colors.Keys.CopyTo(keys, 0);
                currentColor = Cache.colors[keys[Random.Range(0, keys.Length)]];
            }
            else
            {
                currentColor = Cache.colors[question[2 + int.Parse(question[2])]];
                colorName = question[2 + int.Parse(question[2])];
            }
            BackScreen.color = currentColor;
        }

        void SetTrigger(string[] question)
        {
            var goal = GameObject.Find("Goal");
            foreach(Transform child in goal.transform)
            {
                if (child.name.StartsWith("Selection", System.StringComparison.CurrentCulture))
                {
                    var index = int.Parse(child.name.Split('-')[1]);
                    var sentence = child.GetComponent<TextMeshProUGUI>();
                    sentence.text = question[2 + index];
                }
            }
            var gp = goal.transform.position;
            var dummy = new Vector3(0, 0, 0);
            for(int i = 1; i <= 3; i++)
            {
                var t = Instantiate(trigger, dummy, Quaternion.identity);
                t.tag = (i == int.Parse(question[2])) ? "Correct" : "Wrong";
                if (i == 1)
                {
                    var bc = t.GetComponent<BoxCollider2D>();
                    bc.size = new Vector2(5, 15);
                    bc.offset = new Vector2(0, 6);
                }
                t.transform.SetParent(goal.transform);
                t.transform.position = gp + new Vector3(10f + (3 - i) * 4.5f, (3 - i) * 8, 0);
            }
        }

        void QuestionAnimPlay()
        {
            QuestionAnim.Play();
        }

        public static float getScore()
        {
            return time;
        }
    }
}
