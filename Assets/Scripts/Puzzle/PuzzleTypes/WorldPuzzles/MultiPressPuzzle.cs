using UnityEngine;

public abstract class MultiPressPuzzle : WorldSpacePuzzle
{
    [SerializeField] protected int totalActivationStates;

    protected bool[] activateStates;

    //HOW THIS WORKS
    //If there are 3 pressure plates, there will be 3 totalStates and 3 booleans created
    //Each pressure plate is assigned to each index, so when this detects that all 3 booleans linked to the indexes are true,
    //It will complete the puzzle.
    //You just have to create children classes that inherit this and add it as a script.

    public override void StartPuzzle()
    {
        base.StartPuzzle();
        activateStates = new bool[totalActivationStates];
    }
    public void SetActivationState(int index, bool isActive)
    {
        if (activateStates == null || index < 0 || index >= activateStates.Length) return;

        activateStates[index] = isActive;
        CheckCompletion(); //everytime u set something to active/not active, check if it had completed the puzzle or not.
    }
    void CheckCompletion()
    {
        foreach (bool state in activateStates)
        {
            if (!state) return;
        }
        CompletePuzzle();
    }
}