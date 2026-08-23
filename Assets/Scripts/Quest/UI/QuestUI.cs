using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuestUI : MonoBehaviour
{
    [Header("Quest UI")]
    [SerializeField] Transform logsListTransform;
    [SerializeField] GameObject logPrefab;
    [SerializeField] Sprite todoSprite;
    [SerializeField] ButtonColorFlash bttnColorFlash;

    [Header("Event Channels")]
    [SerializeField] EventVoid onQuestsUpdatedEvent;

    [Header("Modifiers")]
    [SerializeField] float logPadding = 10f;

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
            GameObject newLog = Instantiate(logPrefab);

            newLog.transform.SetParent(logsListTransform);
            Vector3 newPos = newLog.transform.localPosition;
            newPos = Vector3.zero;
            newPos.y = currentOffsetY;
            newLog.transform.localPosition = newPos;
            currentOffsetY -= 150f - logPadding;

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

        // Do color flash
        bttnColorFlash.FlashForDuration();
    }
}
