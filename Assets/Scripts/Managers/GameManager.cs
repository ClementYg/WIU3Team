using UnityEngine;
using System.Collections;

enum StartType
{
    AtSpawnPoint,
    AtCurrentPosition
}

public class GameManager : PersistentSingleton<GameManager>
{
    [Header("Tutorial")]
    [SerializeField] TutorialData introTutorial;

    [Header("Testing")]
    [SerializeField] Transform playerTransform;
    [SerializeField] StartType playerShouldStart;
    [SerializeField] Vector3 playerSpawnPoint = new(0f, 2f, 0f);
    [SerializeField] bool shouldStartTutorial = true;

    [Header("Event Channels")]
    [SerializeField] EventTutorialData onTriggerTutorialEvent;

    IEnumerator Start()
    {
        // Implementation of a "LateStart" function using coroutine

        yield return null;

        if (playerShouldStart == StartType.AtSpawnPoint)
        {
            playerTransform.position = playerSpawnPoint;
        }

        if (shouldStartTutorial)
        {
            onTriggerTutorialEvent.Raise(introTutorial);
        }
    }
}
