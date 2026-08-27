using UnityEngine;

public class EasterCutscenes : MonoBehaviour
{
    [Header("Key Cutscene")]
    [SerializeField] private EventVoid onEnterKeyCutscene;
    [SerializeField] private Cutscene keyCutscene;

    [Header("Joe Cutscene")]
    [SerializeField] private EventVoid onEnterJoeCutscene;
    [SerializeField] private Cutscene joeCutscene;

    [Header("Golden Cutscene")]
    [SerializeField] private EventVoid onEnterGoldenCutscene;
    [SerializeField] private Cutscene goldenCutscene;

    private void OnEnable()
    {
        onEnterKeyCutscene.Subscribe(OnEnterKeyCutscene);
        onEnterJoeCutscene.Subscribe(OnEnterJoeCutscene);
        onEnterGoldenCutscene.Subscribe(OnEnterGoldenCutscene);
    }

    private void OnDisable()
    {
        onEnterKeyCutscene.Unsubscribe(OnEnterKeyCutscene);
        onEnterJoeCutscene.Unsubscribe(OnEnterJoeCutscene);
        onEnterGoldenCutscene.Unsubscribe(OnEnterGoldenCutscene);
    }

    private void OnEnterKeyCutscene()
    {
        CutsceneManager.Instance.Play(keyCutscene);
    }

    private void OnEnterJoeCutscene()
    {
        CutsceneManager.Instance.Play(joeCutscene);
    }
    
    private void OnEnterGoldenCutscene()
    {
        CutsceneManager.Instance.Play(goldenCutscene);
    }
}
