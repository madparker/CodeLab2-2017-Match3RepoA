using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepThisGuyAround : MonoBehaviour {

	public static GameObject instance;

	// Use this for initialization
	void Start () {

		if (instance == null) {
			instance = this.gameObject;
		} else {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (instance);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
