using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public bool isCompleted = false;
    public string puzzleID; 

    protected virtual void CompletePuzzle()
    {
        isCompleted = true;
        //possibly add a event for onComplete
        PuzzleManager.Instance.ExitPuzzle();
    }

    public virtual void StartPuzzle()
    {
        PuzzleManager.Instance.EnterPuzzle(this);
        //possibly add a event for onEnter

    }

    public virtual void ExitPuzzle()
    {
        //possibly add a event for onExit

        PuzzleManager.Instance.ExitPuzzle();
    }
}
