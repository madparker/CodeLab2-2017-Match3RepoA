using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnrewInputManager : InputManagerScript {

	public int swaps;
	public int maxSwaps;
	public Text display;

	void Update()
	{
		display.text = "Swaps: " + swaps.ToString ();
	}

	public override void SelectToken(){
		if(Input.GetMouseButtonDown(0)){
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Collider2D collider = Physics2D.OverlapPoint(mousePos);

			if(collider != null){

				if(selected == null){
					selected = collider.gameObject;
				} else {
					Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
					Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);
					//if the Players still has swaps to make, they can switch the position in the grid without it autoswitching back.
					if((Mathf.Abs(pos1.x - pos2.x)) + (Mathf.Abs(pos1.y - pos2.y)) == 1){
						if (swaps > 0) {
							moveManager.SetupTokenExchange (selected, pos1, collider.gameObject, pos2, false);
							swaps--; //If the player makes a non-matching switch, reduce the number of swaps they have left.
						} else {
							//if the player has no swaps left, the tokens will lerp back to their original position.
							moveManager.SetupTokenExchange (selected, pos1, collider.gameObject, pos2, true);
						}
					}
					selected = null;
				}
			}
		}

	}
}
