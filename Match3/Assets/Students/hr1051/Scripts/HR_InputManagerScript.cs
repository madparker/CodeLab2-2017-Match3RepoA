using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {
	public class HR_InputManagerScript : InputManagerScript {
		[SerializeField] GameObject mySelectRing;
		private Vector2 mySelectTokenInGrid;
		private Vector2 myMouseDownPosition;

		public override void Start () {
			base.Start ();

			mySelectRing = Instantiate (mySelectRing, this.transform);
			mySelectRing.SetActive (false);
		}

		//Click on a token to to select;
		public override void SelectToken(){
			//On mouse left button clikced to select a token;
			if (Input.GetMouseButtonDown (0)) {
				//Convert mouse position to world point (in position);
				myMouseDownPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				//Get the collider of the mouse clicked point (postion);
				Collider2D collider = Physics2D.OverlapPoint (myMouseDownPosition);

				//Check if there any token;
				if (collider != null) {

					//The current selected token positon in grid;
					mySelectTokenInGrid = gameManager.GetPositionOfTokenInGrid (collider.gameObject);

					//If there is a token, get the token's game object;
					if (selected == null) {
						//If the first selected token is empty, "selected" stores the current tokan game object;
						selected = collider.gameObject;
					} else {
						//The first selected token position;
						Vector2 pos1 = gameManager.GetPositionOfTokenInGrid (selected);
						Vector2 pos2 = mySelectTokenInGrid;

						//check the distance between the 2 selected tokens' position;
						if (Mathf.Abs (pos1.x - pos2.x) + Mathf.Abs (pos1.y - pos2.y) == 1) {
							//If the distance is 1, do the position swap;
							moveManager.SetupTokenExchange (selected, pos1, collider.gameObject, pos2, true);
//							Debug.Log (selected.name + " " + pos1 + "-" + collider.gameObject.name + " " + pos2);

							//Clear the "selected" to null;
							selected = null;
						} else {
							selected = collider.gameObject;
						}
					}
				}

				UpdateSelectDisplay ();
			}

			if (Input.GetMouseButton (0) && selected != null) {
				//get mouse position in world space
				Vector2 t_mouseMove = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition) - myMouseDownPosition;

				if (t_mouseMove.sqrMagnitude > 1) {

					GameObject t_targetGameObject = this.gameObject;

					if (Mathf.Abs (t_mouseMove.x) > Mathf.Abs (t_mouseMove.y)) {
						//horizontal move

						if (t_mouseMove.x > 0 && mySelectTokenInGrid.x < gameManager.gridWidth - 1) {
							//swap right
							//get target game object
							t_targetGameObject = gameManager.gridArray[(int)mySelectTokenInGrid.x + 1, (int)mySelectTokenInGrid.y];
							moveManager.SetupTokenExchange (
								selected, mySelectTokenInGrid, 
								t_targetGameObject, mySelectTokenInGrid + Vector2.right, 
								true
							);
								
//							Debug.Log (selected.name + " " + mySelectTokenInGrid + "-" + t_targetGameObject.name + " " + (mySelectTokenInGrid + Vector2.right));
						} else if (t_mouseMove.x < 0 && mySelectTokenInGrid.x > 0) {
							//swap left//get target game object
							t_targetGameObject = gameManager.gridArray[(int)mySelectTokenInGrid.x - 1, (int)mySelectTokenInGrid.y];
							moveManager.SetupTokenExchange (
								selected, mySelectTokenInGrid, 
								t_targetGameObject, mySelectTokenInGrid + Vector2.left, 
								true
							);
						}
					} else {
						//vertical move

						if (t_mouseMove.y > 0 && mySelectTokenInGrid.y < gameManager.gridHeight - 1) {
							//swap up
							//get target game object
							t_targetGameObject = gameManager.gridArray[(int)mySelectTokenInGrid.x, (int)mySelectTokenInGrid.y + 1];
							moveManager.SetupTokenExchange (
								selected, mySelectTokenInGrid, 
								t_targetGameObject, mySelectTokenInGrid + Vector2.up, 
								true
							);
						} else if (t_mouseMove.y < 0 && mySelectTokenInGrid.y > 0) {
							//swap left//get target game object
							t_targetGameObject = gameManager.gridArray[(int)mySelectTokenInGrid.x, (int)mySelectTokenInGrid.y - 1];
							moveManager.SetupTokenExchange (
								selected, mySelectTokenInGrid, 
								t_targetGameObject, mySelectTokenInGrid + Vector2.down, 
								true
							);
						}
					}

					//Clear the "selected" to null;
					selected = null;

					UpdateSelectDisplay ();
				}
			}
		}

		private void UpdateSelectDisplay () {
			if (selected == null && mySelectRing.activeSelf == true) {
				mySelectRing.SetActive (false);
			} else if (selected != null) {
				//if select something, update the select ring pos
				mySelectRing.transform.position = selected.transform.position;
				if (mySelectRing.activeSelf == false)
					mySelectRing.SetActive (true);
			}
		}
	}
}
