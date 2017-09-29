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
            for (int x = 0; x < Services.GameManager.gridWidth; x++)
            {
                //  Gets a tokens at the top of the grid to fill in empty spaces
                GameObject token = Services.GameManager.gridArray[x, Services.GameManager.gridHeight - 1];
                //  This happens when a match has been made and tokens in the grid fall down
                if (token == null)
                {
                    //  Puts new tokens at the top based on x position, y position and the token's parent
                    ((GameManager)Services.GameManager).AddTokenToPosInGrid_cjw(   x, 
                                                              Services.GameManager.gridHeight - 1, 
                                                                Services.GameManager.grid);
                }
            }
        }
    }
}