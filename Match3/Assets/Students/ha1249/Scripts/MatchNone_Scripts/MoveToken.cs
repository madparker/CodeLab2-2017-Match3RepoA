using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hosni{

	public class MoveToken : MonoBehaviour {

		GameManager gameManager;
		MatchManager matchManager;

		//Bool to check if token is moving.
		public bool move = false;

		//Determines how fast a token will move to a new position.
		public float lerpPercent;
		public float lerpSpeed;



		//this runs at the start
		void Start () {

			// Gets references to GameManagerScript and MatchManager Script.
			gameManager = GetComponent<Hosni.GameManager>();
			matchManager = GetComponent<Hosni.MatchManager>();
			lerpPercent = 0;
		}

		//this runs every frame
		void Update () {


			//Increments lerpPercent to 1 when a token is moving.
			if(move){
				lerpPercent += lerpSpeed;

				//Caps lerpPercent at 1.
				if(lerpPercent >= 1){
					lerpPercent = 1;
				}

		
			}
		}



		//Called whenever a token starts to move; sets move true and resets the lerpPercent
		public void SetupTokenMove(){
			move = true;
			lerpPercent = 0;
		}

	



		//Moves token down if there is an empty space below it (fally tokey)
		public void MoveTokenToEmptyPos(int startGridX, int startGridY,
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
				if (startGridX != gameManager.lastCursorGridXPos) {
					gameManager.gridArray [startGridX, startGridY] = null;
				}
				move = false;
			}
		}

		//Checks if there is a space for a token to fall into (Spacey Checky).
		public bool MoveTokensToFillEmptySpaces(){
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
							break;
						}
					}
				}
			}

			//Sets move to false when lerp is complete (lerpy stoppy).
//			if(lerpPercent == 1){
//				move = false;
//			}
			// Returns true if there is an empty space that needs to be repopulated.
			return movedToken;
		}
	}
}
