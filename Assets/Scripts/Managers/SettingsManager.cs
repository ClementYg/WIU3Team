using UnityEngine;

public class SettingsManager : PersistentSingleton<SettingsManager>
{
    // The purpose of this is class is to save the audio settings of
    // the user using PlayerPrefs

    [Header("Settings Manager Data")]
    [SerializeField] private SettingsData settings;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Load()
    {
        settings.SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
        settings.SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume", 1f));
        settings.SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
        settings.SetDialogueVolume(PlayerPrefs.GetFloat("DialogueVolume", 1f));
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", settings.MasterVolume);
        PlayerPrefs.SetFloat("BGMVolume", settings.BGMVolume);
        PlayerPrefs.SetFloat("SFXVolume", settings.SFXVolume);
        PlayerPrefs.SetFloat("DialogueVolume", settings.DialogueVolume);
        PlayerPrefs.Save();
    }
}