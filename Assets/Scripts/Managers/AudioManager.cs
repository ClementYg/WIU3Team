using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [Header("References")]
    [SerializeField] private SettingsData settings;
    
    [Header("Event Channels")]
    [SerializeField] private EventVoid OnVolumeChangedEvent;
    [SerializeField] private EventAudioClip OnBGMRequestEvent;
    [SerializeField] private EventAudioClip OnSFXRequestEvent;
    [SerializeField] private EventAudioClipFloat OnDialogueSFXRequestEvent;
    [SerializeField] private EventBool OnPlayerLowHealthEvent;
    [SerializeField] private EventVoid OnGameOverEvent;
    [SerializeField] private EventBool OnToggledPauseEvent;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource dialogueSource;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    private Coroutine fadeCoroutine;

    private const float NORMAL_LOWPASS = 22000f;
    private const float LOW_HEALTH_LOWPASS = 800f;

    private bool isLowHealth = false;

    private void OnEnable()
    {
        OnVolumeChangedEvent.Subscribe(OnVolumeChanged);
        OnBGMRequestEvent.Subscribe(PlayBGM);
        OnSFXRequestEvent.Subscribe(PlaySFX);
        OnDialogueSFXRequestEvent.Subscribe(PlayDialogueSFX);
        OnPlayerLowHealthEvent.Subscribe(OnPlayerLowHealth);
        OnGameOverEvent.Subscribe(OnGameOver);
        OnToggledPauseEvent.Subscribe(OnToggledPause);
    }

    private void OnDisable()
    {
        OnVolumeChangedEvent.Unsubscribe(OnVolumeChanged);
        OnBGMRequestEvent.Unsubscribe(PlayBGM);
        OnSFXRequestEvent.Unsubscribe(PlaySFX);
        OnDialogueSFXRequestEvent.Unsubscribe(PlayDialogueSFX);
        OnPlayerLowHealthEvent.Unsubscribe(OnPlayerLowHealth);
        OnGameOverEvent.Unsubscribe(OnGameOver);
        OnToggledPauseEvent.Unsubscribe(OnToggledPause);
    }

    private void Start()
    {
        ApplyVolumeSettings();
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    private void OnToggledPause(bool isPaused)
    {
        if (isPaused)
        {
            PauseBGM();
        }
        else
        {
            ResumeBGM();
        }
    }

    private void PauseBGM()
    {
        bgmSource.Pause(); 
    }

    private void ResumeBGM()
    {
        bgmSource.UnPause();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDialogueSFX(AudioClip clip, float pitch)
    {
        audioMixer.SetFloat("DialogueSFXPitch", pitch);
        dialogueSource.PlayOneShot(clip);
    }

    private void OnVolumeChanged()
    {
        ApplyVolumeSettings();
    }

    private void OnPlayerLowHealth(bool isLowHealth)
    {
        this.isLowHealth = isLowHealth;
        UpdateLowPassFilter();
    }

    private void OnGameOver()
    {
        fadeCoroutine = StartCoroutine(FadeOutBGM(1f));
    }

    private void UpdateLowPassFilter()
    {
        float cutoff = NORMAL_LOWPASS;
        if (isLowHealth)
        {
            cutoff = LOW_HEALTH_LOWPASS;
        }

        audioMixer.SetFloat("BGMLowPassCutoff", cutoff);
    }

    public void StartFadeBGM(float duration = 1f)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        StartCoroutine(FadeOutBGM(duration));
    }

    public void StopFadeBGM()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
    }

    public IEnumerator FadeOutBGM(float duration)
    {
        float startVolume = bgmSource.volume;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.volume = startVolume;
    }

    private void ApplyVolumeSettings()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(settings.MasterVolume, 0.0001f)) * 20f);
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(settings.BGMVolume, 0.0001f)) * 20f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(settings.SFXVolume, 0.0001f)) * 20f);
        audioMixer.SetFloat("DialogueSFXVolume", Mathf.Log10(Mathf.Max(settings.SFXVolume, 0.0001f)) * 20f);
    }
}
