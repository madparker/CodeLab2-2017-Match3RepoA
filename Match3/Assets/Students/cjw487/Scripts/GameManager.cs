namespace Chrs
{
    public class GameManager : GameManagerScript
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            if (!GridHasEmpty())
            {
                if (matchManager.GridHasMatch())
                {
                    matchManager.RemoveMatches();
                }
                else
                {
                    inputManager.SelectToken();
                }
            }
            else
            {
                if (!moveTokenManager.move)
                {
                    moveTokenManager.SetupTokenMove();
                }
                if (!moveTokenManager.MoveTokensToFillEmptySpaces())
                {
                    repopulateManager.AddNewTokensToRepopulateGrid();
                }
            }
        }

        public override bool GridHasEmpty()
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    if (gridArray[x, y] == null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}