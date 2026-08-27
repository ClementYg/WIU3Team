using UnityEngine;

[CreateAssetMenu(fileName = "TransitionScene", menuName = "ScriptableObjects/Interaction/TransitionScene")]
public class TransitionScene : Interaction
{
    [SerializeField] string sceneName;
    [SerializeField] string spawnPos;

    public override void Do()
    {
        SceneTransitionManager.Instance.TransitionToScene(sceneName, spawnPos);
    }
}
