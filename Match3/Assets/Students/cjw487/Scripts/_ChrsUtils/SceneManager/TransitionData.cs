using UnityEngine;


public class TransitionData
{
    private static TransitionData instance;
    public static TransitionData Instance
    {
        get
        {
            if (instance == null)
                instance = new TransitionData();

            return instance;
        }
        set { }
    }

    public TitleScreen TITLE;

    public struct TitleScreen
    {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
    }
}

