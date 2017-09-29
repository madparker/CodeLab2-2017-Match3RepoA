using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GLGameManager : GameManagerScript {
    public static GLGameManager instance;
    [SerializeField]
    private Text scoreUI;
    [SerializeField]
    private Text timerUI;
    [SerializeField]
    private Text gameOverUI;
    private int score;
    private float time = 180;

    public bool gameIsOver = false;

    public override void Start() {
        instance = this;
        tokenTypes = (Object[])Resources.LoadAll("cl3846");
        gridArray = new GameObject[gridWidth, gridHeight];
        MakeGrid();
        matchManager = GetComponent<GLMatchManager>();
        inputManager = GetComponent<GLInputManager>();
        repopulateManager = GetComponent<RepopulateScript>();
        moveTokenManager = GetComponent<MoveTokensScript>();
    }

    public override void Update() {
        base.Update();

        string second;

        time -= Time.deltaTime;

        if (time <= 0) {
            gameIsOver = true;
            gameOverUI.gameObject.SetActive(true);
            return;
        }

        if (Mathf.Floor(time % 60) < 10) {
            second = "0" + (Mathf.Floor(time % 60)).ToString();
        } else {
            second = (Mathf.Floor(time % 60)).ToString();
        }

        timerUI.text = "Time: " + (Mathf.Floor(time / 60)).ToString() + ":" + second;

        score += GLMatchManager.instance.RemoveMatches();
        scoreUI.text = "Score: " + score.ToString();
    }
}
