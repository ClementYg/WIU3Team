using System.Collections;
using UnityEngine;

public enum CutsceneAudioType { BGM, SFX }

[System.Serializable]
public class CutsceneAudioStep : CutsceneStep
{
    public CutsceneAudioType audioType;
    public AudioClip clip;

    public override IEnumerator Execute()
    {
        if (audioType == CutsceneAudioType.BGM)
        {
            CutsceneManager.Instance.onBGMRequestEvent.Raise(clip);
        }
        else if (audioType == CutsceneAudioType.SFX)
        {
            CutsceneManager.Instance.onSFXRequestEvent.Raise(clip);
        }

        // If blocking before next CutsceneStep, wait out the entire clip length
        // before moving on. Useful for SFX mainly
        if (blocking && clip != null)
        {
            yield return new WaitForSeconds(clip.length);
        }
    }
}
