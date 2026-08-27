using UnityEngine;

[CreateAssetMenu(fileName = "UITimer", menuName = "ScriptableObjects/UI/UITimer")]
public class UITimer : ScriptableObject
{
    [Header("UI Timer")]
    public float timeAtStart = 20f;

    [Header("Event Channels")]
    public EventVoid onTimerEndedEvent;
}
