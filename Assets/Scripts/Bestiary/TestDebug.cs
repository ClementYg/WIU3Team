using UnityEngine;
using UnityEngine.InputSystem;
public class BestiaryTestDebug : MonoBehaviour
{
    [SerializeField] BestiaryDatabase database;
    [SerializeField] string testEntryID; //must match an existing entry's EntryID exactly
    [SerializeField] EventAlertFloat OnRequestAlertEvent;
    [SerializeField] string testAreaEntryID; //must match an AreaEntryData's entryID exactly
    void Start()
    {
        Debug.Log($"Before unlock - IsUnlocked({testEntryID}): {BestiaryManager.Instance.IsUnlocked(testEntryID)}");

        BestiaryManager.Instance.Unlock(testEntryID);

        Debug.Log($"After unlock - IsUnlocked({testEntryID}): {BestiaryManager.Instance.IsUnlocked(testEntryID)}");
        Debug.Log($"Completion: {BestiaryManager.Instance.GetCompletionPercent(database) * 100f:0.0}%");

        OnRequestAlertEvent.Raise(AlertType.PuzzleComplete, 0f);

        BestiaryManager.Instance.Unlock(testAreaEntryID);
        //MapManager.Instance.OpenMap();
    }
}