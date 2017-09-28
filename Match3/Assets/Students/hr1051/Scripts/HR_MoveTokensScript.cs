using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {
	public class HR_MoveTokensScript : MoveTokensScript {
		private List<Vector2> myMoveList = new List<Vector2> ();
		[SerializeField] float myMoveStartSpeed = 1;
		private float myMoveCurrentSpeed;
		[SerializeField] float myMoveGravity = 10;
		private bool isMoving = false;

		public override void Start ()
		{
			base.Start ();

			myMoveCurrentSpeed = myMoveStartSpeed;
		}

		public override void Update () {
//			Debug.Log (move);
			base.Update ();

			if (isMoving) {
				UpdateMoveTokens ();
			}
		}

		//Checks if there is a space for a token to fall into (Spacey Checky).
		public override bool MoveTokensToFillEmptySpaces(){
			for(int x = 0; x < gameManager.gridWidth; x++){ //Searches through the grid.
				int t_newPosY = 0;
				for (int y = 0; y < gameManager.gridHeight; y++) {
					if (gameManager.gridArray [x, y] == null) {
						isMoving = true;
					} else {
						Vector2 t_moveTarget = new Vector2 (x, t_newPosY);
						if (myMoveList.IndexOf (t_moveTarget) == -1) {
							myMoveList.Add (t_moveTarget);
						}

						gameManager.gridArray [x, t_newPosY] = gameManager.gridArray [x, y];

						t_newPosY += 1;
					}

//					Debug.Log (t_newPosY);
				}

				int t_emptyCount = gameManager.gridHeight - t_newPosY;
				for (int i = 0; i < t_emptyCount; i++) {
					//add the token to moving object list
					Vector2 t_moveTarget = new Vector2 (x, gameManager.gridHeight - 1 - i);
					if (myMoveList.IndexOf (t_moveTarget) == -1) {
						myMoveList.Add (t_moveTarget);
					}
					((HR_GameManagerScript)gameManager).AddTokenToPosInGrid(x, gameManager.gridHeight - 1 - i, gameManager.grid);
					gameManager.gridArray [x, gameManager.gridHeight - 1 - i].transform.position = 
						((HR_GameManagerScript)gameManager).GetWorldPositionFromGridPosition (x, gameManager.gridHeight + t_emptyCount - i - 1);
				}
			}
			return true;
		}

		private void UpdateMoveTokens () {
			if (isMoving) {
				myMoveCurrentSpeed += myMoveGravity * Time.deltaTime;
				for (int i = 0; i < myMoveList.Count; i++) {
					int t_x = (int)myMoveList [i].x;
					int t_y = (int)myMoveList [i].y;
					Transform t_targetTransform = gameManager.gridArray [t_x, t_y].transform;
					t_targetTransform.position += Vector3.down * myMoveCurrentSpeed * Time.deltaTime;
					if (t_targetTransform.position.y < ((HR_GameManagerScript)gameManager).GetWorldPositionFromGridPosition (t_x, t_y).y) {
						//arrive
						t_targetTransform.position = ((HR_GameManagerScript)gameManager).GetWorldPositionFromGridPosition (t_x, t_y);
						myMoveList.RemoveAt (i);
						i--;
					}
				}

				if (myMoveList.Count == 0) {
					isMoving = false;
					move = false;
					myMoveCurrentSpeed = myMoveStartSpeed;
				}
			}
		}

		public bool GetIsMoving () {
			return isMoving;
		}


	}


}
