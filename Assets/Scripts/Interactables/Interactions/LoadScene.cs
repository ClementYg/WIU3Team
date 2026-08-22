using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadScene", menuName = "ScriptableObjects/Interaction/LoadScene")]
public class LoadScene : Interaction
{
    [SerializeField] string sceneName;

    public override void Do()
    {
        SceneManager.LoadScene(sceneName);
    }
}
