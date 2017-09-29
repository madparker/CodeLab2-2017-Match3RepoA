using UnityEngine.Assertions;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int _numPlayers;
    public int NumPlayers
    {
        get { return _numPlayers; }
        private set
        {
            if (_numPlayers <= 0)
            {
                _numPlayers = 1;
            }
            else
            {
                _numPlayers = value;
            }
        }
    }

    [SerializeField] private Camera _mainCamera;
    public Camera MainCamera
    {
        get { return _mainCamera; }
    }

    public void Init()
    {
        NumPlayers = 1;
        _mainCamera = Camera.main;
    }

	// Use this for initialization
	public void Init (int players)
    {
        NumPlayers = players;
        _mainCamera = Camera.main;
	}
	
    public void ChangeCameraTo(Camera camera)
    {
        _mainCamera = camera;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
