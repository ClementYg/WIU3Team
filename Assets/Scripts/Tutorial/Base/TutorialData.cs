using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "ScriptableObjects/Tutorial/TutorialData")]
public class TutorialData : ScriptableObject
{
    [Header("Tutorial Data")]
    public StepData firstStep;
}
