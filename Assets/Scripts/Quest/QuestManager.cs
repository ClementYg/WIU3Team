using UnityEngine;
using System.Collections.Generic;

public class QuestManager : PersistentSingleton<QuestManager>
{
    List<QuestInstance> assignedQuests;

    public int AssignQuest(QuestInstance toAssign)
    {
        assignedQuests.Add(toAssign);

        return assignedQuests.Count - 1;
    }

    public bool CompleteQuest(int questIndex)
    {
        if (questIndex < 0 || questIndex > assignedQuests.Count - 1) return false;

        assignedQuests.RemoveAt(questIndex);

        return true;
    }
}
