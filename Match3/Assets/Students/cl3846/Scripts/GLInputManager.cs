using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLInputManager : InputManagerScript {
    private Vector2 tokenInGrid;
    private Vector2 mouseSelectPosition;

    public override void Start() {
        base.Start();
    }

    //Click on a token to to select;
    public override void SelectToken() {

        if (GLGameManager.instance.gameIsOver) {
            return;
        }

        //On mouse left button clikced to select a token;
        if (Input.GetMouseButtonDown(0)) {
            //Convert mouse position to world point (in position);
            mouseSelectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Get the collider of the mouse clicked point (postion);
            Collider2D collider = Physics2D.OverlapPoint(mouseSelectPosition);

            //Check if there any token;
            if (collider != null) {

                //The current selected token positon in grid;
                tokenInGrid = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

                //If there is a token, get the token's game object;
                if (selected == null) {
                    //If the first selected token is empty, "selected" stores the current tokan game object;
                    selected = collider.gameObject;
                } else {
                    //The first selected token position;
                    Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
                    Vector2 pos2 = tokenInGrid;

                    //check the distance between the 2 selected tokens' position;
                    if (Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y) == 1) {
                        //If the distance is 1, do the position swap;
                        moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
                        //							Debug.Log (selected.name + " " + pos1 + "-" + collider.gameObject.name + " " + pos2);

                        //Clear the "selected" to null;
                        selected = null;
                    } else {
                        selected = collider.gameObject;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) && selected != null) {
            //get mouse position in world space
            Vector2 t_mouseMove = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseSelectPosition;

            if (t_mouseMove.sqrMagnitude > 1) {

                GameObject t_targetGameObject = this.gameObject;

                if (Mathf.Abs(t_mouseMove.x) > Mathf.Abs(t_mouseMove.y)) {
                    //horizontal move

                    if (t_mouseMove.x > 0 && tokenInGrid.x < gameManager.gridWidth - 1) {
                        //swap right
                        //get target game object
                        t_targetGameObject = gameManager.gridArray[(int)tokenInGrid.x + 1, (int)tokenInGrid.y];
                        moveManager.SetupTokenExchange(
                            selected, tokenInGrid,
                            t_targetGameObject, tokenInGrid + Vector2.right,
                            true
                        );

                        //							Debug.Log (selected.name + " " + mySelectTokenInGrid + "-" + t_targetGameObject.name + " " + (mySelectTokenInGrid + Vector2.right));
                    } else if (t_mouseMove.x < 0 && tokenInGrid.x > 0) {
                        //swap left//get target game object
                        t_targetGameObject = gameManager.gridArray[(int)tokenInGrid.x - 1, (int)tokenInGrid.y];
                        moveManager.SetupTokenExchange(
                            selected, tokenInGrid,
                            t_targetGameObject, tokenInGrid + Vector2.left,
                            true
                        );
                    }
                } else {
                    //vertical move

                    if (t_mouseMove.y > 0 && tokenInGrid.y < gameManager.gridHeight - 1) {
                        //swap up
                        //get target game object
                        t_targetGameObject = gameManager.gridArray[(int)tokenInGrid.x, (int)tokenInGrid.y + 1];
                        moveManager.SetupTokenExchange(
                            selected, tokenInGrid,
                            t_targetGameObject, tokenInGrid + Vector2.up,
                            true
                        );
                    } else if (t_mouseMove.y < 0 && tokenInGrid.y > 0) {
                        //swap left//get target game object
                        t_targetGameObject = gameManager.gridArray[(int)tokenInGrid.x, (int)tokenInGrid.y - 1];
                        moveManager.SetupTokenExchange(
                            selected, tokenInGrid,
                            t_targetGameObject, tokenInGrid + Vector2.down,
                            true
                        );
                    }
                }

                //Clear the "selected" to null;
                selected = null;
            }
        }
    }
}

