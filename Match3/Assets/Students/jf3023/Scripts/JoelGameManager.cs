using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoelGameManager : GameManagerScript {

    // Use this for initialization
    public override void Start()
    {
        tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
        tokenTypes = (Object[])Resources.LoadAll("jf3023/Tokens/");
        gridArray = new GameObject[gridWidth, gridHeight];
        MakeGrid();
        matchManager = GetComponent<MatchManagerScript>();
        inputManager = GetComponent<InputManagerScript>();
        repopulateManager = GetComponent<RepopulateScript>();
        moveTokenManager = GetComponent<MoveTokensScript>();
    }

    	
}
