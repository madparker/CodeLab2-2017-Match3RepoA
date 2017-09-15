using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class MoveTokens : MoveTokensScript
    {
        public override void Start()
        {

            // Gets references to GameManagerScript and MatchManager Script.
            gameManager = GetComponent<GameManager>();
            matchManager = GetComponent<MatchManagerScript>();
            lerpPercent = 0;
        }

        public override void Update()
        {
            base.Update();
        }

        public override bool MoveTokensToFillEmptySpaces()
        {
            bool movedToken = false;

            for (int x = 0; x < gameManager.gridWidth; x++)
            { //Searches through the grid.
                for (int y = 1; y < gameManager.gridHeight; y++)
                {
                    if (gameManager.gridArray[x, y - 1] == null)
                    { //If the space below the space we're on is empty
                        for (int pos = y; pos < gameManager.gridHeight; pos++)
                        { // search the grid again to find all
                            GameObject token = gameManager.gridArray[x, pos]; // tokens above.
                            if (token != null)
                            { // if a token exists in that space
                                MoveTokenToEmptyPos(x, pos, x, pos - 1, token); //call MoveTokenToEmptyPos to move it down
                                movedToken = true;// and changed movedToken to true so that it calls so that it doesn't repopulate
                            }
                        }
                    }
                }
            }

            //Sets move to false when lerp is complete (lerpy stoppy).
            if (lerpPercent == 1)
            {
                move = false;
            }
            // Returns true if there is an empty space that needs to be repopulated.
            return movedToken;
        }
    }
}