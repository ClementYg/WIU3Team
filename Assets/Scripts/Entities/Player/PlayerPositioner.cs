using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] EventVector3 onSetPlayerPositionEvent;

    private void OnEnable()
    {
        onSetPlayerPositionEvent.Subscribe(SetPlayerPosition);
    }

    private void OnDisable()
    {
        onSetPlayerPositionEvent.Unsubscribe(SetPlayerPosition);
    }

    private void SetPlayerPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
