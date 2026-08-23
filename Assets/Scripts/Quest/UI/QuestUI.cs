using UnityEngine;
using System.Collections.Generic;

public class QuestUI : MonoBehaviour
{
    [Header("Quest UI")]
    [SerializeField] Transform logsListTransform;
    [SerializeField] GameObject logPrefab;

    [Header("Event Channels")]
    [SerializeField] EventVoid onQuestsUpdatedEvent;

    List<GameObject> logs = new();

    private void OnEnable()
    {
        onQuestsUpdatedEvent.Subscribe(UpdateUI);
    }

    private void OnDisable()
    {
        onQuestsUpdatedEvent.Unsubscribe(UpdateUI);
    }

    private void UpdateUI()
    {
        List<QuestInstance> assignedQuests = QuestSystem.Instance.AssignedQuests;

        // Clear the current logs list
        foreach (GameObject log in logs)
        {
            Destroy(log);
        }

        logs.Clear();

        // Update the logs list with new data
        foreach (QuestInstance quest in assignedQuests)
        {
            GameObject newLog = Instantiate(logPrefab);

            newLog.transform.SetParent(logsListTransform);
            newLog.transform.localPosition = Vector3.zero;

            logs.Add(newLog);
        }
    }
}
