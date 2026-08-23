using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    [Header("Quest Log")]
    [SerializeField] Transform containerTransform;
    [SerializeField] GameObject logEntryPrefab;
    [SerializeField] Sprite todoSprite;

    [Header("Modifiers")]
    [SerializeField] float logEntryPadding = 10f;

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
        // Clear the current logs list
        foreach (GameObject log in logs)
        {
            Destroy(log);
        }

        logs.Clear();

        // Update the logs list with new data
        List<QuestInstance> assignedQuests = QuestSystem.Instance.AssignedQuests;
        float currentOffsetY = 265f;

        foreach (QuestInstance quest in assignedQuests)
        {
            GameObject newLog = Instantiate(logEntryPrefab);

            newLog.transform.SetParent(containerTransform);
            Vector3 newPos = newLog.transform.localPosition;
            newPos = Vector3.zero;
            newPos.y = currentOffsetY;
            newLog.transform.localPosition = newPos;
            currentOffsetY -= 150f - logEntryPadding;

            Transform textTransform = newLog.transform.GetChild(0);
            if (textTransform.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI txtMesh))
            {
                txtMesh.text = quest.questData.questName;
            }

            Transform statusTransform = newLog.transform.GetChild(1);
            if (statusTransform.TryGetComponent<Image>(out Image img))
            {
                img.sprite = todoSprite;
            }

            logs.Add(newLog);
        }
    }
}
