using UnityEngine; 

public class LockedDoor : MonoBehaviour
{
    [SerializeField] EventString OnPuzzleFinishEvent;
    [SerializeField] EventVoid onUnlockDoorEvent;
    [SerializeField] Puzzle puzzle;

    private void OnEnable()
    {
        OnPuzzleFinishEvent.Subscribe(PuzzleFinished);   
    }
    private void OnDisable()
    {
        OnPuzzleFinishEvent.Unsubscribe(PuzzleFinished);
    }

    private void PuzzleFinished(string puzzleID)
    {
        if (puzzle.puzzleID == puzzleID)
        {
            // Raise the event for closed door
            if (onUnlockDoorEvent != null)onUnlockDoorEvent.Raise();

            Destroy(this.gameObject);
        }
    }
}
