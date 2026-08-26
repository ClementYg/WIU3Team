using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    [Header("Quest NPC")]
    [SerializeField] QuestInstance quest;
    [SerializeField] QuestNPCData npcData;
    [SerializeField] DialogueConversation questConvo;

    [Header("Event Channels")]
    [SerializeField] EventVoid onQuestCompletedEvent;

    int questID;

    bool isQuestAssigned = false;
    bool isQuestCompleted = false;

    private void OnEnable()
    {
        // Validate quest convo
        if (questConvo != null && questConvo.onConvoEndedEvent != null)
        {
            questConvo.onConvoEndedEvent.Subscribe(AssignQuest);
        }
        else
        {
            Debug.LogWarning("QuestNPC: Missing quest conversation references.");
        }

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
        // Validate quest convo
        if (questConvo != null && questConvo.onConvoEndedEvent != null)
        {
            questConvo.onConvoEndedEvent.Unsubscribe(AssignQuest);
        }

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
