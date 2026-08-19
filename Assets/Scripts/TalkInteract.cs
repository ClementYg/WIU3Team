using UnityEngine;

public class TalkInteract : Interactable
{
    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.15f;
            textContent = "Talk";
            fontSize = 1f;
            initialDistanceFromCenter = 1.5f;
        }

        base.Start();
    }

    public override void Interact()
    {
        // Code to trigger dialogue
    }
}
