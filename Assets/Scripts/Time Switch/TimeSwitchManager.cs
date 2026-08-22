using UnityEngine;

public class TimeSwitchManager : MonoBehaviour
{
    [Header("Time Switch")]
    [SerializeField] GameObject presentContainer;
    [SerializeField] GameObject pastContainer;

    [Header("Transition Sequence")]
    [SerializeField] CameraShaker cmrShaker;
    [SerializeField] float transitionDuration = 3f;
    float transitionStartTime = -Mathf.Infinity;
    bool isInTransition = false;
    public bool IsTransitionDone => (isInTransition && (Time.time - transitionStartTime >= transitionDuration));

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
            Debug.Log("TimeSwitchManager: toggling time state");
            SetTimeState(!isInPresent);
            isInTransition = false;
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
    }

    private void SetTimeState(bool isInPresent)
    {
        this.isInPresent = isInPresent;

        presentContainer.SetActive(isInPresent);
        pastContainer.SetActive(!isInPresent);
    }
}
