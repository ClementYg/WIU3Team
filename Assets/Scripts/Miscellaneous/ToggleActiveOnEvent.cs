using UnityEngine;

public class ToggleActiveOnEvent : MonoBehaviour
{
    [SerializeField] private GameObject otherGameObject;
    [SerializeField] private EventVoid onToggledEvent;
    [SerializeField] private bool isActive;
    [SerializeField] private bool isOtherActive;

    private void OnEnable()
    {
        onToggledEvent.Subscribe(OnToggled);
    }

    private void OnDisable()
    {
        onToggledEvent.Unsubscribe(OnToggled);
    }

    private void OnToggled()
    {
        if (otherGameObject != null) otherGameObject.SetActive(isOtherActive);
        this.gameObject.SetActive(isActive);
    }
}
