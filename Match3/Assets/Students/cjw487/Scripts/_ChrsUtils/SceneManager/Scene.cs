using UnityEngine;


public class Scene<TTransitionData> : MonoBehaviour
{
    private string sceneName;
    public string SceneName
    {
        get { return sceneName; }
        private set { sceneName = value; }
    }
    private const string DELIMITER = "Script";
    public GameObject Root { get { return gameObject; } }

    internal void _OnEnter(TTransitionData data)
    {
        SceneName = transform.name;
        SceneName = SceneName.Replace(DELIMITER, "");
        Root.SetActive(true);
        OnEnter(data);
    }

    internal void _OnExit()
    {
        Root.SetActive(false);
        OnExit();
    }

    internal virtual void OnEnter(TTransitionData data) { }
    internal virtual void OnExit() { }
    //	internal virtual void Init() { }
    //	internal virtual void Destroy() { }
    //	internal virtual void Update() { }
    //	internal virtual void Tick () { }
    //	internal virtual void Render() { }


}


