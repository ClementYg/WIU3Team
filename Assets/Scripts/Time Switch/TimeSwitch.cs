using UnityEngine;

public class TimeSwitch : MonoBehaviour
{
    [Header("Time Switch")]
    [SerializeField] GameObject presentContainer;
    [SerializeField] GameObject pastContainer;

    [Header("Transition Sequence")]
    [SerializeField] CameraShaker cmrShaker;
    [SerializeField] ColorChannel prsntClrChannel;
    [SerializeField] ColorChannel pstClrChannel;
    [SerializeField] float transitionDuration = 3f;
    float transitionStartTime = -Mathf.Infinity;
    bool isInTransition = false;
    public bool IsTransitionDone => (isInTransition && (Time.time - transitionStartTime >= transitionDuration));

    [Header("Event Channels")]
    [SerializeField] EventVoid OnTimeTransitionStartedEvent;
    [SerializeField] EventVoid OnTimeTransitionEndedEvent;

    bool isInPresent = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // At the start, player should be in the present
        SetTimeState(true);

        // Set the camera shake duration to be the same as transition duration
        cmrShaker.SetSustainTime(transitionDuration);
    }

    private void Update()
    {
        // Toggle the time state when the transition is done
        if (IsTransitionDone)
        {
            EndTransition();
        }
    }

    public void UseAtlas()
    {
        StartTransition();
    }

    private void StartTransition()
    {
        // Start the transition
        transitionStartTime = Time.time;
        isInTransition = true;

        // Do the camera shake
        cmrShaker.DoShake();

        // Do the color flash
        ToggleColorChannel();
        
        // Raise the event
        OnTimeTransitionStartedEvent.Raise();
    }

    private void EndTransition()
    {
        isInTransition = false;

        // Stop the color flash
        ToggleColorChannel();

        // Raise the event
        OnTimeTransitionEndedEvent.Raise();

        // Toggle the time state
        SetTimeState(!isInPresent);
    }

    private void SetTimeState(bool isInPresent)
    {
        this.isInPresent = isInPresent;

        presentContainer.SetActive(isInPresent);
        pastContainer.SetActive(!isInPresent);
    }

    private void ToggleColorChannel()
    {
        if (isInTransition)
        {
            if (isInPresent)
            {
                prsntClrChannel.StartColorFlash();
            }
            else
            {
                pstClrChannel.StartColorFlash();
            }
        }
        else
        {
            if (isInPresent)
            {
                prsntClrChannel.StopColorFlash();
            }
            else
            {
                pstClrChannel.StopColorFlash();
            }
        }
    }
}
