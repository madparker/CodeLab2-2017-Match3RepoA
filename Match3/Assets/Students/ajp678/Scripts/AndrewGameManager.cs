using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AndrewGameManager : GameManagerScript {

	public int multiplier;
	public int combosLeft;
	public Text display;
	public Text final;
	string endText;

	public override void Start ()
	{
		tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
		gridArray = new GameObject[gridWidth, gridHeight];
		matchManager = GetComponent<MatchManagerScript>();
		matchManager.SendMessage ("setGM");
		inputManager = GetComponent<InputManagerScript>();
		repopulateManager = GetComponent<RepopulateScript>();
		moveTokenManager = GetComponent<MoveTokensScript>();
		MakeGridAJP();
	}

	// Update is called once per frame
	public override void Update () 
	{
		//Makes sure UI displays correctly.
		display.text = "Combos Left: " + combosLeft.ToString();
		final.text = endText;
		if (combosLeft == 0 && !GridHasEmpty()) { 
			endText = "FINAL SCORE";
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				SceneManager.LoadScene ("RandomScene_ajp");
			}
		} else{
			endText = "";
		}

		if(!GridHasEmpty())
		{
			if(matchManager.GridHasMatch())
			{
				matchManager.RemoveMatches();
				GetComponent<AndrewMatchManager> ().comboMultiplier++; //Increase multiplier every time there is a match.
				if (GetComponent<AndrewMatchManager> ().comboMultiplier == 2) 
				{
					combosLeft--; //Subtract from max combos remaining at the start of each new combo.
					GetComponent<AnrewInputManager> ().swaps = GetComponent<AnrewInputManager> ().maxSwaps; //reset swaps available at the beginning of each new combo.
				}
			}
			else
			{
				if (combosLeft > 0) //Game only continues if the players still has combos left to make.
				{
					inputManager.SelectToken ();
					GetComponent<AndrewMatchManager> ().comboMultiplier = 1; // reset combo multiplier when combos stop.
				}
			}
		}
		else
		{
			if(!moveTokenManager.move)
			{
				moveTokenManager.SetupTokenMove();
			}
			if(!moveTokenManager.MoveTokensToFillEmptySpaces())
			{
				repopulateManager.AddNewTokensToRepopulateGrid();
			}
		}
	}

	void MakeGridAJP ()
	{
		grid = new GameObject ("TokenGrid");
		//  Makes grid based on width and height variables
		for(int x = 0; x < gridWidth; x++)
		{
			for(int y = 0; y < gridHeight; y++)
			{
				//  Fills in the grid with tokens
				AddTokenToPosInGrid(x,y,grid);
				if (matchManager.GridHasMatch()) // if there is a match in the grid.
				{
					for (int x1 = 0; x1 < gridWidth; x1++) { // go through the grid again.
						for (int y1 = 0; y1 < gridHeight; y1++) {
							GameObject pulledToken = gridArray [x1, y1]; 
							if (pulledToken != null) 
							{
								Destroy (pulledToken); // clear out grid.
								gridArray [x1, y1] = null;
							}
							
						}
					}
					x = 0; // reset loop.  Repeat until the grid starts with no matches.
					y = 0;
				}
			}
		}
	}
}
