using UnityEngine;

public class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        if (_instance == this)
        {
            DontDestroyOnLoad(gameObject); // Same as Singleton except doesn't get Reset every SceneLoad
        }
    }
}
