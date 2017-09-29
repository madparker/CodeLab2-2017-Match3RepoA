using UnityEngine;

namespace Chrs
{
    public class GameManager : GameManagerScript
    {
        public override void Start()
        {
            //Random.InitState(System.DateTime.Now.DayOfYear);
            gridArray = new GameObject[gridWidth, gridHeight];
            
            matchManager = GetComponent<MatchManagerScript>();
            
            inputManager = GetComponent<InputManagerScript>();
            repopulateManager = GetComponent<RepopulateScript>();
            moveTokenManager = GetComponent<MoveTokensScript>();
            MakeGrid_cjw();
        }

        protected void MakeGrid_cjw()
        {
            grid = new GameObject("TokenGrid");
            //  Makes grid based on width and height variables
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    //  Fills in the grid with tokens
                    AddTokenToPosInGrid_cjw(x, y, grid);
                }
            }
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

                if (matchManager.GridHasMatch())
                {
                    matchManager.RemoveMatches();
                }
                else
                {
                    inputManager.SelectToken();
                }

                if (!moveTokenManager.move)
                {
                    moveTokenManager.SetupTokenMove();
                }
                if(!moveTokenManager.MoveTokensToFillEmptySpaces())
                {

                }

            }
        }

        bool test;

        public void AddTokenToPosInGrid_cjw(int x, int y, GameObject parent)
        {
            Vector3 position = GetWorldPositionFromGridPosition(x, y);

            int tokenIndex = Random.Range(0, Services.Prefabs.Token.Length);
            //  Gets random token from the tokenType array
            GameObject token =
                Instantiate(Services.Prefabs.Token[tokenIndex],
                            position,
                            Quaternion.identity) as GameObject;
            token.name =  token.name.Replace("(Clone)", "");
            token.transform.parent = parent.transform;
            gridArray[x, y] = token;

            test = false;
            if (test)
            {
                

                if (x > 1 && ((MatchManager)matchManager).GridHasHorizontalMatch(x - 2, y))
                {
                    tokenIndex = (tokenIndex + 1) % Services.Prefabs.Token.Length;
                    token.GetComponent<Token>().SetToken(tokenIndex);                    
                }

                if (y > 1 && ((MatchManager)matchManager).GridHasVerticalMatch(x, y - 2))
                {
                    tokenIndex = (tokenIndex + 1) % Services.Prefabs.Token.Length;
                    token.GetComponent<Token>().SetToken(tokenIndex);
                }

                if (x > 1 && ((MatchManager)matchManager).GridHasHorizontalMatch(x - 2, y))
                {
                    tokenIndex = (tokenIndex + 1) % Services.Prefabs.Token.Length;
                    token.GetComponent<Token>().SetToken(tokenIndex);
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