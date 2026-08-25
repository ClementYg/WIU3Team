using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] EventVoid onTriggerTutorialEvent;

    IEnumerator Start()
    {
        // Implementation of a "LateStart" function using coroutine

        yield return null;

        onTriggerTutorialEvent.Raise();
    }
}
