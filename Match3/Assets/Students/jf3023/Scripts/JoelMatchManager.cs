﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using joel;

public class JoelMatchManager : MatchManagerScript {

    private ScoreManager scoreManager;

    public override void Start()
    {
        base.Start();
        scoreManager = GetComponent<ScoreManager>();

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

                if( y < gameManager.gridHeight - 2)
                {
                    match = match || GridHasVerticalMatch(x, y);
                }
            }

        }

        return match;
    }

    public bool GridHasVerticalMatch(int x, int y)
    {
        GameObject token1 = gameManager.gridArray[x, y + 0];
        GameObject token2 = gameManager.gridArray[x, y + 1];
        GameObject token3 = gameManager.gridArray[x, y + 2];

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

    public int GetVerticalMatchLength(int x, int y)
    {
        int matchLength = 1;

        GameObject first = gameManager.gridArray[x, y];

        if(first != null)
        {
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

            for (int i = y + 1; i < gameManager.gridHeight; i++)
            {
                GameObject other = gameManager.gridArray[x, i];

                if(other != null)
                {
                    SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

                    if(sr1.sprite == sr2.sprite)
                    {
                        matchLength++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        return matchLength;
    }

    public override int RemoveMatches()
    {
        int numRemoved = 0;
        List<TokenScript> tokens = new List<TokenScript>();
        List<Vector2> gridPositions = new List<Vector2>();

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {

                //Discard 2 collums from the right most;
                if (x < gameManager.gridWidth - 2)
                {

                    int horizonMatchLength = GetHorizontalMatchLength(x, y);

                    //If at least 3 tokens are matching, get the number of tokens which will be removed;
                    if (horizonMatchLength > 2)
                    {

                        for (int i = x; i < x + horizonMatchLength; i++)
                        {
       
                            TokenScript token = gameManager.gridArray[i, y].GetComponent<TokenScript>();


                            if (!tokens.Contains(token)) { 
                                tokens.Add(token);
                                token.isInHorizontalMatch = true;
                                token.horizontalMatchLength = horizonMatchLength;
                            }
                            else if (tokens.Contains(token) && token.isInVerticalMatch)
                            {
                                Debug.Log("Hit");

                                token.isInHorizontalMatch = true;
                                token.horizontalMatchLength = horizonMatchLength;
                            }

                            Vector2 gridPos = new Vector2(i, y);

                            if (!gridPositions.Contains(gridPos))
                            {
                                gridPositions.Add(gridPos);
                            }

                        }
                    }
                }

                if(y < gameManager.gridHeight - 2)
                {
                    int verticalMatchLength = GetVerticalMatchLength(x, y);

                    if(verticalMatchLength > 2)
                    {
                        for(int i = y; i < y + verticalMatchLength; i++)
                        {
                            TokenScript token = gameManager.gridArray[x, i].GetComponent<TokenScript>();
                        
                            if (!tokens.Contains(token))
                            {
                                tokens.Add(token);
                                token.isInVerticalMatch = true;
                                token.verticalMatchLength = verticalMatchLength;
                            }
                            else  if (tokens.Contains(token) && token.isInHorizontalMatch)
                            {
                                Debug.Log("Hit");

                                token.isInVerticalMatch = true;
                                token.verticalMatchLength = verticalMatchLength;
                            }


                            Vector2 gridPos = new Vector2(x, i);

                            if (!gridPositions.Contains(gridPos))
                            {
                                gridPositions.Add(gridPos);
                            }

                        }
                    }
                }
            }
        }

        foreach(TokenScript token in tokens)
        {
            scoreManager.Score += token.PointsToAdd();
            if(token.isInHorizontalMatch && token.isInVerticalMatch)
            {
                token.onFire = true;
                token.isInHorizontalMatch = false;
                token.isInVerticalMatch = false;
            }
            else
            {
                Destroy(token.gameObject);
                numRemoved++;
                gameManager.gridArray[(int)gameManager.GetPositionOfTokenInGrid(token.gameObject).x, (int)gameManager.GetPositionOfTokenInGrid(token.gameObject).y] = null;
            }    
        }

        for(int i = 0; i < gridPositions.Count; i++)
        {
           // gameManager.gridArray[(int)gridPositions[i].x, (int)gridPositions[i].y] = null;
        }

        tokens.Clear();
        gridPositions.Clear();

        return numRemoved;
    }
}
