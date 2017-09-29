using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace joel
{
    public class TokenScript : MonoBehaviour
    {
        public bool isInHorizontalMatch =false;
        public bool isInVerticalMatch = false;
        public int horizontalMatchLength;
        public int verticalMatchLength;

        public bool onFire = false;

        private Animator animator;
        private ParticleSystem particleSystem;

        private bool isSelected = false;

        private int points;
        private int onFireBonus = 3000;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            particleSystem = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            //points = ((10 * (horizontalMatchLength - 1) * horizontalMatchLength) + (10 * (verticalMatchLength - 1) * verticalMatchLength));

            if (isSelected)
            {
                animator.Play("selected");
            }
            else
            {
                animator.Play("base");
            }

            if (onFire)
            {
                particleSystem.Play();
            }
            else if(onFire == false)
            {
                particleSystem.Pause();
            }

        }

        public void Select()
        {
            isSelected = true;
        }

        public void DeSelect()
        {
            isSelected = false;
        }

        public int PointsToAdd()
        {
            if (onFire)
            {
                Debug.Log("Bonus applied");
                return ((10 * (horizontalMatchLength - 1) * horizontalMatchLength) + (10 * (verticalMatchLength - 1) * verticalMatchLength)) + onFireBonus;
            }
            else
            {
                return ((10 * (horizontalMatchLength - 1) * horizontalMatchLength) + (10 * (verticalMatchLength - 1) * verticalMatchLength));
            }
        }
    }
}
