using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolText : MonoBehaviour {

	public float sizeLerpSpeed;
	public float colorLerpSpeed;
	public Vector3 small;
	public Vector3 large;
	public Color light; 
	public Color dark; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.localScale = Vector3.Lerp(small, large, Mathf.PingPong(Time.time * sizeLerpSpeed, 1));  
		GetComponent<TextMesh>().color = Color.Lerp(light, dark, Mathf.PingPong(Time.time * sizeLerpSpeed, 1));  
	}
		
}
