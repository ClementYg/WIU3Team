using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    [Header("Overall Puzzle Settings")]
    public bool isCompleted = false;
    public string puzzleID;

    [Header("Event Channels")]
    [SerializeField] protected EventVoid OnPuzzleFinishEvent;
    [SerializeField] protected EventVoid OnPuzzleStartEvent;
    [SerializeField] protected EventVoid OnPuzzleEndEvent;

    protected virtual void CompletePuzzle()
    {
        isCompleted = true;
        //possibly add a event for onComplete
        OnPuzzleFinishEvent.Raise();
        PuzzleManager.Instance.ExitPuzzle();
    }

    public virtual void StartPuzzle()
    {
        PuzzleManager.Instance.EnterPuzzle(this);
        //possibly add a event for onEnter
        OnPuzzleStartEvent.Raise();

    }

    public virtual void ExitPuzzle()
    {
        //possibly add a event for onExit

        PuzzleManager.Instance.ExitPuzzle();
        OnPuzzleEndEvent.Raise();
    }
}
