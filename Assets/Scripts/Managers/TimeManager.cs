using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Note: This is currently unused.
    // We may revisit this for possible future implementations of a pause menu.

    [Header("Event Channels")]
    [SerializeField] EventVoid OnSampleEvent;

    private void PauseTime()
    {
        Time.timeScale = 0f;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
    }
}
