using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class CompositePuzzle : Puzzle
{

    [SerializeField] List<string> puzzleIDs;
    HashSet<string> completedPuzzleIDs = new();

    private void OnEnable()
    {
        OnPuzzleFinishEvent.Subscribe(HasAnyPuzzleFinished);
    }

    private void OnDisable()
    {
        OnPuzzleFinishEvent.Unsubscribe(HasAnyPuzzleFinished);
    }

    private void HasAnyPuzzleFinished(string finishedPuzzleID)
    {
        if (isCompleted) return;    
        if (!puzzleIDs.Contains(finishedPuzzleID)) return;
        Debug.LogWarning(finishedPuzzleID);
        completedPuzzleIDs.Add(finishedPuzzleID);
        if (HasAllCompletedPuzzles())
        {
            CompletePuzzle(puzzleID);
        }   
    }

    private bool HasAllCompletedPuzzles()
    {
        //check if all puzzles required is inside completed.
        foreach(string puzzleID in puzzleIDs)
        {
            if (!completedPuzzleIDs.Contains(puzzleID)) return false;
        }
        return true;
    }

}