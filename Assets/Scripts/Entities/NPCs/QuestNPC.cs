using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    [Header("Quest NPC")]
    [SerializeField] QuestInstance quest;
    [SerializeField] QuestNPCData npcData;
    [SerializeField] DialogueConversation questConvo;
    [SerializeField] ComponentCache questCache;

    private void OnEnable()
    {
        if (ValidateQuestConvo() == false) return;
        questConvo.onConvoEndedEvent.Subscribe(AssignQuest);
    }

    private void OnDisable()
    {
        if (ValidateQuestConvo() == false) return;
        questConvo.onConvoEndedEvent.Unsubscribe(AssignQuest);
    }

    private void AssignQuest()
    {
        npcData.AssignQuest(quest, questCache);
    }

    private bool ValidateQuestConvo()
    {
        if (questConvo == null || questConvo.onConvoEndedEvent == null)
        {
            Debug.LogWarning("QuestNPC: Missing quest convo reference.", this);
            return false;
        }

        return true;
    }
}
