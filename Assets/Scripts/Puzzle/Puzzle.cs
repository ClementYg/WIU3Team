using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public bool isCompleted = false;
    public string puzzleID; 

    protected virtual void CompletePuzzle()
    {
        isCompleted = true;
    }

    public virtual void EnterPuzzle()
    {
        PuzzleManager.Instance.EnterPuzzle(this);

    }

    public virtual void ExitPuzzle()
    {
        PuzzleManager.Instance.ExitPuzzle(this);
    }
}
