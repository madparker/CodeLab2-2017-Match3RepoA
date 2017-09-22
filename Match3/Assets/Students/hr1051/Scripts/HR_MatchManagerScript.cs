using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {
	public class HR_MatchManagerScript : MatchManagerScript {

		private List<Vector2> myRemoveList = new List<Vector2> ();

		void Awake () {
			gameManager = this.GetComponent<GameManagerScript> ();
		}
		//Check if Match and return a bool;
		public override bool GridHasMatch(){

			int t_widthForMatchCheck = gameManager.gridWidth - 2;
			int t_heightForMatchCheck = gameManager.gridHeight - 2;
			
			for(int x = 0; x < gameManager.gridWidth; x++){
				for(int y = 0; y < gameManager.gridHeight; y++){
					//Check horizontally match and Vertical match;
					if ((x < t_widthForMatchCheck && GridHasHorizontalMatch (x, y)) ||
					    (y < t_heightForMatchCheck && GridHasVerticalMatch (x, y))) {
						Debug.Log ("match");
						return true;
					}
				}
			}

			return false;
		}

		//Check if there are 3 matching tokens in Horizontal grid; This method is called in "GridHasMatch()";
		public bool GridHasVerticalMatch(int x, int y){
			GameObject token1 = gameManager.gridArray[x, y + 0];
			GameObject token2 = gameManager.gridArray[x, y + 1];
			GameObject token3 = gameManager.gridArray[x, y + 2];

			//Check the token sprite exist;
			if (token1 != null && token2 != null && token3 != null) {
				//Get "Sprite Renderer" from each token;
				SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
				SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
				SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

				//Check are these 3 sprite (token using this sprite) are matching;
				return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
			} else {
				//if not, return false;
				return false;
			}
		}

		//This method is called in "RemoveMatches()";
		//Return the lenth of matched tokens;
		//(int x, int y) indecate the left most token position in the matched tokens;
		public int GetVerticalMatchLength(int x, int y){

			//to count the length of match
			int matchLength = 1;

			//store the left most token's game object;
			GameObject first = gameManager.gridArray[x, y];

			//Check the left most token is existed;
			if(first != null){
				SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

				//Check the other tokens at right side are match and get the lenth of the matched tokens;
				for(int i = y + 1; i < gameManager.gridHeight; i++){
					//get the game object from this grid
					GameObject other = gameManager.gridArray[x, i];

					//check if the game object exists;
					if(other != null){

						//get the sprite renderer from the game object;
						SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

						//check if the sprite is the same as the left most token's sprite;
						if(sr1.sprite == sr2.sprite){

							//increase the count for match;
							matchLength++;
						} else {
							//if the sprite is different, end the for loop;
							break;
						}
					} else {
						//if the game object dosen't exist, end the for loop;
						break;
					}
				}
			}

			//Return the lenth of matched tokens as int;
			return matchLength;
		}

		//Remove matched tokens;
		public override int RemoveMatches(){
			int numRemoved = 0;

			int t_widthForMatchCheck = gameManager.gridWidth - 2;
			int t_heightForMatchCheck = gameManager.gridHeight - 2;

			//Go through all the grid to check the matched tokens horizontally;
			for(int x = 0; x < gameManager.gridWidth; x++){
				for(int y = 0; y < gameManager.gridHeight; y++){

					if (x < t_widthForMatchCheck) {
						//Call "GetHorizontalMatchLength(int x, int y)";
						//Get the lenth of matched tokens;
						int horizonMatchLength = GetHorizontalMatchLength (x, y);

						//If at least 3 tokens are matching, get the number of tokens which will be removed;
						if (horizonMatchLength > 2) {

							//Destroy matched token game objects from the grid and get the number of removed tokens;
							for (int i = x; i < x + horizonMatchLength; i++) {
							
								//Get the game object of token that will be removed;
								myRemoveList.Add (new Vector2 (i, y));
							}
						}
					}
					if (y < t_heightForMatchCheck) {
						int verticalMatchLength = GetVerticalMatchLength (x, y);

						//If at least 3 tokens are matching, get the number of tokens which will be removed;
						if (verticalMatchLength > 2) {

							//Destroy matched token game objects from the grid and get the number of removed tokens;
							for (int i = y; i < y + verticalMatchLength; i++) {

								//Get the game object of token that will be removed;
								myRemoveList.Add (new Vector2 (x, i));
							}
						}
					}
				}
			}

			foreach (Vector2 t_removePos in myRemoveList) {
				GameObject token = gameManager.gridArray [(int)t_removePos.x, (int)t_removePos.y];

				if (!token)
					continue;

				//Destory the game ojbect;
				Destroy(token);

				//Reset the grid to null;
				gameManager.gridArray[(int)t_removePos.x, (int)t_removePos.y] = null;

				//Increase the number of removed tokens;
				numRemoved++;
			}

			myRemoveList.Clear ();

			Debug.Log (numRemoved);
			//Return the number of tokens which will be removed;
			return numRemoved;
		}
	}
}
