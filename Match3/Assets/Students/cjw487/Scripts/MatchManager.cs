using UnityEngine;

namespace Chrs
{
    public class MatchManager : MatchManagerScript
    {

        public override void Start()
        {
            base.Start();
        }
        /// <summary>
        /// Scans the grid for matches both horizontally and veritcally.
        /// </summary>
        /// <returns>True if 3 tokens have been matched</returns>
        public override bool GridHasMatch()
        {
            bool hasMatch = false;
            for (int x = 0; x < Services.GameManager.gridWidth; x++)
            {
                for (int y = 0; y < Services.GameManager.gridHeight; y++)
                {
                    if (x < Services.GameManager.gridWidth - 2)
                    {
                        //  Check for matches for horizontal matches
                        hasMatch = hasMatch || GridHasHorizontalMatch(x, y);
                    }

                    //  Check verical matches
                    if (y < Services.GameManager.gridHeight - 2)
                    {
                        hasMatch = hasMatch || GridHasVerticalMatch(x, y);
                    }
                }
            }
            return hasMatch;
        }

        public override bool GridHasHorizontalMatch(int x, int y)
        {
            GameObject token1 = Services.GameManager.gridArray[x + 0, y];
            GameObject token2 = Services.GameManager.gridArray[x + 1, y];
            GameObject token3 = Services.GameManager.gridArray[x + 2, y];

            //Check the token sprite exist;
            if (token1 != null && token2 != null && token3 != null)
            {
                //Get "Sprite Renderer" from each token;
                SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
                SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
                SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

                //Check are these 3 sprite (token using this sprite) are matching;
                return (sr1.color == sr2.color && sr2.color == sr3.color);
            }
            else
            {
                //if not, return false;
                return false;
            }
        }

        /// <summary>
        /// Scans the coloumns for veritcal matches using the Transistive property.
        /// </summary>
        /// <param name="x">X position of the token to be compared</param>
        /// <param name="y">Y position of the token to be compared</param>
        /// <returns>True if 3 tokens has the same sprite</returns>
        public bool GridHasVerticalMatch(int x, int y)
        {

            GameObject token1 = Services.GameManager.gridArray[x, y + 0];
            GameObject token2 = Services.GameManager.gridArray[x, y + 1];
            GameObject token3 = Services.GameManager.gridArray[x, y + 2];

            //Check the token sprite exist;
            if (token1 != null && token2 != null && token3 != null)
            {
                //Get "Sprite Renderer" from each token;
                SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
                SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
                SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

                //Check are these 3 sprite (token using this sprite) are matching;
                return (sr1.color == sr2.color && sr2.color == sr3.color);
            }
            else
            {
                //if not, return false;
                return false;
            }
        }

        /// <summary>
        /// Gives the total amount of tokens have the smae sprite in a contiguous.
        /// </summary>
        /// <param name="x">X position of the token to be compared</param>
        /// <param name="y">Y position of the token to be compared</param>
        /// <returns>The number of tokens in a line with the same sprite</returns>
        public int GetVerticalMatchLength(int x, int y)
        {
            //  Stores the lenght of the matching tokens
            int matchLength = 1;

            //  Stores the first element of the match
            GameObject first = Services.GameManager.gridArray[x, y];

            if (first != null)
            {
                SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
                
                for (int i = y + 1; i < Services.GameManager.gridWidth; i++)
                {
                    //  Gets other token to compare the first token to
                    GameObject other = Services.GameManager.gridArray[x, i];

                    //  If the other token is not null, do the comparison
                    if (other != null)
                    {
                        SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

                        //  Compares sprite images of the tokens
                        if (sr1.sprite == sr2.sprite)
                        {
                            //  Increase matchLength if the sprites are the same
                            matchLength++;
                        }
                        else
                        {
                            //  Stop the count when you encounter a differect sprite
                            break;
                        }
                    }
                    else
                    {
                        //  Exit for loop if the other token is null
                        break;
                    }
                }
            }
            return matchLength;
        }

        /// <summary>
        /// Removes macthed tokens from the grid.
        /// </summary>
        /// <returns>Number of tokens to be removed</returns>
        public override int RemoveMatches()
        {
            int numRemoved = 0;

            //  Go through all the grid to check the matched tokens
            for (int x = 0; x < Services.GameManager.gridWidth; x++)
            {
                for (int y = 0; y < Services.GameManager.gridHeight; y++)
                {
                    
                    int verticalMatchLength = GetVerticalMatchLength(x, y);

                    //  If we have matched more than 2 tokens remove them from
                    //  the grid
                    if (verticalMatchLength > 2)
                    {
                        for (int i = y; i < y + verticalMatchLength; i++)
                        {
                            GameObject token = Services.GameManager.gridArray[x, i];
                            Destroy(token);
                            Services.GameManager.gridArray[x, i] = null;

                            numRemoved++;
                        }
                    }

                    //  Discard 2 collums from the right most
                    if (x < Services.GameManager.gridWidth - 2)
                    {
                        //  Call "GetHorizontalMatchLength(int x, int y)";
                        //  Get the lenth of matched tokens;
                        int horizonMatchLength = GetHorizontalMatchLength(x, y);
                        
                        //  If at least 3 tokens are matching, get the number of tokens which will be removed;
                        if (horizonMatchLength > 2)
                        {
                            //  Destroy matched token game objects from the grid and get the number of removed tokens;
                            for (int i = x; i < x + horizonMatchLength; i++)
                            {
                                //  Get the game object of token that will be removed;
                                GameObject token = Services.GameManager.gridArray[i, y];

                                //  Destory the game ojbect;
                                Destroy(token);

                                //  Reset the grid to null;
                                Services.GameManager.gridArray[i, y] = null;

                                //  Increase the number of removed tokens;
                                numRemoved++;
                            }
                        }
                    }
                }
            }

            //Return the number of tokens which will be removed;
            return numRemoved;
        }
    }
}