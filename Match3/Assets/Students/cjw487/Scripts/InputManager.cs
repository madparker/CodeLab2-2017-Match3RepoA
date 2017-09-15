using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class InputManager : InputManagerScript
    {
        [SerializeField] protected int MAX_DISTANCE_YOU_CAN_SWAP = 1;
        protected const int LEFT_MOUSE_BUTTON = 0;

        /// <summary>
        /// Uses cursor input to select tokens on the grid.
        /// </summary>
        public override void SelectToken()
        {
            //  On mouse left button clikced to select a token
            if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
            {
                //  Convert mouse position to world point (in position)
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //  Get the collider of the mouse clicked point (postion)
                Collider2D collider = Physics2D.OverlapPoint(mousePos);

                //  Check if there any token
                if (collider != null)
                {
                    //  If there is a token, get the token's game object
                    if (selected == null)
                    {
                        //  If the first selected token is empty, "selected" stores the current tokan game object
                        selected = collider.gameObject;
                    }
                    else
                    {
                        //  The first selected token position
                        Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
                        //  The current selected token positon
                        Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

                        //  What caused the error:
                        //          Before we were using ONLY the distance formula to determine if a switch was valid.
                        //          The problem with this is diagonals have a distance as well.
                        //
                        //  Solution:
                        //          I added two extra boolean check to see if the token to be switched is
                        //          either in the same row OR coloumn, then I check the distance using the formula.
                        if ((pos1.x == pos2.x || pos1.y == pos2.y) && Mathf.Abs((pos1.x - pos2.x) + (pos1.y - pos2.y)) <= MAX_DISTANCE_YOU_CAN_SWAP)
                        {
                            //  If the distance is in the same row or coloumn 1 and if the token is either,
                            //  then do the position swap
                            moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
                        }

                        //  Clear the "selected" to null;
                        selected = null;
                    }
                }
            }
        }
    }
}