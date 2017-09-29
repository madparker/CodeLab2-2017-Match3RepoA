using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kitkat
{
    public class MatchyManagey : MatchManagerScript
    {

        public AudioSource soundy;
        public AudioClip yay;
        public TextMesh scoreText;

        public int score = 0;

        void Update()
        {
            scoreText.text = "Score: " + score.ToString();
        }

        public bool GridHasVerticalMatch(int x, int y)
        {
            GameObject token1 = gameManager.gridArray[x, y + 0];
            GameObject token2 = gameManager.gridArray[x, y + 1];
            GameObject token3 = gameManager.gridArray[x, y + 2];

            //Check the token sprite exist;
            if (token1 != null && token2 != null && token3 != null)
            {
                //Get "Sprite Renderer" from each token;
                SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
                SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
                SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

                //Check are these 3 sprite (token using this sprite) are matching;
                return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
            }
            else
            {
                //if not, return false;
                return false;
            }
        }

        public override bool GridHasMatch()
        {
            bool match = false;

            for (int x = 0; x < gameManager.gridWidth; x++)
            {
                for (int y = 0; y < gameManager.gridHeight; y++)
                {
                    if (x < gameManager.gridWidth - 2)
                    {
                        
                        match = match || GridHasHorizontalMatch(x, y);
                    }

                    if (y < gameManager.gridHeight - 2)
                    {
                        
                        match = match || GridHasVerticalMatch(x, y);
                    }
                }
            }

            return match;
        }

        //This method is called in "RemoveMatches()";
        //Return the lenth of matched tokens;
        //(int x, int y) indecate the left most token position in the matched tokens;
        public int GetVerticalMatchLength(int x, int y)
        {

            //to count the length of match
            int matchLength = 1;

            //store the left most token's game object;
            GameObject first = gameManager.gridArray[x, y];

            //Check the left most token is existed;
            if (first != null)
            {
                SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

                //Check the other tokens at right side are match and get the lenth of the matched tokens;
                for (int i = y + 1; i < gameManager.gridWidth; i++)
                {
                    //get the game object from this grid
                    GameObject other = gameManager.gridArray[x, i];

                    //check if the game object exists;
                    if (other != null)
                    {

                        //get the sprite renderer from the game object;
                        SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

                        //check if the sprite is the same as the left most token's sprite;
                        if (sr1.sprite == sr2.sprite)
                        {

                            //increase the count for match;
                            matchLength++;
                        }
                        else
                        {
                            //if the sprite is different, end the for loop;
                            break;
                        }
                    }
                    else
                    {
                        //if the game object dosen't exist, end the for loop;
                        break;
                    }
                }
            }

            //Return the lenth of matched tokens as int;
            return matchLength;
        }

        //Remove matched tokens;
        public override int RemoveMatches()
        {
            int numRemoved = 0;

            //Go through all the grid to check the matched tokens horizontally;
            for (int x = 0; x < gameManager.gridWidth; x++)
            {
                for (int y = 0; y < gameManager.gridHeight; y++)
                {

                    //Discard 2 collums from the right most;
                    if (x < gameManager.gridWidth - 2)
                    {

                        //Call "GetHorizontalMatchLength(int x, int y)";
                        //Get the lenth of matched tokens;
                        int horizonMatchLength = GetHorizontalMatchLength(x, y);

                        //If at least 3 tokens are matching, get the number of tokens which will be removed;
                        if (horizonMatchLength > 2)
                        {

                            //Destroy matched token game objects from the grid and get the number of removed tokens;
                            for (int i = x; i < x + horizonMatchLength; i++)
                            {
                                //Get the game object of token that will be removed;
                                GameObject token = gameManager.gridArray[i, y];

                                //Destory the game ojbect;
                                Destroy(token);

                                //Reset the grid to null;
                                gameManager.gridArray[i, y] = null;

                                //Increase the number of removed tokens;
                                numRemoved++;
                            }
                        }
                    }

                    if (y < gameManager.gridHeight - 2)
                    {

                        //Call "GetHorizontalMatchLength(int x, int y)";
                        //Get the lenth of matched tokens;
                        int verticalMatchLength = GetVerticalMatchLength(x, y);

                        //If at least 3 tokens are matching, get the number of tokens which will be removed;
                        if (verticalMatchLength > 2)
                        {

                            //Destroy matched token game objects from the grid and get the number of removed tokens;
                            for (int i = y; i < y + verticalMatchLength; i++)
                            {
                                //Get the game object of token that will be removed;
                                GameObject token = gameManager.gridArray[x, i];

                                //Destory the game ojbect;
                                Destroy(token);

                                //Reset the grid to null;
                                gameManager.gridArray[x, i] = null;

                                //Increase the number of removed tokens;
                                numRemoved++;
                            }
                        }
                    }
                }
            }

            soundy.PlayOneShot(yay);

            //Return the number of tokens which will be removed;
            score += numRemoved;
            return numRemoved;
        }
    }


}
