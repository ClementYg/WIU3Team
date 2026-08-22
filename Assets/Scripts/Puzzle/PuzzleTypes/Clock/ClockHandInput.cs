using UnityEngine;
using UnityEngine.EventSystems;

public class ClockHandInput : MonoBehaviour, IDragHandler
{
    [SerializeField] RectTransform clockCenter;
    [SerializeField] RectTransform handRect;
    [SerializeField] bool isHourHand;
    [SerializeField] int snapIncrement = 5;
    [SerializeField] EventFloat OnHandChanged; 

    float currentAngle;

    void Awake()
    {
        if (handRect == null) handRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dir = eventData.position - RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, clockCenter.position);
        float rawAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        rawAngle = (rawAngle + 360f) % 360f;

        currentAngle = Mathf.Round(rawAngle / snapIncrement) * snapIncrement;
        handRect.localEulerAngles = new Vector3(0, 0, -currentAngle);

        OnHandChanged?.Raise(GetCurrentValue());
    }

    public int GetCurrentValue()
    {
        return isHourHand
            ? Mathf.RoundToInt(currentAngle / 30f) % 12
            : Mathf.RoundToInt(currentAngle / 6f) % 60;
    }
}