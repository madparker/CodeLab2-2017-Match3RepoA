using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {
	public class HR_GameManagerScript : GameManagerScript {
		public GameObject myTokenPrefab;
		public Color[] myTokenColors;
		public Sprite[] myTokenTexture;
		public SpriteRenderer myBackSpriteRenderer;

		private static HR_GameManagerScript instance = null;

		//========================================================================
		public static HR_GameManagerScript Instance {
			get { 
				return instance;
			}
		}

		void Awake () {
			if (instance != null && instance != this) {
				Destroy(this.gameObject);
			} else {
				instance = this;
			}
//			DontDestroyOnLoad(this.gameObject);
		}
		//========================================================================


		public override void Start () {
			gridArray = new GameObject[gridWidth, gridHeight];
			matchManager = GetComponent<MatchManagerScript>();
			HR_MakeGrid();
			inputManager = GetComponent<InputManagerScript>();
			repopulateManager = GetComponent<RepopulateScript>();
			moveTokenManager = GetComponent<MoveTokensScript>();

			myBackSpriteRenderer.size = new Vector2 (gridWidth + 0.2f, gridHeight + 0.2f);
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

		void HR_MakeGrid () {
			grid = new GameObject ("TokenGrid");
			//  Makes grid based on width and height variables
			for (int x = 0; x < gridWidth; x++) {
				for (int y = 0; y < gridHeight; y++) {
					//  Fills in the grid with tokens
					HR_AddTokenToPosInGrid (x, y, grid);
				}
			}
		}

		public Vector2 GetWorldPositionFromGridPosition(int x, int y)
		{
			return new Vector2(
				(x - gridWidth/2) * tokenSize,
				(y - gridHeight/2) * tokenSize);
		}

		public void HR_AddTokenToPosInGrid(int x, int y, GameObject parent) {
			Vector3 position = GetWorldPositionFromGridPosition (x, y);

			int t_num = Random.Range (0, myTokenColors.Length);
			//  Gets random token from the tokenType array
			GameObject token = 
				Instantiate (myTokenPrefab, 
					position, 
					Quaternion.identity) as GameObject;
			token.transform.parent = parent.transform;
			token.GetComponent<HR_Token> ().SetToken (t_num);
			gridArray [x, y] = token;

			if (x > 1 && ((HR_MatchManagerScript)matchManager).HR_GridHasHorizontalMatch (x - 2, y)) {
				t_num = (t_num + 1) % myTokenColors.Length;
				token.GetComponent<HR_Token> ().SetToken (t_num);
			}

			if (y > 1 && ((HR_MatchManagerScript)matchManager).HR_GridHasVerticalMatch (x, y - 2)) {
				t_num = (t_num + 1) % myTokenColors.Length;
				token.GetComponent<HR_Token> ().SetToken (t_num);
			}
		}
	}
}
