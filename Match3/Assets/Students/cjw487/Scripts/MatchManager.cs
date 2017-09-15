using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public class MatchManager : MatchManagerScript
    {
        
        /// <summary>
        /// Scans the grid for matches both horizontally and veritcally.
        /// </summary>
        /// <returns>True if 3 tokens have been matched</returns>
        public override bool GridHasMatch()
        {
            bool hasMatch = false;
            for (int x = 0; x < gameManager.gridWidth; x++)
            {
                for (int y = 0; y < gameManager.gridHeight; y++)
                {
                    if (x < gameManager.gridWidth - 2)
                    {
                        //  Check for matches for horizontal matches
                        hasMatch = hasMatch || GridHasHorizontalMatch(x, y);
                    }

                    //  Check verical matches
                    hasMatch = hasMatch || GridHasVerticalMatch(x, y);
                }
            }
            return hasMatch;
        }

        /// <summary>
        /// Scans the coloumns for veritcal matches using the Transistive property.
        /// </summary>
        /// <param name="x">X position of the token to be compared</param>
        /// <param name="y">Y position of the token to be compared</param>
        /// <returns>True if 3 tokens has the same sprite</returns>
        public bool GridHasVerticalMatch(int x, int y)
        {
            int top = y + 1;
            int mid = y;
            int low = y - 1;

            //  If the index you're checking is out of bounds,
            //  we do not have a vertical match
            if(low < 0 || top > gameManager.gridHeight - 1)
            {
                return false;
            }

            GameObject token1 = gameManager.gridArray[x, top];
            GameObject token2 = gameManager.gridArray[x, mid];
            GameObject token3 = gameManager.gridArray[x, low];

            //  Check the token sprite exists
            if (token1 != null && token2 != null && token3 != null)
            {
                //  Get "Sprite Renderer" from each token;
                SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
                SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
                SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

                //  Use transitive property to compare all the sprites
                return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
            }
            else
            {
                //  If one token is null, we do not have a vertical match
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
            GameObject first = gameManager.gridArray[x, y];

            if (first != null)
            {
                SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
                
                for (int i = y + 1; i < gameManager.gridWidth; i++)
                {
                    //  Gets other token to compare the first token to
                    GameObject other = gameManager.gridArray[x, i];

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
            for (int x = 0; x < gameManager.gridWidth; x++)
            {
                for (int y = 0; y < gameManager.gridHeight; y++)
                {
                    
                    int verticalMatchLength = GetVerticalMatchLength(x, y);

                    //  If we have matched more than 2 tokens remove them from
                    //  the grid
                    if (verticalMatchLength > 2)
                    {
                        for (int i = y; i < y + verticalMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[x, i];
                            Destroy(token);
                            gameManager.gridArray[x, i] = null;

                            numRemoved++;
                        }
                    }

                    //  Discard 2 collums from the right most
                    if (x < gameManager.gridWidth - 2)
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
                                GameObject token = gameManager.gridArray[i, y];

                                //  Destory the game ojbect;
                                Destroy(token);

                                //  Reset the grid to null;
                                gameManager.gridArray[i, y] = null;

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