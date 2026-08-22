using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CutsceneCameraTag : MonoBehaviour
{
    [SerializeField] private string cameraID;
    private new CinemachineCamera camera;

    private void Awake()
    {
        camera = GetComponent<CinemachineCamera>();
    }

    private void OnEnable()
    {
        CutsceneManager.RegisterCamera(cameraID, camera);
    }

    private void OnDisable()
    {
        CutsceneManager.UnregisterCamera(cameraID);
    }
}
