using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hosni{

	public class InputManager : MonoBehaviour {

		GameManager gameManager;
		MoveToken moveManager;
		GameObject dropped = null;


		void Start () {
			moveManager = GetComponent<Hosni.MoveToken>();
			gameManager = GetComponent<Hosni.GameManager>();

		}
			


		// Drops Token when mouse is clicked
		public void DropToken(){
			if (Input.GetMouseButtonDown (0)) {

				// If its not dropping a token and the column has space, it drops the last token and buffers another one 
				if (!moveManager.move &&  gameManager.GetEmptyPositioninColumn(gameManager.cursorGridXPos) < gameManager.gridHeight - 1) {


					dropped = gameManager.nextToken;
					dropped.transform.parent = gameManager.grid.transform;
					gameManager.nextToken = gameManager.GetNextToken ();


					// Sets the horizontal pos in the array as the cursor's last horizontal position
					gameManager.lastCursorGridXPos = gameManager.cursorGridXPos;
					moveManager.SetupTokenMove ();


				}

			}
		}

		void Update(){

			//Moving dropped tokens into position (when it can)
			if (moveManager.move) {
				moveManager.MoveTokenToEmptyPos (gameManager.lastCursorGridXPos, gameManager.gridHeight, 
													gameManager.lastCursorGridXPos,
													gameManager.GetEmptyPositioninColumn (gameManager.lastCursorGridXPos), 
													dropped);
			}
				
		}
	}
}
