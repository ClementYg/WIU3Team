using UnityEngine;
public class BestiaryTestDebug : MonoBehaviour
{
    [SerializeField] BestiaryDatabase database;
    [SerializeField] string testEntryID; // must match an existing entry's EntryID exactly
    [SerializeField] EventAlertFloat OnRequestAlertEvent;
    void Start()
    {
        Debug.Log($"Before unlock - IsUnlocked({testEntryID}): {BestiaryManager.Instance.IsUnlocked(testEntryID)}");

        BestiaryManager.Instance.Unlock(testEntryID);

        Debug.Log($"After unlock - IsUnlocked({testEntryID}): {BestiaryManager.Instance.IsUnlocked(testEntryID)}");
        Debug.Log($"Completion: {BestiaryManager.Instance.GetCompletionPercent(database) * 100f:0.0}%");

        OnRequestAlertEvent.Raise(AlertType.PuzzleComplete, 0f);
    }
}