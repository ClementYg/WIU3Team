using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class QuestInstance
{
    public QuestData questData;
    public QuestReward questReward;

    [Header("Tasks")]
    List<TaskInstance> tasks;

    public bool IsQuestCompleted
    {
        get
        {
            foreach (TaskInstance task in tasks)
            {
                if (!task.IsCompleted) return false;
            }

            return true;
        }
    }

    // Need a reference to the NPC who gave this task, to be implemented when NPCs are in
    //public NPC questGiver;
}
