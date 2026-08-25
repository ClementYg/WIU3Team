using UnityEngine;
using System.Collections;

public class GameManager : PersistentSingleton<GameManager>
{
    [Header("Game Manager")]
    [SerializeField] bool shouldStartTutorial = true;

    [Header("Event Channels")]
    [SerializeField] EventVoid onTriggerTutorialEvent;

    IEnumerator Start()
    {
        // Implementation of a "LateStart" function using coroutine

        yield return null;

        if (shouldStartTutorial)
        {
            onTriggerTutorialEvent.Raise();
        }
    }
}
