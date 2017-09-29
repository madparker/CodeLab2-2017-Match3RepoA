using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace joel
{
    public class ScoreManager : MonoBehaviour
    {
        private int score;

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

                Debug.Log("score was set to: " + score.ToString("000000000"));

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
