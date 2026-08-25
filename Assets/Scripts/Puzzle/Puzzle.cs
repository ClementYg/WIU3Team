using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    [Header("Overall Puzzle Settings")]
    public bool isCompleted = false;
    public string puzzleID;

    [Header("Event Channels")]
    [SerializeField] protected EventString OnPuzzleFinishEvent;
    [SerializeField] protected EventString OnPuzzleStartEvent;
    [SerializeField] protected EventString OnPuzzleExitEvent;
    [SerializeField] protected EventAlertFloat OnRequestAlert;
    [SerializeField] protected EventItem OnRequestItem;
    [SerializeField] protected Item itemPrefab;

    protected virtual void CompletePuzzle(string puzzleID, bool requestItem = false)
    {
        isCompleted = true;
        //possibly add a event for onComplete
        OnPuzzleFinishEvent.Raise(puzzleID);
        if (requestItem && itemPrefab != null)
        {
            //dont know if this works, need to check with player to see if they receive this event and get the item added. 
            OnRequestItem.Raise(itemPrefab);
        }
        OnRequestAlert.Raise(AlertType.PuzzleComplete, 0f);
        PuzzleManager.Instance.ExitPuzzle();
    }

    public virtual void StartPuzzle(string puzzleID)
    {
        PuzzleManager.Instance.EnterPuzzle(this);
        //possibly add a event for onEnter
        OnPuzzleStartEvent.Raise(puzzleID);

    }

    public virtual void ExitPuzzle(string puzzleID)
    {
        //possibly add a event for onExit

        PuzzleManager.Instance.ExitPuzzle();
        OnPuzzleExitEvent.Raise(puzzleID);
    }
}
