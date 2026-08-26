using System.Collections.Generic;
using UnityEngine;

public class CompositePuzzle : Puzzle
{

    [SerializeField] List<string> puzzleIDs;
    HashSet<string> completedPuzzleIDs;

    private void OnEnable()
    {
        //OnPuzzleFinishEvent.Subscribe();
    }

    private void OnDisable()
    {
        //OnPuzzleFinishEvent.Unsubscribe();
    }


}