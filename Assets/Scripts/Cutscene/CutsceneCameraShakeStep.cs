using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

[System.Serializable]
public class CutsceneCameraShakeStep : CutsceneStep
{
    public string cameraID;

    [Header("Shake Settings")]
    public float duration = 0.5f;
    public float amplitude = 1f;
    public float frequency = 1f;

    public override IEnumerator Execute()
    {
        var camera = CutsceneManager.GetCamera(cameraID);
        if (camera == null)
        {
            yield break;
        }

        var perlin = camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null)
        {
            yield break;
        }

        float originalAmplitude = perlin.AmplitudeGain;
        float originalFrequency = perlin.FrequencyGain;

        perlin.AmplitudeGain = amplitude;
        perlin.FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        perlin.AmplitudeGain = originalAmplitude;
        perlin.FrequencyGain *= originalFrequency;
    }
}
