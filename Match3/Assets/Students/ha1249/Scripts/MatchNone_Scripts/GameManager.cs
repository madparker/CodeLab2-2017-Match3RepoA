﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hosni{

	public class GameManager : MonoBehaviour {

		public int gridWidth = 8;
		public int gridHeight = 8;
		public float tokenSize = 1;

		public int cursorGridXPos;
		public int lastCursorGridXPos;


		MatchManager matchManager;
		InputManager inputManager;
		Repopulate repopulateManager;
		MoveToken moveTokenManager;

		public GameObject grid;
		public  GameObject[,] gridArray;
		Object[] tokenTypes;
		GameObject selected;
		GameObject cursor;
		Object frame;
		public GameObject nextToken;

		void Start () {
			tokenTypes = (Object[])Resources.LoadAll("ha1249/Tokens/");
			frame = Resources.Load ("ha1249/TokenFrame") as GameObject;
			gridArray = new GameObject[gridWidth, gridHeight];
//			MakeGrid();

			matchManager = GetComponent<Hosni.MatchManager>();
			inputManager = GetComponent<Hosni.InputManager>();
			repopulateManager = GetComponent<Hosni.Repopulate>();
			moveTokenManager = GetComponent<Hosni.MoveToken>();

			// Create the slider cursor and hide system cursor
			cursor = Instantiate (frame, GetWorldPositionFromGridPosition(0,gridHeight), Quaternion.identity) as GameObject;
			nextToken =  GetNextToken ();
			Cursor.visible = !Cursor.visible;

			//Makes the Grid object that parents all tokens 
			grid = new GameObject("TokenGrid");

		}

		 void Update(){

			MoveCursor ();

//			inputManager.DropToken ();

			if(matchManager.GridHasMatch()){
				print ("hasMatch");
				matchManager.RemoveMatches();
			}

			if (!moveTokenManager.MoveTokensToFillEmptySpaces()) {
				inputManager.DropToken ();
			}



//
//			if(!GridHasEmpty()){
//				if(matchManager.GridHasMatch()){
//					matchManager.RemoveMatches();
//				}
//				else{
//					inputManager.SelectToken();
//				}
//			}
//			else{
//				if(!moveTokenManager.move){
//					moveTokenManager.SetupTokenMove();
//				}
//				if(!moveTokenManager.MoveTokensToFillEmptySpaces()){
//					repopulateManager.AddNewTokensToRepopulateGrid();
//				}
//			}
		}


		/// Makes grid based on height and width variables

//		void MakeGrid()
//		{
//			grid = new GameObject("TokenGrid");
//			//  Makes grid based on width and height variables
//			for(int x = 0; x < gridWidth; x++)
//			{
//				for(int y = 0; y < gridHeight; y++)
//				{
//					//  Fills in the grid with tokens
//					AddTokenToPosInGrid(x, y, grid);
//				}
//			}
//		}


		/// Checks if there is an empty node in the grid

		/// <returns>True if a node of the grid is empty</returns>
//		public bool GridHasEmpty()
//		{
//			for(int x = 0; x < gridWidth; x++)
//			{
//				for(int y = 0; y < gridHeight ; y++)
//				{
//					if(gridArray[x, y] == null)
//					{
//						return true;
//					}
//				}
//			}
//
//			return false;
//		}


		/// Gets the position of the token in the grid

		/// <param name="token">The token we want to find the position of</param>
		/// <returns>Token's position in the grid in a Vector 2</returns>
		public Vector2 GetPositionOfTokenInGrid(GameObject token)
		{
			for(int x = 0; x < gridWidth; x++)
			{
				for(int y = 0; y < gridHeight ; y++)
				{
					if(gridArray[x, y] == token)
					{
						return(new Vector2(x, y));
					}
				}
			}

			// If position not found, return an empty Vector 2
			return new Vector2();
		}

	
		/// Gets grid position and translate the position into Unity world space

	
		/// <returns> A Vector2 with the grid position width and height in world space</returns>
		public Vector2 GetWorldPositionFromGridPosition(int x, int y)
		{
			return new Vector2(
				(x - gridWidth/2) * tokenSize,
				(y - gridHeight/2) * tokenSize);
		}


		/// Places tokens at positions x, y of the parent grid.

		public void AddTokenToPosInGrid(int x, int y, GameObject token, GameObject parent )
		{
//			Vector3 position = GetWorldPositionFromGridPosition(x, y);

			//  Gets random token from the tokenType array
//			GameObject token = Instantiate(tokenTypes[Random.Range(0, tokenTypes.Length)], position, Quaternion.identity) as GameObject;
			token.transform.parent = parent.transform;
			gridArray[x, y] = token;
			print ("grid array " + x + " " + y + " " + gridArray [x, y].name);
		}

		void MoveCursor(){
			// Gets the X position of the Mouse, Clamped to the grid heigh and width
			float xPos = Mathf.Clamp(Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
				GetWorldPositionFromGridPosition(0,gridHeight).x,
				GetWorldPositionFromGridPosition(gridWidth-1,gridHeight).x);
			
			// Moves cursor side to side with mouse movement
			cursor.transform.position = new Vector2 (xPos, cursor.transform.position.y);

			//Get the horizontal position of the cursor 
			GetCursorPos (xPos);
	
		
		}

		// Returns the upcoming token
		public GameObject GetNextToken(){
			return Instantiate (tokenTypes[Random.Range(0, tokenTypes.Length)],cursor.transform.position,Quaternion.identity, cursor.transform) as GameObject;
		}

		// Gets the Cursor X Position relative to the Grid array
		public void  GetCursorPos(float xPos){
			for (int i = 0; i < gridWidth; i++) {
				if (Mathf.RoundToInt (xPos) == GetWorldPositionFromGridPosition (i, gridHeight).x) {
					cursorGridXPos = i;
					
				} 

			}

		}

		public int GetEmptyPositioninColumn(int x){

			int yPos = 0;

			for (int i = 0; i < gridHeight - 1; ++i) {

				if (gridArray [x, i] == null) {
					yPos = i;
					break;
				}
			}

			return yPos;

		}
	}
}
