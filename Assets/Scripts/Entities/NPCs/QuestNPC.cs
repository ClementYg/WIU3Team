using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    [Header("Quest NPC")]
    [SerializeField] QuestInstance quest;
    [SerializeField] QuestNPCData npcData;

    [Header("Event Channels")]
    [SerializeField] EventVoid onQuestCompletedEvent;
    [SerializeField] EventVoid onSubmitItemEvent;

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

        // Subscribe to the item submission
        onSubmitItemEvent.Subscribe(OnSubmitItem);
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

        // Unsubscribe to the item submission
        onSubmitItemEvent.Unsubscribe(OnSubmitItem);
    }

    private void Awake()
    {
        // Set the effect's bool to false first
        npcData.effect.canSubmitItem = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            npcData.effect.canSubmitItem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            npcData.effect.canSubmitItem = false;
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

    private void OnSubmitItem()
    {
        // Don't need to check whether the item exists; this function was triggered by
        // using the item in the toolbar.

        // Remove the item from the inventory
        Inventory.Instance.RemoveSelectedItem();

        // Trigger the item submission conversation
        DialogueManager.Instance.StartConversation(npcData.questCompletionConvo);
    }
}
