using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HR_UI_Play : MonoBehaviour {
	
	private static HR_UI_Play instance = null;

	//========================================================================
	public static HR_UI_Play Instance {
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
//		DontDestroyOnLoad(this.gameObject);
	}
	//========================================================================

	[SerializeField] UnityEngine.UI.Text myScoreDisplay;
	[SerializeField] Transform myTimerBackTransform;
	[SerializeField] Transform myTimerBarTransform;
	[SerializeField] SpriteRenderer myTimerBarSpriteRenderer;
	[SerializeField] Vector2 myTimerBarMoveRange = new Vector2(0, 10);
	[SerializeField] Gradient myTimerGradient;

	// Use this for initialization
	void Start () {
		myScoreDisplay.text = "0";
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	public void InitTimeBar (float g_position) {
		myTimerBackTransform.position = new Vector2 (0, g_position);
	}

	public void ShowScore (int g_score) {
		myScoreDisplay.text = g_score.ToString ("#");
	}

	public void ShowTimer (float g_percent) {
		myTimerBarTransform.localPosition = 
			new Vector2 ((myTimerBarMoveRange.y - myTimerBarMoveRange.x) * g_percent + myTimerBarMoveRange.x, 0);
		myTimerBarSpriteRenderer.color = myTimerGradient.Evaluate (g_percent);
	}
}
