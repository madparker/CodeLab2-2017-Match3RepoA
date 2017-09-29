using UnityEngine;

namespace Chrs
{
    public class MoveTokens : MoveTokensScript
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ExchangeTokens()
        {
            base.ExchangeTokens();
        }

        public override void MoveTokenToEmptyPos(int startGridX, int startGridY,
                                                 int endGridX, int endGridY,
                                                 GameObject token)
        {
            //Sets start pos as the token pos and the end pos as the empty space on the grid.
            Vector3 startPos = Services.GameManager.GetWorldPositionFromGridPosition(startGridX, startGridY);
            Vector3 endPos = Services.GameManager.GetWorldPositionFromGridPosition(endGridX, endGridY);

            //Sets the lerp for the token (lerpy tokey)
            Vector3 pos = Vector3.Lerp(startPos, endPos, lerpPercent);

            //Moves the token (movey tokey)
            token.transform.position = pos;

            //Set the token's position in the grid to its new spot.  Makes the spot that it moved from null. (spotty setty)
            if (lerpPercent == 1)
            {
                Services.GameManager.gridArray[endGridX, endGridY] = token;
                Services.GameManager.gridArray[startGridX, startGridY] = null;
            }
        }

        public override bool MoveTokensToFillEmptySpaces()
        {
            bool movedToken = false;

            for (int x = 0; x < Services.GameManager.gridWidth; x++)
            { //Searches through the grid.
                for (int y = 1; y < Services.GameManager.gridHeight; y++)
                {
                    if (Services.GameManager.gridArray[x, y - 1] == null)
                    { //If the space below the space we're on is empty
                        for (int pos = y; pos < Services.GameManager.gridHeight; pos++)
                        { // search the grid again to find all
                            GameObject token = Services.GameManager.gridArray[x, pos]; // tokens above.
                            if (token != null)
                            { // if a token exists in that space
                                MoveTokenToEmptyPos(x, pos, x, pos - 1, token); //call MoveTokenToEmptyPos to move it down
                                movedToken = true;// and changed movedToken to true so that it calls so that it doesn't repopulate
                            }
                        }
                        break;
                    }
                }
            }

            //Sets move to false when lerp is complete (lerpy stoppy).
            if (lerpPercent == 1)
            {
                move = false;
                movedToken = false;
            }
            // Returns true if there is an empty space that needs to be repopulated.
            return movedToken;
        }
    }
}