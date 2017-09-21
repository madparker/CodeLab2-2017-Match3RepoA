using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HA_InputManager : InputManagerScript {

	public override void Start (){
		base.Start ();
	}

	public override void SelectToken (){
		//On mouse left button clikced to select a token;
		if(Input.GetMouseButtonDown(0)){
			//Convert mouse position to world point (in position);
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//Get the collider of the mouse clicked point (postion);
			Collider2D collider = Physics2D.OverlapPoint(mousePos);

			//Check if there any token;
			if(collider != null){

				//If there is a token, get the token's game object;
				if(selected == null){
					//If the first selected token is empty, "selected" stores the current tokan game object;
					selected = collider.gameObject;
				} else {
					//The first selected token position;
					Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
					//The current selected token positon;
					Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

					//check the distance between the 2 selected tokens' position;
					if((Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y)) == 1){
						//If the distance is 1, do the position swap;
						moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
					}

					//Clear the "selected" to null;
					selected = null;
				}
			}
		}
			
	}
}
