using UnityEngine;

public class BGMHandler : MonoBehaviour
{
    public enum TimeState
    {
        PRESENT = 0,
        PAST
    }
    [Header("Event Channels")]
    [SerializeField] private EventVoid onTimeTransitionStartedEvent;
    [SerializeField] private EventVoid onTimeTransitionEndedEvent;

    [Header("Time Settings")]
    [SerializeField] private TimeState startingTime;

    [Header("BGM Clips")]
    [SerializeField] private AudioClip presentClip;
    [SerializeField] private AudioClip pastClip;
    private TimeState currentTime;

    private void OnEnable()
    {
        onTimeTransitionStartedEvent.Subscribe(OnTimeTransitionStarted);
        onTimeTransitionEndedEvent.Subscribe(OnTimeTransitionEnded);
    }

    private void OnDisable()
    {
        onTimeTransitionStartedEvent.Unsubscribe(OnTimeTransitionStarted);
        onTimeTransitionEndedEvent.Unsubscribe(OnTimeTransitionEnded);
    }

    private void Start()
    {
        currentTime = startingTime;
        if (currentTime == TimeState.PRESENT)
        {
            AudioManager.Instance.PlayBGM(presentClip);
        }
        else if (currentTime == TimeState.PAST)
        {
            AudioManager.Instance.PlayBGM(pastClip);
        }
    }

    private void OnTimeTransitionStarted()
    {
        AudioManager.Instance.StartFadeBGM(1f);
    }

    private void OnTimeTransitionEnded()
    {
        if (currentTime == TimeState.PRESENT)
        {
            currentTime = TimeState.PAST;
            AudioManager.Instance.PlayBGM(pastClip);
        }
        else if (currentTime == TimeState.PAST)
        {
            currentTime = TimeState.PRESENT;
            AudioManager.Instance.PlayBGM(presentClip);
        }
    }
}
