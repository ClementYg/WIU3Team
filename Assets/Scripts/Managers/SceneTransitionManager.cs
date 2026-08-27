using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : PersistentSingleton<SceneTransitionManager>
{

    [SerializeField] GameObject FadeCanvas; 
    [SerializeField] GameObject Player; 
    bool IsTransitioning = false;
    UIFader fader;
    //we can setup find name or tag. everytime transition scene, try to find the name/tag then that will be player
    //'s new spawn position to keep persistence. 
    //we might wanna do like a tag for all objects too
    string newPosName = "spawnpos";

    protected override void Awake()
    {
        base.Awake();
        if (FadeCanvas != null)
        {
            GameObject instance = Instantiate(FadeCanvas, transform);
            instance.SetActive(true);
            fader = instance.GetComponentInChildren<UIFader>(true);
        }
    }
    public void TransitionToScene(string newScene, string spawnPos = null)
    {
        if (IsTransitioning) return;
        IsTransitioning = true; 
        if (spawnPos != null) newPosName = spawnPos;

        if (fader != null)
        {
            fader.FadeIn(()=>LoadScene(newScene));
        }
        else LoadScene(newScene);
    }

    private void LoadScene(string sceneName)
    {
        //adds sort of a bufferzone for the scene, until fully fade/load
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //cleanup the bufferzone
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (fader != null) fader.FadeOut();
        IsTransitioning = false;

        Transform playerSpawn = GameObject.Find(newPosName).transform;
        if (Player != null) Player.transform.position = playerSpawn.position;
        CinemachineCamera gameplayCam = GameObject.Find("GameplayCamera").GetComponent<CinemachineCamera>();
        gameplayCam.Follow = Player.transform;
    }

}