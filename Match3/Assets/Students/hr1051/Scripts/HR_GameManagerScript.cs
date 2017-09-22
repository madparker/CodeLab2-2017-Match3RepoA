using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {
	public class HR_GameManagerScript : GameManagerScript {
		public virtual void Start () {
			tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
			gridArray = new GameObject[gridWidth, gridHeight];
			matchManager = GetComponent<MatchManagerScript>();
			MakeGrid();
			inputManager = GetComponent<InputManagerScript>();
			repopulateManager = GetComponent<RepopulateScript>();
			moveTokenManager = GetComponent<MoveTokensScript>();
		}

		public override void Update () {
			if (((HR_MoveTokensScript)(moveTokenManager)).GetIsMoving ())
				return;
			
			if (!GridHasEmpty ()) {
				if (matchManager.GridHasMatch ()) {
					matchManager.RemoveMatches ();
				} else {
					inputManager.SelectToken ();
				}
			} else {
				moveTokenManager.MoveTokensToFillEmptySpaces ();
			}
		}

		void MakeGrid () {
			grid = new GameObject ("TokenGrid");
			//  Makes grid based on width and height variables
			for (int x = 0; x < gridWidth; x++) {
				for (int y = 0; y < gridHeight; y++) {
					//  Fills in the grid with tokens
					AddTokenToPosInGrid (x, y, grid);
				}
			}
		}

		public void AddTokenToPosInGrid(int x, int y, GameObject parent) {
			Vector3 position = GetWorldPositionFromGridPosition (x, y);

			int t_num = Random.Range (0, tokenTypes.Length);
			//  Gets random token from the tokenType array
			GameObject token = 
				Instantiate (tokenTypes [t_num], 
					position, 
					Quaternion.identity) as GameObject;
			token.transform.parent = parent.transform;
			gridArray [x, y] = token;

			if (x > 1 && matchManager.GridHasHorizontalMatch (x - 2, y)) {
				t_num = (t_num + 1) % tokenTypes.Length;
				token.GetComponent<SpriteRenderer> ().sprite = ((GameObject)tokenTypes [t_num]).GetComponent<SpriteRenderer> ().sprite;
			}

			if (y > 1 && ((HR_MatchManagerScript)matchManager).GridHasVerticalMatch (x, y - 2)) {
				t_num = (t_num + 1) % tokenTypes.Length;
				token.GetComponent<SpriteRenderer> ().sprite = ((GameObject)tokenTypes [t_num]).GetComponent<SpriteRenderer> ().sprite;
			}
		}
	}
}
