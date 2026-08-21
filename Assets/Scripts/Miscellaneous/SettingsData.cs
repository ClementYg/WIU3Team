using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/Data/Settings")]
public class SettingsData : ScriptableObject
{
    [Header("Audio Settings")]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float bgmVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float dialogueVolume = 1f;

    public float MasterVolume => masterVolume;
    public float BGMVolume => bgmVolume;
    public float SFXVolume => sfxVolume;

    public float DialogueVolume => dialogueVolume;

    public void SetMasterVolume(float newVolume) => masterVolume = newVolume;
    public void SetBGMVolume(float newVolume) => bgmVolume = newVolume;
    public void SetSFXVolume(float newVolume) => sfxVolume = newVolume;

    public void SetDialogueVolume(float newVolume) => dialogueVolume = newVolume;
}
