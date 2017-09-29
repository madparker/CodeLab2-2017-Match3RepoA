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

        private bool isSelected = false;

        private int points;
        private int onFireBonus = 3000;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
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
                return ((10 * (horizontalMatchLength - 1) * horizontalMatchLength) + (10 * (verticalMatchLength - 1) * verticalMatchLength)) + onFireBonus;
            }
            else
            {
                return ((10 * (horizontalMatchLength - 1) * horizontalMatchLength) + (10 * (verticalMatchLength - 1) * verticalMatchLength));
            }
        }
    }
}
