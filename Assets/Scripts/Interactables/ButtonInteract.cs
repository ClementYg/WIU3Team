using UnityEngine;
using System.Collections.Generic;

public class ButtonInteract : Interactable
{
    [Header("Event Channels")]
    [SerializeField] private EventGameObject onButtonPressedEvent;

    [Header("Interactions")]
    [SerializeField] private List<Interaction> pressedInteractions;

    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.2f;
            textContent = "Press";
            fontSize = 1f;
            initialDistanceFromCenter = 0f;
        }

        base.Start();
    }
    
    public override void Interact()
    {
        onButtonPressedEvent.Raise(this.gameObject);
        foreach (Interaction interaction in pressedInteractions)
        {
            interaction.Do();
        }
    }
}
