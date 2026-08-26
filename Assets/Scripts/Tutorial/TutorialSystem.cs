using UnityEngine;

public class TutorialSystem : PersistentSingleton<TutorialSystem>
{
    [Header("Event Channels")]
    [SerializeField] EventTutorialData onTriggerTutorialEvent;

    private void OnEnable()
    {
        onTriggerTutorialEvent.Subscribe(OnTriggerTutorial);
    }

    private void OnDisable()
    {
        onTriggerTutorialEvent.Unsubscribe(OnTriggerTutorial);
    }

    private void OnTriggerTutorial(TutorialData data)
    {
        TutorialInstance tutorial = new(data);
        tutorial.StartTutorial();
    }
}
