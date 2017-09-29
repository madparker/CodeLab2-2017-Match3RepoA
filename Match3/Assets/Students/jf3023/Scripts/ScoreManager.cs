using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace joel
{
    public class ScoreManager : MonoBehaviour
    {
        private int score;
        public Text scoreboard;

        //Put a public wrapper around "score" called "Score" which allows you to alter the getter and setter of score
        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                scoreboard.text = "Score: " + score.ToString("0000000");
                //Debug.Log("score was set to: " + score.ToString("000000000"));

            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
