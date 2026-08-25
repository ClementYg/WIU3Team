using System.Collections.Generic;
using UnityEngine;

public class InspectInteract : Interactable
{
    [Header("Interactions")]
    [SerializeField] private List<Interaction> inspectInteractions;
    
    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.2f;
            textContent = "Inspect";
            fontSize = 1f;
            initialDistanceFromCenter = 0f;
        }

        base.Start();
    }

    public override void Interact()
    {
        foreach (Interaction interaction in inspectInteractions)
        {
            interaction.Do();
        }
    }
}
