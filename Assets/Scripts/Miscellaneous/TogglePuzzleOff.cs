using UnityEngine;

public class TogglePuzzleOff : MonoBehaviour
{
    [SerializeField] private EventString onPuzzleEndEvent;
    [SerializeField] private string puzzleID;

    private void OnEnable()
    {
        onPuzzleEndEvent.Subscribe(OnPuzzleEnded);
    }

    private void OnDisable()
    {
        onPuzzleEndEvent.Unsubscribe(OnPuzzleEnded);
    }

    private void OnPuzzleEnded(string puzzleID)
    {
        if (this.puzzleID == puzzleID)
        {
            Destroy(this.gameObject);
        }
    }
}
