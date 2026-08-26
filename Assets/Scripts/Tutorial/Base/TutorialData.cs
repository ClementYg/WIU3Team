using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "ScriptableObjects/Tutorial/TutorialData")]
public class TutorialData : ScriptableObject
{
    [Header("Tutorial Data")]
    public StepData firstStep;
    public ItemData itemData;
    public ItemEffect itemEffect;

    [Header("Event Channels")]
    public EventItemDataItemEffect onAddItemEvent;

    public void RaiseEvent()
    {
        if (itemData != null && itemEffect != null && onAddItemEvent != null)
        {
            onAddItemEvent.Raise(itemData, itemEffect);
        }
        else
        {
            Debug.Log("TutorialData: Missing event channel reference.");
        }
    }
}
