using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] UITimer data;
    [SerializeField] TimerUI UI;

    [Header("Event Channels")]
    [SerializeField] EventVoid onTimerStartedEvent;

    float timer = 0f;
    bool isTimerEnabled = false;

    private void OnEnable()
    {
        onTimerStartedEvent.Subscribe(StartTimer);
    }

    private void OnDisable()
    {
        onTimerStartedEvent.Unsubscribe(StartTimer);
    }

    private void Awake()
    {
        timer = data.timeAtStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerEnabled == false) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            data.onTimerEndedEvent.Raise();
            timer = data.timeAtStart;
            isTimerEnabled = false;
        }

        // Calculate the UI display
        float zRotation = ((timer / data.timeAtStart) * 360f) + 90f;
        int intTimer = (int)((timer + 0.5f)); // Round up

        UI.UpdateUI(zRotation, intTimer.ToString());
    }

    private void StartTimer()
    {
        isTimerEnabled = true;
    }
}
