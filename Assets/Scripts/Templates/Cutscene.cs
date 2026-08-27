using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene", menuName = "ScriptableObjects/Cutscene")]
public class Cutscene : ScriptableObject
{
    [SerializeReference] public List<CutsceneStep> steps = new();

    [Header("Event Channels")]
    [SerializeField] EventVoid onCutsceneEndedEvent;

    public void RaiseEvent()
    {
        if (onCutsceneEndedEvent == null) return;
        onCutsceneEndedEvent.Raise();
    }
}
