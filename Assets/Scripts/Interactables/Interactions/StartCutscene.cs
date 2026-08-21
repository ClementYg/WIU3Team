using UnityEngine;

[CreateAssetMenu(fileName = "StartCutscene", menuName = "ScriptableObjects/Interaction/StartCutscene")]
public class StartCutscene : Interaction
{
    [SerializeField] private Cutscene cutscene;
    
    public override void Do()
    {
        CutsceneManager.Instance.Play(cutscene);
    }
}
