using System.Collections.Generic;
using UnityEngine;

public class Cutscene : ScriptableObject
{
    [SerializeReference] public List<CutsceneStep> steps = new();
}
