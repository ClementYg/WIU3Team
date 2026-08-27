using UnityEngine;
using System.Collections;

public class GameManager : PersistentSingleton<GameManager>
{
    [Header("Tutorial")]
    [SerializeField] TutorialData introTutorial;

    [Header("Testing")]
    [SerializeField] bool shouldStartTutorial = true;

    [Header("Event Channels")]
    [SerializeField] EventTutorialData onTriggerTutorialEvent;

    IEnumerator Start()
    {
        // Implementation of a "LateStart" function using coroutine

        yield return null;

        if (shouldStartTutorial)
        {
            onTriggerTutorialEvent.Raise(introTutorial);
        }
    }
}
