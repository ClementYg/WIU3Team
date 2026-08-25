using UnityEngine; 

public class LockedDoor : MonoBehaviour
{
    [SerializeField] EventVoid OnPuzzleFinishEvent;

    private void OnEnable()
    {
        OnPuzzleFinishEvent.Subscribe(PuzzleFinished);   
    }
    private void OnDisable()
    {
        OnPuzzleFinishEvent.Unsubscribe(PuzzleFinished);
    }

    private void PuzzleFinished()
    {
        Destroy(this.gameObject);
    }
}