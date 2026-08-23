using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class QuestInstance
{
    [Header("References")]
    public QuestData questData;
    public QuestReward questReward;

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
}
