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
        // Get the quest to remove
        QuestInstance toRemove = GetQuestWithID(questID);
        if (toRemove == null) return false;

        // Note: Remember to give the reward

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
