using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang.PoppingParticlePool;

namespace Hang {
	namespace PoppingParticlePool {
		public enum ParticleType {
			Match,
			End
		}

		[System.Serializable]
		public class ParticlePoolObject {
			[SerializeField] ParticleType myParticleType;
			[SerializeField] GameObject myParticleObject;
			[SerializeField] int myPoolSize;

			public GameObject GetMyObject () {
				return myParticleObject;
			}

			public int GetMyTypeInt () {
				return (int)myParticleType;
			}

			public ParticleType GetMyType () {
				return myParticleType;
			}

			public int GetMyPoolSize () {
				return myPoolSize;
			}
		}

		public static class ParticleActions {
			public static void PlayParticle (ParticleSystem g_particleSystem, Vector3 g_position) {
				g_particleSystem.transform.position = g_position;
				g_particleSystem.Play ();
			}

			public static void PlayParticle (ParticleSystem g_particleSystem, Vector2 g_position) {
				PlayParticle (g_particleSystem, (Vector3)g_position);
			}

			/// <summary>
			/// set the first color for ColorOverLifetime. there should only be 2 color in the ColorOverLifetime
			/// </summary>
			/// <param name="myBool">Parameter value to pass.</param>
			/// <returns>Returns an integer based on the passed value.</returns>
			public static void SetFromColor (ParticleSystem g_particleSystem, Color g_color) {
				ParticleSystem.ColorOverLifetimeModule t_colm = g_particleSystem.colorOverLifetime;
				Gradient t_gradient = new Gradient ();
				t_gradient.SetKeys (new GradientColorKey[] {
					new GradientColorKey (g_color, 0.0f),
					t_colm.color.gradient.colorKeys [1]
				}, t_colm.color.gradient.alphaKeys);
				t_colm.color = t_gradient;
			}

			public static void SetColor (ParticleSystem g_particleSystem, Color g_color) {
				ParticleSystem.MainModule t_main = g_particleSystem.main;
				t_main.startColor = g_color;
			}
		}
	}
}

public class PoppingParticlePoolManager : MonoBehaviour {
	[SerializeField] ParticlePoolObject[] myParticlePoolObjects;
	private List<List<ParticleSystem>> myParticlePool = new List<List<ParticleSystem>> ();

	//========================================================================
	private static PoppingParticlePoolManager instance = null;

	public static PoppingParticlePoolManager Instance {
		get { 
			return instance;
		}
	}

	// Use this for initialization
	void Awake () {

		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
		//		DontDestroyOnLoad(this.gameObject);

		//========================================================================
	}

	// Use this for initialization
	void Start () {
		InitPool ();
	}

	private void InitPool () {
		for (int i = 0; i < (int)ParticleType.End; i++) {
			myParticlePool.Add (new List<ParticleSystem> ());
		}

		for (int i = 0; i < myParticlePoolObjects.Length; i++) {
			for (int j = 0; j < myParticlePoolObjects [i].GetMyPoolSize (); j++) {
				CreateNewParticle (myParticlePoolObjects [i]);
			}
		}
	}

	public ParticleSystem GetFromPool (ParticleType g_type) {
		List<ParticleSystem> t_pool = myParticlePool [(int)g_type];

		foreach (ParticleSystem t_particle in t_pool) {
			if (t_particle.isStopped)
				return t_particle;
		}

		Debug.Log ("no particle available, creating a new one! " + g_type);

		//find the type
		ParticlePoolObject t_particlePoolObject = null;
		foreach (ParticlePoolObject t_object in myParticlePoolObjects) {
			if (t_object.GetMyType () == g_type) {
				t_particlePoolObject = t_object;
				break;
			}
		}

		if (t_particlePoolObject == null) {
			Debug.LogError ("can not find the type: " + g_type);
			return null;
		}
			
		return CreateNewParticle (t_particlePoolObject);
	}
		
	private ParticleSystem CreateNewParticle (ParticlePoolObject g_particlePoolObject) {
		GameObject t_particleObject = Instantiate (g_particlePoolObject.GetMyObject (), this.transform) as GameObject;
		ParticleSystem t_particleSystem = t_particleObject.GetComponent<ParticleSystem> ();
		t_particleSystem.Stop ();
		myParticlePool [g_particlePoolObject.GetMyTypeInt ()].Add (t_particleSystem);
		return t_particleSystem;
	}
}



