using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    [Header("Quest NPC")]
    [SerializeField] QuestInstance quest;
    [SerializeField] QuestNPCData npcData;

    [Header("Event Channels")]
    [SerializeField] EventVoid onQuestCompletedEvent;

    int questID;

    bool isQuestAssigned = false;
    bool isQuestCompleted = false;

    private void OnEnable()
    {
        // Subscribe to the quest convo
        npcData.SubscribeToConvo(AssignQuest);

        // Validate quest completed event
        if (onQuestCompletedEvent != null)
        {
            onQuestCompletedEvent.Subscribe(CompleteQuest);
        }
        else
        {
            Debug.LogWarning("QuestNPC: Missing quest completion reference.");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to the quest convo
        npcData.UnsubscribeToConvo(AssignQuest);

        // Validate quest completed event
        if (onQuestCompletedEvent != null)
        {
            onQuestCompletedEvent.Unsubscribe(CompleteQuest);
        }
    }

    private void AssignQuest()
    {
        if (isQuestCompleted || isQuestAssigned) return;
        questID = npcData.AssignQuest(quest);
        isQuestAssigned = true;
    }

    private void CompleteQuest()
    {
        if (isQuestAssigned)
        {
            npcData.CompleteQuest(questID);
            isQuestCompleted = true;
        }
    }
}
