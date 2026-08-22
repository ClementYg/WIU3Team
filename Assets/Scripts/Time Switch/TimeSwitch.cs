using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSwitch : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] EventVoid OnTimeTransitionStartedEvent;
    [SerializeField] EventVoid OnTimeTransitionEndedEvent;

    // References
    TimeSwitchReferences references;

    // Containers
    GameObject present;
    GameObject past;

    // Transition sequence
    [SerializeField] float transitionDuration = 3f;
    CameraShaker cmrShaker;
    ColorChannel prsntClrChannel;
    ColorChannel pstClrChannel;
    float transitionStartTime = -Mathf.Infinity;
    bool isInTransition = false;
    public bool IsTransitionDone => (isInTransition && (Time.time - transitionStartTime >= transitionDuration));

    bool isInPresent = true;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle the time state when the transition is done
        if (IsTransitionDone)
        {
            EndTransition();
        }
    }

    public void UseAtlas()
    {
        if (isInTransition) return;

        StartTransition();
    }

    private void Init()
    {
        // Assign all references
        references = FindAnyObjectByType<TimeSwitchReferences>();
        if (references == null)
        {
            Debug.LogError("TimeSwitch: Failed to find time switch references.");
            return;
        }

        references.AssignReferences();

        present = references.Present;
        past = references.Past;
        cmrShaker = references.CmrShaker;
        prsntClrChannel = references.PrsntClrChannel;
        pstClrChannel = references.PstClrChannel;

        if (
            present == null || past == null || cmrShaker == null ||
            prsntClrChannel == null || pstClrChannel == null
            )
        {
            Debug.LogError("TimeSwitch: Failed to assign references.");
            return;
        }

        // At the start, player should be in the present
        SetTimeState(true);

        // Set the camera shake duration to be the same as transition duration
        cmrShaker.SetSustainTime(transitionDuration);
    }

    private void StartTransition()
    {
        // Start the transition
        transitionStartTime = Time.time;
        isInTransition = true;

        // Do the camera shake
        cmrShaker.DoShake();

        // Do the color flash
        ToggleColorFlash();
        
        // Raise the event
        OnTimeTransitionStartedEvent.Raise();
    }

    private void EndTransition()
    {
        isInTransition = false;

        // Stop the color flash
        ToggleColorFlash();

        // Raise the event
        OnTimeTransitionEndedEvent.Raise();

        // Toggle the time state
        SetTimeState(!isInPresent);
    }

    private void SetTimeState(bool isInPresent)
    {
        this.isInPresent = isInPresent;

        present.SetActive(isInPresent);
        past.SetActive(!isInPresent);
    }

    private void ToggleColorFlash()
    {
        if (isInPresent)
        {
            prsntClrChannel.ToggleColorFlash();
        }
        else
        {
            pstClrChannel.ToggleColorFlash();
        }
    }

    private void OnSceneLoaded(Scene scn, LoadSceneMode mode)
    {
        Init();
    }
}
