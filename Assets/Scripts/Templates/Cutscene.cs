using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene", menuName = "ScriptableObjects/Cutscene")]
public class Cutscene : ScriptableObject
{
    [SerializeReference] public List<CutsceneStep> steps = new();
}
