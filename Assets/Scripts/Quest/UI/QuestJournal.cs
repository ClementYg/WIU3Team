using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestJournal : MonoBehaviour
{
    [Header("Quest Journal")]
    [SerializeField] Transform contentTransform;
    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] TextMeshProUGUI questContext;
    [SerializeField] TextMeshProUGUI questSource;
    [SerializeField] GameObject instructionPrefab;

    [Header("Modifiers")]
    [SerializeField] float instructionPadding = 25f;

    [Header("Event Channels")]
    [SerializeField] EventQuestLogEntry onLogEntryClickedEvent;

    List<GameObject> instructions = new();

    private void OnEnable()
    {
        onLogEntryClickedEvent.Subscribe(DisplayEntry);
    }

    private void OnDisable()
    {
        onLogEntryClickedEvent.Unsubscribe(DisplayEntry);
    }

    private void DisplayEntry(QuestLogEntry entry)
    {
        // Get the quest instance
        QuestInstance quest = entry.quest;
        if (quest == null) return;

        // Set all the text data
        questName.text = quest.questData.questName;
        questContext.text = quest.questData.source.questContext;
        
        if (quest.questData.source is NPCQuestSource source)
        {
            questSource.text = "- " + source.npcData.npcName;
        }

        foreach (GameObject instruction in instructions)
        {
            Destroy(instruction);
        }

        instructions.Clear();

        float currentOffsetY = -620f;

        foreach (TaskInstance task in quest.tasks)
        {
            GameObject newInstruction = Instantiate(instructionPrefab);

            // Set position
            newInstruction.transform.SetParent(contentTransform);
            Vector3 newPos = newInstruction.transform.localPosition;
            newPos = Vector3.zero;
            newPos.y = currentOffsetY;
            newInstruction.transform.localPosition = newPos;
            currentOffsetY -= 50f - instructionPadding;

            // Set UI
            if (newInstruction.transform.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI txtMesh))
            {
                txtMesh.text = task.taskData.instruction;
            }

            instructions.Add(newInstruction);
        }
    }
}
