using UnityEngine;

public class IDManager : MonoBehaviour
{
    int currentID = 0;

    public int RequestID()
    {
        return currentID++;
    }
}
