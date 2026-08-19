using UnityEngine;

// Base for puzzles that live in the game world and get triggered via the
// interaction system (owned separately) 
public abstract class WorldSpacePuzzle : Puzzle
{
    public override void StartPuzzle()
    {
        base.StartPuzzle();
    }
}