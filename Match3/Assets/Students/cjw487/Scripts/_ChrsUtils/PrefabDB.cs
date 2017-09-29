using UnityEngine;


[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject
{

    [SerializeField] private GameObject _player;
    public GameObject Player
    {
        get { return _player; }
    }

    [SerializeField] private GameObject[] _token;
    public GameObject[] Token
    {
        get { return _token; }
    }

    [SerializeField] private GameObject[] _scenes;
    public GameObject[] Scenes
    {
        get { return _scenes; }
    }
}
