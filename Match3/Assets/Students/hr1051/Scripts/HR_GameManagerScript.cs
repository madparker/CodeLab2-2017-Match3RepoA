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

		[SerializeField] float myGameTime = 30;
		[SerializeField] float myMatchTimeGain = 0.2f;
		private float myGameTimer;

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
			moveTokenManager = GetComponent<MoveTokensScript>();
			MakeGrid();
			inputManager = GetComponent<InputManagerScript>();
			repopulateManager = GetComponent<RepopulateScript>();

			myBackSpriteRenderer.size = new Vector2 (gridWidth + 0.2f, gridHeight + 0.2f);

			HR_UI_Play.Instance.InitTimeBar (-gridHeight / 2.0f - 0.6f);

			myScore = 0;

			myGameTimer = myGameTime;
		}

		public override void Update () {
			if (myGameTimer > 0) {
				myGameTimer -= Time.deltaTime;
				if (myGameTimer <= 0) {
					myGameTimer = 0;
				}

				HR_UI_Play.Instance.ShowTimer (myGameTimer / myGameTime);
			}

			if (((HR_MoveTokensScript)(moveTokenManager)).GetIsMoving ())
				return;
			
			if (!GridHasEmpty ()) {
				if (matchManager.GridHasMatch ()) {
					matchManager.RemoveMatches ();
				} else {
					if (GetIsEnd ())
						return;
					inputManager.SelectToken ();
				}
			} else {
				moveTokenManager.MoveTokensToFillEmptySpaces ();
			}
		}

		new void MakeGrid () {
			grid = new GameObject ("TokenGrid");
			//  Makes grid based on width and height variables
			for (int x = 0; x < gridWidth; x++) {
				for (int y = 0; y < gridHeight; y++) {
					//  Fills in the grid with tokens
					AddTokenToPosInGrid (x, y, grid);
				}
			}

			((HR_MoveTokensScript)(moveTokenManager)).StartMoving ();
		}

		public override Vector2 GetWorldPositionFromGridPosition(int x, int y)
		{
			return new Vector2 (
				(x - gridWidth / 2.0f) * tokenSize + 0.5f,
				(y - gridHeight / 2.0f) * tokenSize + 0.5f
			);
		}

		public new void AddTokenToPosInGrid(int x, int y, GameObject parent) {
			Vector3 position = GetWorldPositionFromGridPosition (x, y) + gridHeight * Vector2.up;

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

		public void AddScore (int g_count) {
			myScore += g_count;
			HR_UI_Play.Instance.ShowScore (myScore);

			MatchAddTime (g_count);
		}

		public void MatchAddTime (int g_count) {
			if (myGameTimer > myGameTime)
				myGameTimer = myGameTime;
			myGameTimer = myGameTimer + g_count * myMatchTimeGain;
		}

		public bool GetIsEnd () {
			if (myGameTimer == 0)
				return true;
			return false;
		}

		public void RegenerateGrid () {
			foreach (GameObject t_token in gridArray) {
				Destroy (t_token);
			}

			MakeGrid ();
		}
	}
}
