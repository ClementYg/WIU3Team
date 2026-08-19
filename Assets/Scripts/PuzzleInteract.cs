using UnityEngine;

public class PuzzleInteract : Interactable
{
    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.15f;
            textContent = "Solve";
            fontSize = 1f;
            initialDistanceFromCenter = 1.7f;
        }

        base.Start();
    }

    public override void Interact()
    {
        // Code to trigger puzzle
    }
}
