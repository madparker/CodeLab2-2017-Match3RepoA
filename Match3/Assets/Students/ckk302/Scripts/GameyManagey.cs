using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kitkat
{
    public class GameyManagey : GameManagerScript
    {
        public override void Start()
        {
            tokenTypes = (Object[])Resources.LoadAll("ckk302/");
            gridArray = new GameObject[gridWidth, gridHeight];
            MakeGrid();
            matchManager = GetComponent<MatchManagerScript>();
            inputManager = GetComponent<InputManagerScript>();
            repopulateManager = GetComponent<RepopulateScript>();
            moveTokenManager = GetComponent<MoveTokensScript>();
        }

        void MakeGrid()
        {
            grid = new GameObject("TokenGrid");
            //  Makes grid based on width and height variables
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    //  Fills in the grid with tokens
                    AddTokenToPosInGrid(x, y, grid);
                }
            }
        }

    }
}