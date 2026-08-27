using UnityEngine;

public class AtlantisCutscenes : MonoBehaviour
{
    [Header("Key Cutscene")]
    [SerializeField] private EventVoid onStartCompositePuzzle;
    [SerializeField] private Cutscene compositePuzzle;
    [Header("Exit Cutscene")]
    [SerializeField] private EventVoid onExitCompositePuzzle;
    [SerializeField] private Cutscene ExitCompositePuzzle;

    private void OnEnable()
    {
        onStartCompositePuzzle.Subscribe(OnEnterCompositeCutscene);
        onExitCompositePuzzle.Subscribe(OnExitCompositeCutscene);
    }

    private void OnDisable()
    {
        onStartCompositePuzzle.Unsubscribe(OnEnterCompositeCutscene);
        onExitCompositePuzzle.Unsubscribe(OnExitCompositeCutscene);
    
    }

    private void OnEnterCompositeCutscene()
    {
        CutsceneManager.Instance.Play(compositePuzzle);
    }
    private void OnExitCompositeCutscene()
    {
        CutsceneManager.Instance.Play(ExitCompositePuzzle);
    }
}
