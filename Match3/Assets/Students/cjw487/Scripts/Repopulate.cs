using UnityEngine;

namespace Chrs
{
    public class Repopulate : RepopulateScript
    {
        public override void Start()
        {
            base.Start();
        }

        public override void AddNewTokensToRepopulateGrid()
        {
            for (int x = 0; x < gameManager.gridWidth; x++)
            {
                //  Gets a tokens at the top of the grid to fill in empty spaces
                GameObject token = gameManager.gridArray[x, gameManager.gridHeight - 1];
                //  This happens when a match has been made and tokens in the grid fall down
                if (token == null)
                {
                    //  Puts new tokens at the top based on x position, y position and the token's parent
                    gameManager.AddTokenToPosInGrid(x, gameManager.gridHeight - 1, gameManager.grid);
                }
            }
        }
    }
}