using UnityEngine;

public class PuzzleItemOn : MonoBehaviour
{
    [SerializeField] private EventString onPuzzleEndEvent;
    [SerializeField] private string puzzleID;
    [SerializeField] private GameObject otherGameObject;
    [SerializeField] private bool isOtherActive;

    private void OnEnable()
    {
        onPuzzleEndEvent.Subscribe(OnPuzzleEndEvent);
    }

    private void OnDisable()
    {
        onPuzzleEndEvent.Unsubscribe(OnPuzzleEndEvent);
    }

    private void OnPuzzleEndEvent(string puzzleID)
    {
        if (this.puzzleID == puzzleID)
        {
            otherGameObject.SetActive(isOtherActive);
        }
    }
}
