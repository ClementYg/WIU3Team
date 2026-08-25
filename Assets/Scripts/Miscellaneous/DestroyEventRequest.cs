using UnityEngine;

public class DestroyEventRequest : MonoBehaviour
{
    [SerializeField] private EventVoid onDestroyedEvent;

    private void OnDestroy()
    {
        onDestroyedEvent.Raise();
    }
}
