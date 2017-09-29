using UnityEngine;
using System.Collections;

public class MoveTokensScript : MonoBehaviour {

	//The other scripts on the Game Manager Object.
	protected GameManagerScript gameManager;
	protected MatchManagerScript matchManager;

	//Bool to check if token is moving.
	public bool move = false;

	//Determines how fast a token will move to a new position.
	public float lerpPercent;
	public float lerpSpeed;

	//Determines whether or not the selected token should move(?)
	bool userSwap;

	protected GameObject exchangeToken1;
	GameObject exchangeToken2;

	protected Vector2 exchangeGridPos1;
	protected Vector2 exchangeGridPos2;

	//this runs at the start
	public virtual void Start () {

		// Gets references to GameManagerScript and MatchManager Script.
		gameManager = GetComponent<GameManagerScript>();
		matchManager = GetComponent<MatchManagerScript>();
		lerpPercent = 0;
	}

	//this runs every frame
	public virtual void Update () {

		//Increments lerpPercent to 1 when a token is moving.
		if(move){
			lerpPercent += lerpSpeed;

			//Caps lerpPercent at 1.
			if(lerpPercent >= 1){
				lerpPercent = 1;
			}

			//Echange Token is called whenever tokens are selected and sent from Input Manager.
			if(exchangeToken1 != null){
				ExchangeTokens();
			}
		}
	}

	//Called whenever a token starts to move; sets move true and resets the lerpPercent
	public void SetupTokenMove(){
		move = true;
		lerpPercent = 0;
	}

	public void SetupTokenExchange(GameObject token1, Vector2 pos1,
	                               GameObject token2, Vector2 pos2, bool reversable){
		SetupTokenMove();


		exchangeToken1 = token1;//GameObject of first token selected.
		exchangeToken2 = token2;//GameObject of second token selected.

		exchangeGridPos1 = pos1;//Position of first token selected.
		exchangeGridPos2 = pos2;//Position of second token selected.

		this.userSwap = reversable;//Will the token go back to its original position after switching? 
	}

	public virtual void ExchangeTokens(){
		//Sets the position of the first token as the start point and the position of the second token as the end point.
		Vector3 startPos = gameManager.GetWorldPositionFromGridPosition((int)exchangeGridPos1.x, (int)exchangeGridPos1.y);
		Vector3 endPos = gameManager.GetWorldPositionFromGridPosition((int)exchangeGridPos2.x, (int)exchangeGridPos2.y);

		//Lerps the positions of the two tokens. (lerpy tokey)
		Vector3 movePos1 = Vector3.Lerp(startPos, endPos, lerpPercent);
		Vector3 movePos2 = Vector3.Lerp(endPos, startPos, lerpPercent);

		//Moves the first token to the second tokens position and vice versa. (movey tokey)
		exchangeToken1.transform.position = movePos1;
		exchangeToken2.transform.position = movePos2;

		//Updates the grid when both tokens are at their new positions. 
		//Token 1's position in the grid is now Token 2's and vice versa. (swappy tokey)
		if(lerpPercent == 1){
			gameManager.gridArray[(int)exchangeGridPos2.x, (int)exchangeGridPos2.y] = exchangeToken1;
			gameManager.gridArray[(int)exchangeGridPos1.x, (int)exchangeGridPos1.y] = exchangeToken2;

			//If the Match Manager does not find a match, and userSwap is true, 
			//the tokens will revert to their original position and makes userSwap false so that it
			//doesn't swap back and forth infinitley. Otherwise it empties out the exchange tokens (swappy checky)
			if(!matchManager.GridHasMatch() && userSwap){
				SetupTokenExchange(exchangeToken1, exchangeGridPos2, exchangeToken2, exchangeGridPos1, false);
			} else {
				exchangeToken1 = null;
				exchangeToken2 = null;
				move = false;
			}
		}
	}

	//Moves token down if there is an empty space below it (fally tokey)
	public virtual void MoveTokenToEmptyPos(int startGridX, int startGridY,
	                                int endGridX, int endGridY,
	                                GameObject token){
	
		//Sets start pos as the token pos and the end pos as the empty space on the grid.
		Vector3 startPos = gameManager.GetWorldPositionFromGridPosition(startGridX, startGridY);
		Vector3 endPos = gameManager.GetWorldPositionFromGridPosition(endGridX, endGridY);

		//Sets the lerp for the token (lerpy tokey)
		Vector3 pos = Vector3.Lerp(startPos, endPos, lerpPercent);

		//Moves the token (movey tokey)
		token.transform.position =	pos;

		//Set the token's position in the grid to its new spot.  Makes the spot that it moved from null. (spotty setty)
		if(lerpPercent == 1){
			gameManager.gridArray[endGridX, endGridY] = token;
			gameManager.gridArray[startGridX, startGridY] = null;
		}
	}

	//Checks if there is a space for a token to fall into (Spacey Checky).
	public virtual bool MoveTokensToFillEmptySpaces(){
		bool movedToken = false;

		for(int x = 0; x < gameManager.gridWidth; x++){ //Searches through the grid.
			for(int y = 1; y < gameManager.gridHeight ; y++){
				if(gameManager.gridArray[x, y - 1] == null){ //If the space below the space we're on is empty
					for(int pos = y; pos < gameManager.gridHeight; pos++){ // search the grid again to find all
						GameObject token = gameManager.gridArray[x, pos]; // tokens above.
						if(token != null){ // if a token exists in that space
							MoveTokenToEmptyPos(x, pos, x, pos - 1, token); //call MoveTokenToEmptyPos to move it down
							movedToken = true;// and changed movedToken to true so that it calls so that it doesn't repopulate
						}
					}
				}
			}
		}

		//Sets move to false when lerp is complete (lerpy stoppy).
		if(lerpPercent == 1){
			move = false;
		}
		// Returns true if there is an empty space that needs to be repopulated.
		return movedToken;
	}
}
