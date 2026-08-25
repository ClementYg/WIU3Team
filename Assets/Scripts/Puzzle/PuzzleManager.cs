using UnityEngine;

public class PuzzleManager : PersistentSingleton<PuzzleManager>
{
    //possibly think of making this a list. 
    public Puzzle currentPuzzle = null;
    public bool inPuzzle => currentPuzzle != null; 

    //Returns true when successfully entered a puzzle
    public bool EnterPuzzle(Puzzle puzzle)
    {
        if (inPuzzle)
        {
            Debug.Log($"[PZL] Tried to enter {puzzle.puzzleID} but is already in {currentPuzzle.puzzleID}\n");
            //If encounter this bug, remember to exit puzzle. 
            return false;
        }
        currentPuzzle = puzzle;
        return true;
    }

    public void ExitPuzzle()
    {
        currentPuzzle = null; 
    }
}
