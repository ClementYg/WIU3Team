using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositePuzzle : Puzzle
{

    [SerializeField] List<string> puzzleIDs;
    HashSet<string> completedPuzzleIDs = new();

    [SerializeField] bool triggerEnterEvent = false;
    [SerializeField] EventVoid onEnterComposite;

    [SerializeField] bool triggerExitEvent = false;
    [SerializeField] EventVoid onExitComposite;


    public override void StartPuzzle(string puzzleID)
    {
        if (triggerEnterEvent)
        {
            onEnterComposite.Raise();
        }
        base.StartPuzzle(puzzleID);
    }
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
        completedPuzzleIDs.Add(finishedPuzzleID);
        if (HasAllCompletedPuzzles())
        {
            StartCoroutine(DelayCompletePuzzle());
            if (triggerExitEvent)
            {
                onExitComposite.Raise();
            }
        }   
    }

    IEnumerator DelayCompletePuzzle()
    {
        yield return new WaitForSeconds(2);
        CompletePuzzle(puzzleID);
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