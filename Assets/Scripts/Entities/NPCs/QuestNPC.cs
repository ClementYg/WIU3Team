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
        ValidateEventReferences();
        
        onQuestCompletedEvent.Subscribe(CompleteQuest);
        questConvo.onConvoEndedEvent.Subscribe(AssignQuest);
    }

    private void OnDisable()
    {
        ValidateEventReferences();

        onQuestCompletedEvent.Unsubscribe(CompleteQuest);
        questConvo.onConvoEndedEvent.Unsubscribe(AssignQuest);
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

    private bool ValidateEventReferences()
    {
        // Validate quest convo
        if (questConvo == null || questConvo.onConvoEndedEvent == null)
        {
            Debug.LogWarning("QuestNPC: Missing quest convo reference.", this);
            return false;
        }

        // Validate quest completed event
        if (onQuestCompletedEvent == null)
        {
            Debug.LogWarning("QuestNPC: Missing quest completed event.", this);
            return false;
        }

        return true;
    }
}
