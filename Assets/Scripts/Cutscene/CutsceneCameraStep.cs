using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

[System.Serializable]
public class CutsceneCameraStep : CutsceneStep
{
    public string cameraID;
    public int activePriority = 20;

    public override IEnumerator Execute()
    {
        var targetCamera = CutsceneManager.GetCamera(cameraID);
        if (targetCamera == null)
        {
            Debug.LogWarning($"[CutsceneCameraStep] No camera registered with ID '{cameraID}'.");
            yield break;
        }

        CutsceneManager.Instance.SetActiveCamera(targetCamera, activePriority);

        if (blocking)
        {
            yield return null;
            yield return new WaitUntil(() => !CutsceneManager.Instance.cinemachineBrain.IsBlending);
        }
    }

    private static IEnumerator WaitForBlend(CinemachineBrain brain, CinemachineCamera targetCamera)
    {
        int safetyFrames = 10;
        while (safetyFrames-- > 0 && !brain.IsBlending && (object)brain.ActiveVirtualCamera != targetCamera)
        {
            yield return null;
        }

        if (brain.IsBlending)
        {
            yield return new WaitUntil(() => brain.IsBlending);
        }
    }
}
