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
	
	// Update is called once per frame
	public override void Update () 
	{
		display.text = "Combos Left: " + combosLeft.ToString();
		final.text = endText;
		if (combosLeft == 0 && !GridHasEmpty()) { 
			endText = "FINAL SCORE";
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				SceneManager.LoadScene (0);
			}
		} else{
			endText = "";
		}

		if(!GridHasEmpty())
		{
			if(matchManager.GridHasMatch())
			{
				matchManager.RemoveMatches();
				GetComponent<AndrewMatchManager> ().comboMultiplier++;
				if (GetComponent<AndrewMatchManager> ().comboMultiplier == 2) 
				{
					combosLeft--;
					GetComponent<AnrewInputManager> ().swaps = GetComponent<AnrewInputManager> ().maxSwaps;
				}
			}
			else
			{
				if (combosLeft > 0) 
				{
					inputManager.SelectToken ();
					GetComponent<AndrewMatchManager> ().comboMultiplier = 1;
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
}
