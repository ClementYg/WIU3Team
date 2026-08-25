using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [Header("Event Channels")]
    public EventVoid onDialogueEndedEvent;
    public EventAudioClip onBGMRequestEvent;
    public EventAudioClip onSFXRequestEvent;
    public EventBool onToggledCutsceneModeEvent;

    [Header("Camera")]
    public CinemachineBrain cinemachineBrain;
    private static readonly Dictionary<string, CinemachineCamera> cameraRegistry = new();
    private const int InactiveCameraPriority = 0;
    private CinemachineCamera activeCutsceneCamera;
    private Coroutine playRoutine;

    public CinemachineCamera ActiveCutsceneCamera => activeCutsceneCamera;

    public static void RegisterCamera(string id, CinemachineCamera camera)
    {
        if (string.IsNullOrEmpty(id)) return;
        cameraRegistry[id] = camera;
    }

    public static void UnregisterCamera(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        cameraRegistry.Remove(id);
    }

    public static CinemachineCamera GetCamera(string id)
    {
        cameraRegistry.TryGetValue(id, out var camera);
        return camera;
    }

    public void SetActiveCamera(CinemachineCamera camera, int priority)
    {
        if (activeCutsceneCamera != null && activeCutsceneCamera != camera) SetPriority(activeCutsceneCamera, InactiveCameraPriority);

        SetPriority(camera, priority);
        activeCutsceneCamera = camera;
    }

    private static void SetPriority(CinemachineCamera camera, int value)
    {
        var currentPriority = camera.Priority;
        currentPriority.Value = value;
        camera.Priority = currentPriority;
    }

    public void Play(Cutscene cutscene)
    {
        if (playRoutine != null) StopCoroutine(playRoutine);
        onToggledCutsceneModeEvent.Raise(true);
        playRoutine = StartCoroutine(RunCutscene(cutscene));
    }

    private IEnumerator RunCutscene(Cutscene cutscene)
    {
        foreach (var step in cutscene.steps)
        {
            var exec = step.Execute();
            if (step.blocking) yield return StartCoroutine(exec);
            else StartCoroutine(exec);
        }

        if (activeCutsceneCamera != null)
        {
            SetPriority(activeCutsceneCamera, InactiveCameraPriority);
            activeCutsceneCamera = null;
        }

        onToggledCutsceneModeEvent.Raise(false);
        playRoutine = null;
    }
}
