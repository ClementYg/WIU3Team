using UnityEngine;
using System.Collections.Generic;

public class QuestSystem : PersistentSingleton<QuestSystem>
{
    [Header("Quest System")]
    [SerializeField] IDManager idMan;

    [Header("Event Channels")]
    [SerializeField] EventVoid onQuestsUpdatedEvent;

    List<QuestInstance> assignedQuests = new();
    public List<QuestInstance> AssignedQuests => assignedQuests;

    public int AssignQuest(QuestInstance toAssign)
    {
        // Add the quest to our list, give it an ID
        assignedQuests.Add(toAssign);
        toAssign.questID = idMan.RequestID();

        // Raise the event
        onQuestsUpdatedEvent.Raise();

        return toAssign.questID;
    }

    public bool CompleteQuest(int questID)
    {
        Debug.Log("QuestSystem: complete quest called.");

        // Get the quest to remove
        QuestInstance toRemove = GetQuestWithID(questID);
        if (toRemove == null) return false;

        // Give the reward if there is one
        if (toRemove.questReward != null)
        {
            // Create a new item instance
            ItemInstance newItem = new(toRemove.questReward.itemData, toRemove.questReward.itemEffect);

            // Add it to the inventory
            Inventory.Instance.AddItem(newItem);
        }

        // Remove the quest
        assignedQuests.Remove(toRemove);

        // Raise the event
        onQuestsUpdatedEvent.Raise();

        return true;
    }

    private QuestInstance GetQuestWithID(int questID)
    {
        foreach (QuestInstance quest in assignedQuests)
        {
            if (quest.questID == questID)
            {
                return quest;
            }
        }

        return null;
    }
}
