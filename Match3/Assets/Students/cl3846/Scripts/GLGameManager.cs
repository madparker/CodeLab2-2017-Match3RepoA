using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLGameManager : GameManagerScript {

    public override void Start() {
        tokenTypes = (Object[])Resources.LoadAll("cl3846");
        gridArray = new GameObject[gridWidth, gridHeight];
        MakeGrid();
        matchManager = GetComponent<GLMatchManager>();
        inputManager = GetComponent<GLInputManager>();
        repopulateManager = GetComponent<RepopulateScript>();
        moveTokenManager = GetComponent<MoveTokensScript>();
    }

}
