using UnityEngine;

public abstract class ScreenPuzzle : Puzzle
{
    [Header("UI")]
    //This is setup more so the Canvas that stores the specific puzzle will be inactive first in the scene, then just add it as reference and active later on
    [SerializeField] protected GameObject puzzleCanvas;

    public override void StartPuzzle()
    {
        base.StartPuzzle(); //run the code from inherited StartPuzzle() first
        if (puzzleCanvas != null) puzzleCanvas.SetActive(true);
    }

    protected override void CompletePuzzle(bool requestItem = false)
    {
        if (puzzleCanvas != null) puzzleCanvas.SetActive(false);
        base.CompletePuzzle();
    }

    public override void ExitPuzzle()
    {
        if (puzzleCanvas != null) { puzzleCanvas.SetActive(false); }
        base.ExitPuzzle();
    }
}
