using UnityEngine; 

public class LockedDoor : MonoBehaviour
{
    [SerializeField] EventString OnPuzzleFinishEvent;
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
        if (puzzle.puzzleID == puzzleID) Destroy(this.gameObject);
    }
}
