using UnityEngine;

public class PuzzleInteract : Interactable
{
    [SerializeField] Puzzle puzzle; 
    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.15f;
            textContent = "Solve";
            fontSize = 1f;
            initialDistanceFromCenter = 0f;
        }

        base.Start();
    }

    public override void Interact()
    {
        puzzle.StartPuzzle(puzzle.puzzleID);
    }
}
