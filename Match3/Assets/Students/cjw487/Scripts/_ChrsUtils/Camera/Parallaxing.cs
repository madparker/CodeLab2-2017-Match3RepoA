using UnityEngine;
using System.Collections;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	Parallaxing: Applies parallaxing to the background									*/
/*																						*/
/*		Functions:																		*/
/*			public:																		*/
/*																						*/
/*			private:																	*/
/*				void Awake																*/
/*				void Start () 															*/
/*				void LateUpdate ()														*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class Parallaxing : MonoBehaviour 
{

	//	Public Variables
	public float smoothing = 1f;		//	How smooth the parallax is going to be. Set it above 0.
	public Transform[] backgrounds; 	//	List of all the back and foregrounds to be parallaxed

	//	Private Variables
	private float[] m_ParallaxScales; 	//	Porportion of th camera's movement to move background by
	private Transform m_cam;			//	Reference to main cmaera's trnasform
	private Vector3 m_previousCamPos;	//	Stores camera position in previus frame

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Awake: Runs once at the begining of the game before Start							*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Awake	()
	{
		m_cam = Camera.main.transform;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		// stores previous position
		m_previousCamPos = m_cam.position;
		// assigning corresponding parallax scales
		m_ParallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++)
		{
			m_ParallaxScales [i] = backgrounds [i].position.z * -1;
		}
	}
	
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	LateUpdate: Runs once per frame after Update 										*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void LateUpdate () 
	{
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			Vector3 parallax = (m_previousCamPos - m_cam.position) * (m_ParallaxScales [i] / smoothing);

			// set a target x positon which is the current position + parallax
			//float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// create a target position which is background's current position with target's x position
			//Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current position and target position using lerp
			backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
		}	

		// set previous cam position to the cmaera's position at the end of the frame
		m_previousCamPos = m_cam.position;
	}
}

