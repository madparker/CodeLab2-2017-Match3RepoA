using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrewGameManager : GameManagerScript {

	public int multiplier;
	
	// Update is called once per frame
	public override void Update () 
	{
		if(!GridHasEmpty())
		{
			if(matchManager.GridHasMatch())
			{
				matchManager.RemoveMatches();
				GetComponent<AndrewMatchManager> ().comboMultiplier++;
			}
			else
			{
				inputManager.SelectToken();
				GetComponent<AndrewMatchManager>().comboMultiplier = 1;
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
