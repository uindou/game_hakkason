using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

namespace Hackathon
{
    // ゲームに必要なデータ(Playerオブジェクトなど)の参照先をキャッシュしておくクラス
    public class Cache : MonoBehaviour
    {
        public static GameObject player;
        public static GameObject Time;//時間表示用
        public static GameObject BackScreen;//背景用
        public static GameObject QuestionNum;//問題表示用
        public static GameObject Question;
        public static Dictionary<string, Color> colors;//カラーデータ
        public static string[][] questions;

        void Awake()
        {
            player = GameObject.FindWithTag("Player");
            Time = GameObject.Find("Time");
            BackScreen = GameObject.Find("BackScreen");
            QuestionNum = GameObject.Find("QuestionNum");
            Question = GameObject.Find("Question");

            var cr = new CsvReader();
            var data = cr.ReadFile("color");  // List<string[]>
            colors = new Dictionary<string, Color>();
            foreach(var s in data)
            {
                colors[s[0]] = s[1].ToColor();
            }
            questions = cr.ReadFile("question").ToArray();
        }
    }
}