using UnityEngine;

public class ItemInteract : Interactable
{
    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.15f;
            textContent = "Collect";
            fontSize = 1f;
            initialDistanceFromCenter = 0f;
        }
        
        base.Start();
    }
    
    public override void Interact()
    {
        // Code to add item into inventory maybe?
    }
}
