using UnityEngine;
using UnityEngine.UI;

public class UISettingsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider dialogueSlider;
    [SerializeField] private SettingsData settingsData;

    [Header("Event Channels")]
    [SerializeField] private EventVoid OnVolumeChangedEvent;

    private void OnEnable()
    {
        // Apply values to sliders from saved settings previously
        masterSlider.value = settingsData.MasterVolume;
        bgmSlider.value = settingsData.BGMVolume;
        sfxSlider.value = settingsData.SFXVolume;
        dialogueSlider.value = settingsData.DialogueVolume;

        masterSlider.onValueChanged.AddListener(OnMasterChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
        dialogueSlider.onValueChanged.AddListener(OnDialogueChanged);
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(OnMasterChanged);
        bgmSlider.onValueChanged.RemoveListener(OnBGMChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXChanged);
        dialogueSlider.onValueChanged.RemoveListener(OnDialogueChanged);
    }

    public void OnMasterChanged(float value)
    {
        settingsData.SetMasterVolume(value);
        OnVolumeChangedEvent.Raise();
    }

    public void OnBGMChanged(float value)
    {
        settingsData.SetBGMVolume(value);
        OnVolumeChangedEvent.Raise();
    }

    public void OnSFXChanged(float value)
    {
        settingsData.SetSFXVolume(value);
        OnVolumeChangedEvent.Raise();
    }

    public void OnDialogueChanged(float value)
    {
        settingsData.SetDialogueVolume(value);
        OnVolumeChangedEvent.Raise();
    }
}
