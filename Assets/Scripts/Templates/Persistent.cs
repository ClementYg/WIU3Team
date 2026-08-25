using UnityEngine;

public abstract class Persistent<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
