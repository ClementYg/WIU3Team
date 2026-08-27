using System.Collections;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] EventVector3 onSetPlayerPositionEvent;

    [Header("Testing")]
    [SerializeField] PositionStartType playerShouldStart;
    [SerializeField] Vector3 spawnPoint = new(0f, 2f, 0f);

    private void OnEnable()
    {
        onSetPlayerPositionEvent.Subscribe(SetPlayerPosition);
    }

    private void OnDisable()
    {
        onSetPlayerPositionEvent.Unsubscribe(SetPlayerPosition);
    }

    IEnumerator Start()
    {
        yield return null;

        if (playerShouldStart == PositionStartType.AtSpawnPoint)
        {
            transform.position = spawnPoint;
        }
    }

    private void SetPlayerPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
