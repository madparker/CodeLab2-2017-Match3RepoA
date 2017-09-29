using UnityEngine;
using System.Collections;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	CameraShake: Shakes the camera.														*/
/*			HOW TO SET UP:																*/
/*				+	Create an empty GameObject.											*/
/*				+	Tag the newly created empty GameObject with "Camera"				*/
/*				+	Set MainCamera to be a child of the newly created GameObject		*/
/*																						*/
/*			HOW TO USE:																	*/
/*				+	CameraShake.CameraShakeEffect.Shake(0.1f, 0.25f);					*/
/*																						*/
/*		Functions:																		*/
/*			Start ()																	*/
/*			Shake (float amount, float lenght)											*/
/*			DoShake ()																	*/
/*			StopShake ()																*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class CameraShake : MonoBehaviour 
{
	public static CameraShake CameraShakeEffect;				//	Instance of the CameraShakeEffect
	private Vector3 endShakePosition = Vector3.zero;			//	Where the camera ends up after the shake
	public Vector3 EndShakePosition							
	{
		get { return endShakePosition;}
		set { endShakePosition = value;}
	}

	//	Public Variables
	public Camera mainCamera; 									//	Refernce to Main camera
	public float shakeAmount = 0;								//	How violent the shake is

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start()
	{
		if (mainCamera == null)
		{
			mainCamera = Camera.main;
		}

		if (CameraShakeEffect == null)
		{
			CameraShakeEffect = GameObject.FindGameObjectWithTag ("Camera").GetComponent<CameraShake> ();
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Shake: Starts the beginning of the shake											*/
	/*		param:	float amount - how violent the shake is									*/
	/*				float length - how many seconds the shake goes on for					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void Shake(float amount, float length)
	{
		shakeAmount = amount;
		InvokeRepeating ("DoShake", 0, 0.01f);
		Invoke ("StopShake", length);
	}	

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	DoShake: The actual shaking happens here											*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void DoShake()
	{
		if (shakeAmount > 0)
		{
			Vector3 cameraPosition = mainCamera.transform.position;
			float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
			cameraPosition.x += offsetX;
			cameraPosition.y += offsetY;	

			mainCamera.transform.position = cameraPosition;
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	StopShake: Stops the camera shaking													*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void StopShake()
	{
		CancelInvoke ("DoShake");
		mainCamera.transform.localPosition = new Vector3(EndShakePosition.x, EndShakePosition.y, EndShakePosition.z);
	}		
}

