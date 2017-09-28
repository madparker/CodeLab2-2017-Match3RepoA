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

		private int myScore = 0;
		[SerializeField] UnityEngine.UI.Text myScoreDisplay;

		private float myTimer;

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
			MakeGrid();
			inputManager = GetComponent<InputManagerScript>();
			repopulateManager = GetComponent<RepopulateScript>();
			moveTokenManager = GetComponent<MoveTokensScript>();

			myBackSpriteRenderer.size = new Vector2 (gridWidth + 0.2f, gridHeight + 0.2f);

			myScore = 0;
			myScoreDisplay.text = "0";
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

		public new Vector2 GetWorldPositionFromGridPosition(int x, int y)
		{
			return new Vector2 (
				(x - gridWidth / 2.0f) * tokenSize + 0.5f,
				(y - gridHeight / 2.0f) * tokenSize + 0.5f
			);
		}

		public new void AddTokenToPosInGrid(int x, int y, GameObject parent) {
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

			if (x > 1 && ((HR_MatchManagerScript)matchManager).GridHasHorizontalMatch (x - 2, y)) {
				t_num = (t_num + 1) % myTokenColors.Length;
				token.GetComponent<HR_Token> ().SetToken (t_num);
			}

			if (y > 1 && ((HR_MatchManagerScript)matchManager).GridHasVerticalMatch (x, y - 2)) {
				t_num = (t_num + 1) % myTokenColors.Length;
				token.GetComponent<HR_Token> ().SetToken (t_num);
			}

			//update: the logic is wrong, need to do the x direction check again
			if (x > 1 && ((HR_MatchManagerScript)matchManager).GridHasHorizontalMatch (x - 2, y)) {
				t_num = (t_num + 1) % myTokenColors.Length;
				token.GetComponent<HR_Token> ().SetToken (t_num);
			}
		}

		public void AddScore (int g_score) {
			myScore += g_score;
			myScoreDisplay.text = myScore.ToString ("#");
		}
	}
}
