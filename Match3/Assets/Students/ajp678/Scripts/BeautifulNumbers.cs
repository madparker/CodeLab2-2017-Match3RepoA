using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautifulNumbers : MonoBehaviour {

	TextMesh tm;
	public float expandedScale;
	public int displayNum;
	public float scaleTime;

	// Use this for initialization
	void Start () {

		tm = GetComponent<TextMesh> ();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		tm.text = displayNum.ToString ();
		StartCoroutine (scaleOverTime(scaleTime)); 
	}

	IEnumerator scaleOverTime (float time)
	{
		Color originalColor = tm.color;
		Color targetColor = tm.color;
		targetColor.a = 0; 
		Vector3 originalScale = gameObject.transform.localScale;
		Vector3 targetScale = new Vector3 (expandedScale, expandedScale, expandedScale);
		float currentTime = 0.0f;

		do
		{
			gameObject.transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time);
			tm.color = Color.Lerp (originalColor, targetColor, currentTime/time); 
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);

		Destroy(gameObject);
	}
}
